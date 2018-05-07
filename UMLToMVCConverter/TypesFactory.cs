﻿namespace UMLToMVCConverter
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Xml.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;
    using UMLToMVCConverter.Mappers;

    public class TypesFactory : ITypesFactory
    {
        private readonly IXmiWrapper xmiWrapper;
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IPropertyGenerator propertyGenerator;

        public TypesFactory(IXmiWrapper xmiWrapper, IUmlTypesHelper umlTypesHelper, IUmlVisibilityMapper umlVisibilityMapper, IPropertyGenerator propertyGenerator)
        {
            this.xmiWrapper = xmiWrapper;
            this.umlTypesHelper = umlTypesHelper;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.propertyGenerator = propertyGenerator;
        }

        public IEnumerable<ExtendedCodeTypeDeclaration> Create(XElement xUmlModel)
        {
            var xTypes = this.xmiWrapper.GetXTypes(xUmlModel)
                .ToList();

            var typeDeclarations = this.DeclareTypes(xTypes).ToList();

            var xTypesToBuild = xTypes
                .Where(t => !"nestedClassifier".Equals(t.Name.ToString()));

            var types = this.BuildTypes(xTypesToBuild, typeDeclarations).ToList();

            this.GenerateInheritanceRelations(xTypes, types);

            return types;
        }

        private IEnumerable<ExtendedCodeTypeDeclaration> DeclareTypes(IEnumerable<XElement> xTypes)
        {
            var typeDeclarations = new List<ExtendedCodeTypeDeclaration>();

            foreach (var type in xTypes)
            {
                var typeDeclaration = new ExtendedCodeTypeDeclaration(type.ObligatoryAttributeValue("name"))
                {
                    XmiID = this.xmiWrapper.GetElementsId(type)
                };

                typeDeclarations.Add(typeDeclaration);
            }

            return typeDeclarations;
        }

        private IEnumerable<ExtendedCodeTypeDeclaration> BuildTypes(
            IEnumerable<XElement> xTypesToBuild,
            List<ExtendedCodeTypeDeclaration> typeDeclarations)
        {
            var types = new List<ExtendedCodeTypeDeclaration>();

            this.umlTypesHelper.CodeTypeDeclarations = types;

            foreach (var xType in xTypesToBuild)
            {
                types.Add(this.BuildType(xType, typeDeclarations));
            }

            return types;
        }

        private ExtendedCodeTypeDeclaration BuildType(
            XElement xType,
            List<ExtendedCodeTypeDeclaration> typeDeclarations)
        {
            var xTypeName = xType.ObligatoryAttributeValue("name");
            var type = typeDeclarations.Single(t => t.Name.Equals(xTypeName));

            type.IsClass = this.umlTypesHelper.IsClass(xType);
            type.IsStruct = this.umlTypesHelper.IsStruct(xType);
            type.IsEnum = this.umlTypesHelper.IsEnum(xType);

            type.TypeAttributes = TypeAttributes.Public;

            if (this.umlTypesHelper.IsAbstract(xType))
            {
                type.TypeAttributes = type.TypeAttributes | TypeAttributes.Abstract;
            }

            var nestedClasses = xType.Descendants("nestedClassifier").ToList();
            foreach (var nestedClass in nestedClasses)
            {
                var ctdNested = this.BuildType(nestedClass, typeDeclarations);
                type.Members.Add(ctdNested);
            }

            this.GenerateProperties(xType, type);

            this.GenerateMethods(xType, type);

            return type;
        }

        private void GenerateProperties(XElement xType, ExtendedCodeTypeDeclaration type)
        {
            var xAttributes = this.xmiWrapper
                .GetXAttributes(xType)
                .Where(a => string.IsNullOrWhiteSpace(a.OptionalAttributeValue("association")));
            foreach (var xAttribute in xAttributes)
            {
                var property = this.propertyGenerator.Generate(type, xAttribute);
                type.Members.Add(property);
            }

            if (type.IsEnum)
            {
                var counter = 0;
                foreach (var literal in this.xmiWrapper.GetLiterals(xType))
                {
                    type.Literals.Add(++counter, literal.ObligatoryAttributeValue("name"));
                }
                var property = this.propertyGenerator.GenerateBasicProperty("Name", typeof(string));
                type.Members.Add(property);
            }
        }

        private void GenerateMethods(XElement xType, CodeTypeDeclaration type)
        {
            var xOperations = this.xmiWrapper.GetXOperations(xType);
            foreach (var xOperation in xOperations)
            {
                var method = new CodeMemberMethod { Name = xOperation.ObligatoryAttributeValue("name") };

                var returnParameter = this.xmiWrapper.GetXReturnParameter(xOperation);
                var returnType = this.umlTypesHelper.GetXElementCsharpType(returnParameter);
                var typeReference = ExtendedCodeTypeReference.CreateForType(returnType);
                method.ReturnType = typeReference;

                var parameters = this.xmiWrapper.GetXParameters(xOperation);
                foreach (var xParameter in parameters)
                {
                    var parameterType = this.umlTypesHelper.GetXElementCsharpType(xParameter);
                    var parameterName = xParameter.ObligatoryAttributeValue("name");
                    var parameter =
                        new ExtendedCodeParameterDeclarationExpression(parameterType, parameterName);
                    method.Parameters.Add(parameter);
                }

                //visibility
                var umlVisibility = xOperation.ObligatoryAttributeValue("visibility");
                var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharp(umlVisibility);
                method.Attributes = method.Attributes | cSharpVisibility;

                var isStatic = Convert.ToBoolean(xOperation.OptionalAttributeValue("isStatic"));
                if (isStatic)
                {
                    method.Attributes = method.Attributes | MemberAttributes.Static;
                }

                type.Members.Add(method);
            }
        }

        private void GenerateInheritanceRelations(
            IEnumerable<XElement> xTypes,
            List<ExtendedCodeTypeDeclaration> types)
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

                    var baseType = types.FirstOrDefault(i => i.Name == baseTypeName);

                    Insist.IsNotNull(baseType, nameof(baseType));
                    var typeReference = new CodeTypeReference(baseType.Name);

                    var childTypeName = type.ObligatoryAttributeValue("name");
                    var childType = types.FirstOrDefault(i => i.Name == childTypeName);

                    Insist.IsNotNull(childType, nameof(childType));
                    childType.BaseTypes.Add(typeReference);
                }
            }
        }
    }
}