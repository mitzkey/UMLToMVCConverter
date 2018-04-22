namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.CodeDom;
    using System.Reflection;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.ExtensionMethods;
    using UMLToMVCConverter.Interfaces;
    using UMLToMVCConverter.Mappers;

    public class DataModelGenerator : IDataModelGenerator
    {
        private readonly List<ExtendedCodeTypeDeclaration> types;
        private readonly List<ExtendedCodeTypeDeclaration> typeDeclarations;
        private readonly IXmiWrapper xmiWrapper;
        private readonly IUmlTypesHelper umlTypesHelper;
        private readonly IMvcProjectConfigurator mvcProjectConfigurator;
        private readonly IUmlVisibilityMapper umlVisibilityMapper;
        private readonly IAttributeNameResolver attributeNameResolver;
        private readonly List<IRelationship> relationships;
        private readonly IRelationshipFactory relationshipFactory;

        public DataModelGenerator(
            IMvcProjectConfigurator mvcProjectConfigurator,
            IXmiWrapper xmiWrapper,
            IUmlTypesHelper umlTypesHelper,
            IUmlVisibilityMapper umlVisibilityMapper,
            IAttributeNameResolver attributeNameResolver,
            IRelationshipFactory relationshipFactory)
        {
            this.mvcProjectConfigurator = mvcProjectConfigurator;

            this.types = new List<ExtendedCodeTypeDeclaration>();
            this.typeDeclarations = new List<ExtendedCodeTypeDeclaration>();
            this.relationships = new List<IRelationship>();

            this.xmiWrapper = xmiWrapper;
            this.umlTypesHelper = umlTypesHelper;
            this.umlTypesHelper.CodeTypeDeclarations = this.types;
            this.umlVisibilityMapper = umlVisibilityMapper;
            this.attributeNameResolver = attributeNameResolver;
            this.relationshipFactory = relationshipFactory;
        }
        public string GenerateMvcFiles()
        {
            var umlModels = this.xmiWrapper.GetXUmlModels();
            foreach (var umlModel in umlModels)
            {
                var xTypes = this.xmiWrapper.GetXTypes(umlModel)
                    .ToList();

                var xTypesToBuild = xTypes
                    .Where(t => !"nestedClassifier".Equals(t.Name.ToString()));

                foreach (var type in xTypes)
                {
                    this.DeclareType(type);
                }

                foreach (var type in xTypesToBuild)
                {
                    var codeTypeDeclaration = this.BuildType(type);
                    this.types.Add(codeTypeDeclaration);                                                                               
                }

                this.GenerateInheritanceRelations(xTypes);

                this.GenerateAssociations(this.xmiWrapper.GetXAssociations(umlModel));
            }

            this.mvcProjectConfigurator.SetUpMvcProject(this.types, this.relationships);

            return "File successfully processed";
        }

        private void GenerateAssociations(IEnumerable<XElement> xAssociations)
        {
            foreach (var xAssociation in xAssociations)
            {
                var associationEnds = this.xmiWrapper.GetAssociationEnds(xAssociation);

                var aggregationKind = associationEnds.Item1.OptionalAttributeValue("aggregation")
                                      ?? associationEnds.Item2.OptionalAttributeValue("aggregation");

                if (aggregationKind == "composite")
                {
                    var ownerTypeAssociationProperty =
                        string.IsNullOrWhiteSpace(associationEnds.Item1.OptionalAttributeValue("aggregation"))
                            ? associationEnds.Item2
                            : associationEnds.Item1;

                    this.AddCompositionNavigationalProperty(ownerTypeAssociationProperty);

                    var ownedTypeAssociationProperty = associationEnds.Item1.Equals(ownerTypeAssociationProperty)
                        ? associationEnds.Item2
                        : associationEnds.Item1;

                    this.AddCompositionNavigationalProperty(ownedTypeAssociationProperty);

                    var ownerTypeId = this.xmiWrapper.GetElementsId(ownerTypeAssociationProperty.Parent);

                    var ownerType = this.types.Single(x => x.XmiID == ownerTypeId);

                    var ownedTypeId = this.xmiWrapper.GetElementsId(ownedTypeAssociationProperty.Parent);

                    var ownedType = this.types.Single(x => x.XmiID == ownedTypeId);

                    foreach (var ownersID in ownerType.IDs)
                    {
                        ownedType.ForeignKeys.Add(ownerType.Name + ownersID.Name, ownersID);
                    }
                }

                var relationship = this.relationshipFactory.Create(xAssociation, this.types);
                this.relationships.Add(relationship);
            }
        }

        private void AddCompositionNavigationalProperty(XElement associationProperty)
        {
            var typeId = this.xmiWrapper.GetElementsId(associationProperty.Parent);

            var type = this.types.Single(x => x.XmiID == typeId);

            var navigationalProperty = this.GenerateAttribute(type, associationProperty);

            navigationalProperty.IsVirtual = true;

            type.Members.Add(navigationalProperty);
        }

        private void GenerateInheritanceRelations(IEnumerable<XElement> xTypes)
        {
            foreach (var type in xTypes)
            {
                //klasa bazowa
                var xCurrClassGeneralization = this.xmiWrapper.GetXClassGeneralization(type);
                if (xCurrClassGeneralization != null)
                {
                    var baseClassId = xCurrClassGeneralization.ObligatoryAttributeValue("general");

                    var baseClassXElement = this.xmiWrapper.GetXElementById(baseClassId);

                    var baseClassName = baseClassXElement.ObligatoryAttributeValue("name");

                    var baseClass = this.types.FirstOrDefault(i => i.Name == baseClassName);

                    Insist.IsNotNull(baseClass, nameof(baseClass));
                    var ctr = new CodeTypeReference(baseClass.Name);

                    var currClassName = type.ObligatoryAttributeValue("name");
                    var currClass = this.types.FirstOrDefault(i => i.Name == currClassName);

                    Insist.IsNotNull(currClass, nameof(currClass));
                    currClass.BaseTypes.Add(ctr);
                }
            }
        }

        private void DeclareType(XElement xType)
        {
            var typeDeclaration = new ExtendedCodeTypeDeclaration (xType.ObligatoryAttributeValue("name"))
            {
                XmiID = this.xmiWrapper.GetElementsId(xType)
            };
            this.typeDeclarations.Add(typeDeclaration);
        }

        private ExtendedCodeTypeDeclaration BuildType(XElement type)
        {
            var codeTypeDeclarationName = type.ObligatoryAttributeValue("name");
            var codeTypeDeclaration = this.typeDeclarations.Single(t => t.Name.Equals(codeTypeDeclarationName));

            codeTypeDeclaration.IsClass = this.umlTypesHelper.IsClass(type);
            if (!codeTypeDeclaration.IsClass)
            {
                codeTypeDeclaration.IsStruct = this.umlTypesHelper.IsStruct(type);
                if (codeTypeDeclaration.IsStruct)
                {
                    throw new Exception("Undandled type node:\n" + type);
                }
            }

            codeTypeDeclaration.TypeAttributes = TypeAttributes.Public;                    
            
            if (this.umlTypesHelper.IsAbstract(type))
            {
                codeTypeDeclaration.TypeAttributes = codeTypeDeclaration.TypeAttributes | TypeAttributes.Abstract;
            }

            var nestedClasses = type.Descendants("nestedClassifier").ToList();
            foreach (var nestedClass in nestedClasses)
            {
                var ctdNested = this.BuildType(nestedClass);
                codeTypeDeclaration.Members.Add(ctdNested);
            }

            this.GenerateAttributes(type, codeTypeDeclaration);

            this.GenerateMethods(type, codeTypeDeclaration);

            return codeTypeDeclaration;
        }

        private void GenerateMethods(XElement type, CodeTypeDeclaration codeTypeDeclaration)
        {
            var operations = this.xmiWrapper.GetXOperations(type);
            foreach (var operation in operations)
            {
                var codeMemberMethod = new CodeMemberMethod {Name = operation.ObligatoryAttributeValue("name")};

                //returned type
                var returnParameter = this.xmiWrapper.GetXReturnParameter(operation);
                var returnType = this.umlTypesHelper.GetXElementCsharpType(returnParameter);
                CodeTypeReference ctr = ExtendedCodeTypeReference.CreateForType(returnType);
                codeMemberMethod.ReturnType = ctr;


                //parameters
                var parameters = this.xmiWrapper.GetXParameters(operation);
                foreach (var xParameter in parameters)
                {
                    var parType = this.umlTypesHelper.GetXElementCsharpType(xParameter);
                    var parName = xParameter.ObligatoryAttributeValue("name");
                    CodeParameterDeclarationExpression parameter =
                        new ExtendedCodeParameterDeclarationExpression(parType, parName);
                    codeMemberMethod.Parameters.Add(parameter);
                }

                //visibility
                var umlVisibility = operation.ObligatoryAttributeValue("visibility");
                var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharp(umlVisibility);
                codeMemberMethod.Attributes = codeMemberMethod.Attributes | cSharpVisibility;

                var isStatic = Convert.ToBoolean(operation.OptionalAttributeValue("isStatic"));
                if (isStatic)
                {
                    codeMemberMethod.Attributes = codeMemberMethod.Attributes | MemberAttributes.Static;
                }

                codeTypeDeclaration.Members.Add(codeMemberMethod);
            }
        }

        private void GenerateAttributes(XElement type, ExtendedCodeTypeDeclaration codeTypeDeclaration)
        {
            var attributes = this.xmiWrapper
                .GetXAttributes(type)
                .Where(a => string.IsNullOrWhiteSpace(a.OptionalAttributeValue("association")));
            foreach (var attribute in attributes)
            {
                var codeMemberProperty = this.GenerateAttribute(codeTypeDeclaration, attribute);
                codeTypeDeclaration.Members.Add(codeMemberProperty);
            }
        }

        private ExtendedCodeMemberProperty GenerateAttribute(ExtendedCodeTypeDeclaration codeTypeDeclaration, XElement attribute)
        {
            Insist.IsNotNull(attribute, nameof(attribute));

            //type                
            var cSharpType = this.umlTypesHelper.GetXElementCsharpType(attribute);
            CodeTypeReference typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

            //declaration
            var codeMemberProperty = new ExtendedCodeMemberProperty
            {
                Type = typeRef,
                Name = this.attributeNameResolver.GetName(attribute),
                HasSet = true
            };

            var umlVisibility = attribute.ObligatoryAttributeValue("visibility");
            var cSharpVisibility = this.umlVisibilityMapper.UmlToCsharp(umlVisibility);
            codeMemberProperty.Attributes = codeMemberProperty.Attributes | cSharpVisibility;

            var isStatic = Convert.ToBoolean(attribute.OptionalAttributeValue("isStatic"));
            if (isStatic)
            {
                codeMemberProperty.Attributes = codeMemberProperty.Attributes | MemberAttributes.Static;
            }

            var xIsReadonly = Convert.ToBoolean(attribute.OptionalAttributeValue("isReadOnly"));
            if (xIsReadonly)
            {
                codeMemberProperty.HasSet = false;
            }

            var xDefaultValue = attribute.Element("defaultValue");
            if (xDefaultValue != null)
            {
                var extendedType = (ExtendedCodeTypeReference) codeMemberProperty.Type;
                if (extendedType.IsGeneric || extendedType.IsNamedType)
                {
                    throw new NotSupportedException("No default value for generic or declared named types supported");
                }

                codeMemberProperty.DefaultValueString = xDefaultValue.ObligatoryAttributeValue("value");
            }

            var xIsDerived = Convert.ToBoolean(attribute.OptionalAttributeValue("isDerived"));
            if (xIsDerived)
            {
                codeMemberProperty.HasSet = false;
                codeMemberProperty.IsDerived = true;
            }

            var xIsID = Convert.ToBoolean(attribute.OptionalAttributeValue("isID"));
            if (xIsID)
            {
                codeMemberProperty.IsID = true;
                codeTypeDeclaration.IDs.Add(codeMemberProperty);
            }

            return codeMemberProperty;
        }
    }
}