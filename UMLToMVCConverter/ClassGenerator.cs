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

    internal class ClassGenerator
    {
        private readonly string namespaceName;
        private readonly List<CodeTypeDeclaration> types;
        private readonly string outputPath;
        private readonly List<CodeTypeDeclaration> typeDeclarations;
        private readonly XmiWrapper xmiWrapper;
        private readonly UmlTypesHelper umlTypesHelper;

        public ClassGenerator(string xmiPath, string outputPath)
        {
            Insist.IsNotNullOrWhiteSpace(outputPath, nameof(outputPath));
            this.outputPath = outputPath;

            var xdoc = XDocument.Load(xmiPath);
            Insist.IsNotNull(xdoc.Root, nameof(xdoc.Root));

            var xmiNamespace = xdoc.Root.GetNamespaceOfPrefix("xmi");
            Insist.IsNotNull(xmiNamespace, nameof(xmiNamespace));

            var umlNamespace = xdoc.Root.GetNamespaceOfPrefix("uml");
            Insist.IsNotNull(umlNamespace, nameof(umlNamespace));

            var attributeComparer = new AttributeEqualityComparer();

            this.xmiWrapper = new XmiWrapper(xdoc, xmiNamespace, umlNamespace, attributeComparer);
            this.umlTypesHelper = new UmlTypesHelper(this.xmiWrapper, this.types);

            this.namespaceName = "Test";
            //TODO: póki co nie wiadomo czy i jak używać przestrzeni nazw - robię listę typów => pakiety z UML
            this.types = new List<CodeTypeDeclaration>();
            this.typeDeclarations = new List<CodeTypeDeclaration>();
        }
        public string GenerateTypes()
        {
            var umlModels = this.xmiWrapper.GetXUmlModels();
            foreach (var umlModel in umlModels)
            {
                var xTypes = this.xmiWrapper.GetXTypes(umlModel).ToList();
                foreach (var type in xTypes)
                {
                    this.DeclareTypeFromXElement(type);
                }

                foreach (var type in xTypes)
                {
                    var ctd = this.BuildTypeFromXElement(type);
                    this.types.Add(ctd);                                                                               
                }
                
                //dziedziczenie

                //po utworzeniu obiektów w liście typów, dla każdej klasy sprawdzam czy po czyms dziedziczy
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

            //generowanie plików
            this.GenerateFiles(this.types, this.namespaceName);

            return "File successfully processed";
        }

        private void DeclareTypeFromXElement(XElement xType)
        {
            //deklaracja typu
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

            //TODO: UML type visibility other than public
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

            #region attributes & methods          

            //attributes
            var attributes = this.xmiWrapper.GetXAttributes(type);
            foreach (var attribute in attributes)
            {
                Insist.IsNotNull(attribute, nameof(attribute));

                //attribute type                
                var cSharpType = this.umlTypesHelper.GetXElementCsharpType(attribute);
                CodeTypeReference typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

                //attribute declaration
                var codeMemberProperty = new ExtendedCodeMemberProperty()
                {
                    Type = typeRef,
                    Name = attribute.ObligatoryAttributeValue("name")
                };

                //widoczność
                var umlVisibility = attribute.ObligatoryAttributeValue("visibility");
                var cSharpVisibility = UmlVisibilityMapper.UmlToCsharp(umlVisibility);
                codeMemberProperty.Attributes = cSharpVisibility;    
           
                //czy statyczny?
                var isStatic = Convert.ToBoolean(attribute.OptionalAttributeValue("isStatic"));
                if (isStatic)
                {
                    codeMemberProperty.Attributes = codeMemberProperty.Attributes | MemberAttributes.Static;
                }

                //czy tylko do odczytu
                var xIsReadonly = Convert.ToBoolean(attribute.OptionalAttributeValue("isReadOnly"));
                codeMemberProperty.HasSet = !xIsReadonly;

                //default value
                var xDefaultValue = attribute.Element("defaultValue");
                if (xDefaultValue != null)
                {
                    var extendedType = (ExtendedCodeTypeReference)codeMemberProperty.Type;
                    if (extendedType.IsGeneric || extendedType.IsNametType)
                    {
                        throw new NotSupportedException("No default value for generic or declared named types supported");
                    }

                    codeMemberProperty.DefaultValueString = xDefaultValue.ObligatoryAttributeValue("value");
                }

                codeTypeDeclaration.Members.Add(codeMemberProperty);
            }

            //metody
            var operations = this.xmiWrapper.GetXOperations(type);
            foreach (var operation in operations)
            {
                var codeMemberMethod = new CodeMemberMethod {Name = operation.ObligatoryAttributeValue("name")};

                //typ zwracany
                var returnParameter = this.xmiWrapper.GetXReturnParameter(operation);
                var returnType = this.umlTypesHelper.GetXElementCsharpType(returnParameter);
                CodeTypeReference ctr = ExtendedCodeTypeReference.CreateForType(returnType);
                codeMemberMethod.ReturnType = ctr;
                            

                //parametry wejściowe
                var parameters = this.xmiWrapper.GetXParameters(operation);
                foreach(var xParameter in parameters) {
                    var parType = this.umlTypesHelper.GetXElementCsharpType(xParameter);
                    var parName = xParameter.ObligatoryAttributeValue("name");
                    CodeParameterDeclarationExpression parameter = new ExtendedCodeParameterDeclarationExpression(parType, parName);
                    codeMemberMethod.Parameters.Add(parameter);
                }

                //widoczność
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

            #endregion

            return codeTypeDeclaration;
        }

        private ExtendedType GetXElementCsharpType(XElement xElement)
        {
            
        }

        private XElement GetElementById(string innerType)
        {
            return this.xdoc.Descendants()
                .SingleOrDefault(e => innerType.Equals(e.OptionalAttributeValue(this.xmiNamespace + "id")));
        }

        private void GenerateFiles(List<CodeTypeDeclaration> typesToGenerate, string namespaceNameToGenerate)
        {
            //dla każdej klasy generowanie klasy modeli
            this.GenerateModels(typesToGenerate, namespaceNameToGenerate);

            /*tylko niektóre typy będą niezależnymi obiektami, nie będą nimi:
             * - klasy abstrakcyjne
             * - typy wartościowe
            */
            var standaloneEntityTypes = typesToGenerate.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                    && !i.IsStruct)
                .ToList();
            //generowanie pliku kontekstu (DbContext)
            this.GenerateDbContextClass(standaloneEntityTypes, namespaceNameToGenerate);
            //generowanie plików konrolerów
            this.GenerateControllers(standaloneEntityTypes, namespaceNameToGenerate);
            //generowanie widoków
            this.GenerateViews(standaloneEntityTypes, namespaceNameToGenerate);
        }


        private void GenerateDbContextClass(List<CodeTypeDeclaration> classes, string contextName)
        {
            var tmpl = new DbContextTextTemplate(classes, contextName);
            var output = tmpl.TransformText();
            Directory.CreateDirectory(Path.Combine(this.outputPath, "DAL"));
            var filesSubPath = @"DAL\" + contextName + "Context.cs";
            var filesOutputPath = Path.Combine(this.outputPath, filesSubPath);
            File.WriteAllText(filesOutputPath, output);
        }

        private void GenerateControllers(List<CodeTypeDeclaration> classes, string contextName)
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

        private void GenerateViews(List<CodeTypeDeclaration> classes, string contextName)
        {
            //widoki - listy
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

        private void GenerateModels(List<CodeTypeDeclaration> classes, string contextName)
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
    }
}