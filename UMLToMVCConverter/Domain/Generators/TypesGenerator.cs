namespace UMLToMVCConverter.Domain.Generators
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using UMLToMVCConverter.Common;
    using UMLToMVCConverter.Domain.Factories.Interfaces;
    using UMLToMVCConverter.Domain.Generators.Interfaces;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.Domain.Repositories.Interfaces;
    using UMLToMVCConverter.UMLHelpers;
    using UMLToMVCConverter.UMLHelpers.Interfaces;
    using UMLToMVCConverter.XmiTools;
    using UMLToMVCConverter.XmiTools.Interfaces;

    public class TypesGenerator : ITypesGenerator
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IPropertyFactory propertyFactory;
        private readonly ITypesRepository typesRepository;
        private readonly IAssociationsRepository associationsRepository;

        public TypesGenerator(IXmiWrapper xmiWrapper,
            IUmlTypesHelper umlTypesHelper,
            IUmlVisibilityMapper umlVisibilityMapper,
            IPropertyFactory propertyFactory,
            ITypesRepository typesRepository,
            IAssociationsRepository associationsRepository)
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
                .GetXProperties(xType);
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
                var name = xOperation.ObligatoryAttributeValue("name").ToCamelCase();

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