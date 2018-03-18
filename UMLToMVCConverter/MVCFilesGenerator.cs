namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using System.CodeDom;
    using System.IO;
    using System.Reflection;
    using UMLToMVCConverter.CodeTemplates;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.Mappers;

    internal class MvcFilesGenerator
    {
        private const string TemporaryNamespaceName = "Default";
        private readonly string namespaceName;
        private readonly List<CodeTypeDeclaration> types;
        private readonly string outputPath;
        private readonly List<CodeTypeDeclaration> typeDeclarations;
        private readonly XmiWrapper xmiWrapper;
        private readonly UmlTypesHelper umlTypesHelper;

        public MvcFilesGenerator(string xmiPath, string outputPath)
        {
            Insist.IsNotNullOrWhiteSpace(outputPath, nameof(outputPath));
            this.outputPath = outputPath;

            var xdoc = XDocument.Load(xmiPath);
            Insist.IsNotNull(xdoc.Root, nameof(xdoc.Root));

            var xmiNamespace = xdoc.Root.GetNamespaceOfPrefix("xmi");
            Insist.IsNotNull(xmiNamespace, nameof(xmiNamespace));

            var umlNamespace = xdoc.Root.GetNamespaceOfPrefix("uml");
            Insist.IsNotNull(umlNamespace, nameof(umlNamespace));

            this.types = new List<CodeTypeDeclaration>();
            this.typeDeclarations = new List<CodeTypeDeclaration>();

            var attributeComparer = new AttributeEqualityComparer();

            this.xmiWrapper = new XmiWrapper(xdoc, xmiNamespace, umlNamespace, attributeComparer);
            this.umlTypesHelper = new UmlTypesHelper(this.xmiWrapper, this.types);

            this.namespaceName = TemporaryNamespaceName;
        }
        public string GenerateMvcFiles()
        {
            this.ClearOutputDirectory();

            var umlModels = this.xmiWrapper.GetXUmlModels();
            foreach (var umlModel in umlModels)
            {
                var xTypes = this.xmiWrapper.GetXTypes(umlModel)
                    .ToList();

                var xTypesToBuild = xTypes
                    .Where(t => !"nestedClassifier".Equals(t.Name.ToString()));

                foreach (var type in xTypes)
                {
                    this.DeclareTypeFromXElement(type);
                }

                foreach (var type in xTypesToBuild)
                {
                    var codeTypeDeclaration = this.BuildTypeFromXElement(type);
                    this.types.Add(codeTypeDeclaration);                                                                               
                }

                this.GenerateInheritanceRelations(xTypes);
            }

            this.GenerateFiles(this.types, this.namespaceName);

            return "File successfully processed";
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

        private void DeclareTypeFromXElement(XElement xType)
        {
            var typeDeclaration = new CodeTypeDeclaration {Name = xType.ObligatoryAttributeValue("name")};
            this.typeDeclarations.Add(typeDeclaration);
        }

        private CodeTypeDeclaration BuildTypeFromXElement(XElement type)
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
                var ctdNested = this.BuildTypeFromXElement(nestedClass);
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
                var cSharpVisibility = UmlVisibilityMapper.UmlToCsharp(umlVisibility);
                codeMemberMethod.Attributes = codeMemberMethod.Attributes | cSharpVisibility;

                var isStatic = Convert.ToBoolean(operation.OptionalAttributeValue("isStatic"));
                if (isStatic)
                {
                    codeMemberMethod.Attributes = codeMemberMethod.Attributes | MemberAttributes.Static;
                }

                codeTypeDeclaration.Members.Add(codeMemberMethod);
            }
        }

        private void GenerateAttributes(XElement type, CodeTypeDeclaration codeTypeDeclaration)
        {
            var attributes = this.xmiWrapper.GetXAttributes(type);
            foreach (var attribute in attributes)
            {
                Insist.IsNotNull(attribute, nameof(attribute));

                //type                
                var cSharpType = this.umlTypesHelper.GetXElementCsharpType(attribute);
                CodeTypeReference typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

                //declaration
                var codeMemberProperty = new ExtendedCodeMemberProperty
                {
                    Type = typeRef,
                    Name = attribute.ObligatoryAttributeValue("name").FirstCharToUpper()
                };

                var umlVisibility = attribute.ObligatoryAttributeValue("visibility");
                var cSharpVisibility = UmlVisibilityMapper.UmlToCsharp(umlVisibility);
                codeMemberProperty.Attributes = cSharpVisibility;

                var isStatic = Convert.ToBoolean(attribute.OptionalAttributeValue("isStatic"));
                if (isStatic)
                {
                    codeMemberProperty.Attributes = codeMemberProperty.Attributes | MemberAttributes.Static;
                }

                var xIsReadonly = Convert.ToBoolean(attribute.OptionalAttributeValue("isReadOnly"));
                codeMemberProperty.HasSet = !xIsReadonly;

                var xDefaultValue = attribute.Element("defaultValue");
                if (xDefaultValue != null)
                {
                    var extendedType = (ExtendedCodeTypeReference) codeMemberProperty.Type;
                    if (extendedType.IsGeneric || extendedType.IsNametType)
                    {
                        throw new NotSupportedException("No default value for generic or declared named types supported");
                    }

                    codeMemberProperty.DefaultValueString = xDefaultValue.ObligatoryAttributeValue("value");
                }

                codeTypeDeclaration.Members.Add(codeMemberProperty);
            }
        }

        private void GenerateFiles(List<CodeTypeDeclaration> typesToGenerate, string namespaceNameToGenerate)
        {
            this.GenerateModels(typesToGenerate, namespaceNameToGenerate);

            var standaloneEntityTypes = typesToGenerate.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                    && !i.IsStruct)
                .ToList();

            this.GenerateDbContextClass(standaloneEntityTypes, namespaceNameToGenerate);

            this.GenerateControllers(standaloneEntityTypes, namespaceNameToGenerate);

            this.GenerateViews(standaloneEntityTypes, namespaceNameToGenerate);
        }

        private void GenerateDbContextClass(List<CodeTypeDeclaration> classes, string contextName)
        {
            var tmpl = new DbContextTextTemplate(classes, contextName);
            var output = tmpl.TransformText();
            Directory.CreateDirectory(Path.Combine(this.outputPath, "Models"));
            var fileSubPath = @"Models\" + contextName + "Context.cs";
            var fileOutputPath = Path.Combine(this.outputPath, fileSubPath);
            File.WriteAllText(fileOutputPath, output);
        }

        private void GenerateControllers(IEnumerable<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (var ctd in classes)
            {
                var tmpl = new ControllerTextTemplate(ctd, contextName);
                var output = tmpl.TransformText();
                Directory.CreateDirectory(Path.Combine(this.outputPath, "Controllers"));
                var filesSubPath = @"Controllers\" + ctd.Name + "Controller.cs";
                var filesOutputPath = Path.Combine(this.outputPath, filesSubPath);
                File.WriteAllText(filesOutputPath, output);

            }            
        }

        private void GenerateViews(IEnumerable<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (var ctd in classes)
            {
                var tmpl = new ViewIndexTextTemplate(ctd, contextName);
                var output = tmpl.TransformText();
                Directory.CreateDirectory(Path.Combine(this.outputPath, @"Views\" +ctd.Name));
                var filesSubPath = @"Views\" + ctd.Name + @"\Index.cshtml";
                var filesOutputPath = Path.Combine(this.outputPath, filesSubPath);
                File.WriteAllText(filesOutputPath, output);

            }
        }

        private void GenerateModels(IEnumerable<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (var ctd in classes)
            {
                var tmpl = new ModelClassTextTemplate(ctd, contextName);
                var output = tmpl.TransformText();
                Directory.CreateDirectory(Path.Combine(this.outputPath, "Models"));
                var filesSubPath = @"Models\" + ctd.Name + ".cs";
                var filesOutputPath = Path.Combine(this.outputPath, filesSubPath);
                File.WriteAllText(filesOutputPath, output);
            }
        }

        private void ClearOutputDirectory()
        {
            ClearFolder(this.outputPath);
        }

        private void ClearFolder(string path)
        {
            var directory = new DirectoryInfo(path);

            foreach (var file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (var subDirectory in directory.GetDirectories())
            {
                ClearFolder(subDirectory.FullName);
                subDirectory.Delete();
            }
        }
    }
}