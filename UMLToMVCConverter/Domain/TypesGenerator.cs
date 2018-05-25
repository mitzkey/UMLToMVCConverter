namespace UMLToMVCConverter.Domain
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.UMLHelpers;
    using UMLToMVCConverter.XmiTools;

    public class TypesGenerator : ITypesGenerator
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IPropertyFactory propertyFactory;
        private readonly ITypesRepository typesRepository;
        private readonly IAssociationsRepository associationsRepository;

        public TypesGenerator(IXmiWrapper xmiWrapper, IUmlTypesHelper umlTypesHelper, IUmlVisibilityMapper umlVisibilityMapper, IPropertyFactory propertyFactory, ITypesRepository typesRepository, IAssociationsRepository associationsRepository)
        {
            this.xmiWrapper = xmiWrapper;
            this.umlTypesHelper = umlTypesHelper;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.propertyFactory = propertyFactory;
            this.typesRepository = typesRepository;
            this.associationsRepository = associationsRepository;
        }

        public void Generate(XElement xUmlModel)
        {
            var xTypes = this.xmiWrapper.GetXTypes(xUmlModel)
                .ToList();

            this.DeclareTypes(xTypes);

            var xTypesToBuild = xTypes
                .Where(t => !"nestedClassifier".Equals(t.Name.ToString()));

            this.BuildTypes(xTypesToBuild);

            this.GenerateInheritanceRelations(xTypes);
        }

        public void GenerateManyToManyAssociationTypes()
        {
            foreach (var association in this.associationsRepository
                .GetAllAssociations()
                .Where(x => x.Multiplicity == RelationshipMultiplicity.ManyToMany).ToList())
            {
                var associationTypeNameBuilder = new StringBuilder();
                association.Members.ForEach(x => associationTypeNameBuilder.Append(x.Name));
                var associationTypeName = associationTypeNameBuilder.ToString();
                var type = new TypeModel(associationTypeName, true, CSharpVisibilityString.Public);
                this.typesRepository.Add(type);

                foreach (var member in association.Members)
                {
                    var oppositeMember = association.Members.Single(x => !x.Equals(member));
                    var reducedMultiplicity = this.ReduceMultiplicity(member.Multiplicity);
                    var associationTypeMember = new AssociationEndMember(null, oppositeMember.Name, reducedMultiplicity, oppositeMember.AggregationKind, type, oppositeMember.Navigable);
                    var parentAssociationMemberTypesNewMember = new AssociationEndMember(null, member.Name, oppositeMember.Multiplicity, member.AggregationKind, member.Type, member.Navigable);
                    var childAssociationMembers = new List<AssociationEndMember> { associationTypeMember, parentAssociationMemberTypesNewMember };
                    var childAssociation = new Association(childAssociationMembers, null);
                    this.associationsRepository.Add(childAssociation);
                }
            }
        }

        private Multiplicity ReduceMultiplicity(Multiplicity multiplicity)
        {
            if (multiplicity != Multiplicity.OneOrMore
                && multiplicity != Multiplicity.ZeroOrMore)
            {
                throw new ArgumentException("Can't reduce provided multiplicity");
            }

            return multiplicity == Multiplicity.ZeroOrMore
                ? Multiplicity.ZeroOrOne
                : Multiplicity.ExactlyOne;
        }

        private void DeclareTypes(IEnumerable<XElement> xTypes)
        {
            foreach (var type in xTypes)
            {
                var name = type.ObligatoryAttributeValue("name");
                var typeDeclaration = new TypeModel(name)
                {
                    XmiID = this.xmiWrapper.GetElementsId(type)
                };

                this.typesRepository.DeclareType(typeDeclaration);
            }
        }

        private void BuildTypes(
            IEnumerable<XElement> xTypesToBuild)
        {
            var enumerations =
                xTypesToBuild.Where(x => this.xmiWrapper.GetXElementType(x).Equals(XElementType.Enumeration));

            var otherTypes =
                xTypesToBuild.Where(x => !this.xmiWrapper.GetXElementType(x).Equals(XElementType.Enumeration));

            foreach (var xEnumeration in enumerations)
            {
                var type = this.BuildType(xEnumeration);
                this.typesRepository.Add(type);

                this.GenerateProperties(xEnumeration, type);

                this.GenerateMethods(xEnumeration, type);
            }

            foreach (var xType in otherTypes)
            {
                var type = this.BuildType(xType);
                this.typesRepository.Add(type);

                this.GenerateProperties(xType, type);

                this.GenerateMethods(xType, type);
            }
        }

        private TypeModel BuildType(
            XElement xType)
        {
            var xTypeName = xType.ObligatoryAttributeValue("name");
            var type = this.typesRepository.GetTypeDeclaration(xTypeName);

            type.IsClass = this.umlTypesHelper.IsClass(xType);
            type.IsStruct = this.umlTypesHelper.IsStruct(xType);
            type.IsEnum = this.umlTypesHelper.IsEnum(xType);

            type.Visibility = CSharpVisibilityString.Public;

            if (this.umlTypesHelper.IsAbstract(xType))
            {
                type.IsAbstract = true;
            }

            var nestedClasses = xType.Descendants("nestedClassifier").ToList();
            foreach (var nestedClass in nestedClasses)
            {
                var ctdNested = this.BuildType(nestedClass);
                type.NestedClasses.Add(ctdNested);
            }

            return type;
        }

        private void GenerateProperties(XElement xType, TypeModel type)
        {
            var xAttributes = this.xmiWrapper
                .GetXAttributes(xType);
            foreach (var xAttribute in xAttributes)
            {
                var property = this.propertyFactory.Create(type, xAttribute);
                if (property != null)
                {
                    type.Properties.Add(property);
                }
            }

            if (type.IsEnum)
            {
                var counter = 0;
                foreach (var literal in this.xmiWrapper.GetLiterals(xType))
                {
                    type.Literals.Add(++counter, literal.ObligatoryAttributeValue("name"));
                }
                var property = this.propertyFactory.CreateBasicProperty("Name", typeof(string));
                type.Properties.Add(property);
            }
        }

        private void GenerateMethods(XElement xType, TypeModel type)
        {
            var xOperations = this.xmiWrapper.GetXOperations(xType);
            foreach (var xOperation in xOperations)
            {
                var name = xOperation.ObligatoryAttributeValue("name");

                var xReturnParameter = this.xmiWrapper.GetXReturnParameter(xOperation);

                TypeReference returnType;
                if (xReturnParameter == null)
                {
                    returnType = TypeReference.Builder()
                        .SetType(typeof(void))
                        .IsBaseType(true)
                        .Build();
                }
                else
                {
                    returnType = this.umlTypesHelper.GetXElementCsharpType(xReturnParameter);
                }

                var parameters = new List<MethodParameter>();
                var xParameters = this.xmiWrapper.GetXParameters(xOperation);
                foreach (var xParameter in xParameters)
                {
                    var parameterType = this.umlTypesHelper.GetXElementCsharpType(xParameter);
                    var parameterName = xParameter.ObligatoryAttributeValue("name");
                    var parameter =
                        new MethodParameter(parameterType, parameterName);
                    parameters.Add(parameter);
                }

                //visibility
                var umlVisibility = xOperation.ObligatoryAttributeValue("visibility");
                var cSharpVisibilityString = this.umlVisibilityMapper.UmlToCsharpString(umlVisibility);
                var visibility =  cSharpVisibilityString;

                var isStatic = Convert.ToBoolean(xOperation.OptionalAttributeValue("isStatic"));

                var method = new Method(name, returnType, parameters, visibility, isStatic);

                type.Methods.Add(method);
            }
        }

        private void GenerateInheritanceRelations(
            IEnumerable<XElement> xTypes)
        {
            foreach (var type in xTypes)
            {
                var xCurrentTypeGeneralization = this.xmiWrapper.GetXTypeGeneralization(type);
                if (xCurrentTypeGeneralization != null)
                {
                    var baseTypeId = xCurrentTypeGeneralization.ObligatoryAttributeValue("general");

                    var xBaseType = this.xmiWrapper.GetXElementById(baseTypeId);

                    var baseTypeName = xBaseType.ObligatoryAttributeValue("name");

                    var baseType = this.typesRepository.GetTypeByName(baseTypeName);

                    Insist.IsNotNull(baseType, nameof(baseType));

                    var typeReference = new CodeTypeReference(baseType.Name);

                    var childTypeName = type.ObligatoryAttributeValue("name");
                    var childType = this.typesRepository.GetTypeByName(childTypeName);

                    Insist.IsNotNull(childType, nameof(childType));

                    childType.BaseTypes.Add(typeReference);
                }
            }
        }
    }
}