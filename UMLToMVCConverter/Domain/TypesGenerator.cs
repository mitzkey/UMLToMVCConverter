namespace UMLToMVCConverter.Domain
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
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
        private readonly IPropertyGenerator propertyGenerator;
        private readonly ITypesRepository typesRepository;

        public TypesGenerator(IXmiWrapper xmiWrapper, IUmlTypesHelper umlTypesHelper, IUmlVisibilityMapper umlVisibilityMapper, IPropertyGenerator propertyGenerator, ITypesRepository typesRepository)
        {
            this.xmiWrapper = xmiWrapper;
            this.umlTypesHelper = umlTypesHelper;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.propertyGenerator = propertyGenerator;
            this.typesRepository = typesRepository;
        }

        public void Generate(XElement xUmlModel)
        {
            var xTypes = this.xmiWrapper.GetXTypes(xUmlModel)
                .ToList();

            var typeDeclarations = this.DeclareTypes(xTypes).ToList();

            var xTypesToBuild = xTypes
                .Where(t => !"nestedClassifier".Equals(t.Name.ToString()));

            this.BuildTypes(xTypesToBuild, typeDeclarations);

            this.GenerateInheritanceRelations(xTypes);
        }

        private IEnumerable<TypeModel> DeclareTypes(IEnumerable<XElement> xTypes)
        {
            var typeDeclarations = new List<TypeModel>();

            foreach (var type in xTypes)
            {
                var name = type.ObligatoryAttributeValue("name");
                var typeDeclaration = new TypeModel(name)
                {
                    XmiID = this.xmiWrapper.GetElementsId(type)
                };

                typeDeclarations.Add(typeDeclaration);
            }

            return typeDeclarations;
        }

        private void BuildTypes(
            IEnumerable<XElement> xTypesToBuild,
            List<TypeModel> typeDeclarations)
        {
            var enumerations =
                xTypesToBuild.Where(x => this.xmiWrapper.GetXElementType(x).Equals(XElementType.Enumeration));

            var otherTypes =
                xTypesToBuild.Where(x => !this.xmiWrapper.GetXElementType(x).Equals(XElementType.Enumeration));

            foreach (var xEnumeration in enumerations)
            {
                var type = this.BuildType(xEnumeration, typeDeclarations);
                this.typesRepository.Add(type);

                this.GenerateProperties(xEnumeration, type);

                this.GenerateMethods(xEnumeration, type);
            }

            foreach (var xType in otherTypes)
            {
                var type = this.BuildType(xType, typeDeclarations);
                this.typesRepository.Add(type);

                this.GenerateProperties(xType, type);

                this.GenerateMethods(xType, type);
            }
        }

        private TypeModel BuildType(
            XElement xType,
            List<TypeModel> typeDeclarations)
        {
            var xTypeName = xType.ObligatoryAttributeValue("name");
            var type = typeDeclarations.Single(t => t.Name.Equals(xTypeName));

            type.IsClass = this.umlTypesHelper.IsClass(xType);
            type.IsStruct = this.umlTypesHelper.IsStruct(xType);
            type.IsEnum = this.umlTypesHelper.IsEnum(xType);

            type.Visibility = "public";

            if (this.umlTypesHelper.IsAbstract(xType))
            {
                type.IsAbstract = true;
            }

            var nestedClasses = xType.Descendants("nestedClassifier").ToList();
            foreach (var nestedClass in nestedClasses)
            {
                var ctdNested = this.BuildType(nestedClass, typeDeclarations);
                type.NestedClasses.Add(ctdNested);
            }

            return type;
        }

        private void GenerateProperties(XElement xType, TypeModel type)
        {
            var xAttributes = this.xmiWrapper
                .GetXAttributes(xType)
                .Where(a => string.IsNullOrWhiteSpace(a.OptionalAttributeValue("association")));
            foreach (var xAttribute in xAttributes)
            {
                var property = this.propertyGenerator.Generate(type, xAttribute);
                type.Properties.Add(property);
            }

            if (type.IsEnum)
            {
                var counter = 0;
                foreach (var literal in this.xmiWrapper.GetLiterals(xType))
                {
                    type.Literals.Add(++counter, literal.ObligatoryAttributeValue("name"));
                }
                var property = this.propertyGenerator.GenerateBasicProperty("Name", typeof(string));
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
                var extendedReturnType = this.umlTypesHelper.GetXElementCsharpType(xReturnParameter);
                var typeReference = ExtendedCodeTypeReference.CreateForType(extendedReturnType);
                var returnType = typeReference;

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
                var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharpString(umlVisibility);
                var visibility =  cSharpVisibility;

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
                //klasa bazowa
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