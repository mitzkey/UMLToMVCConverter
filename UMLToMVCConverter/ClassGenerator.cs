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
        private readonly XDocument xdoc;
        private readonly XNamespace xmiNamespace;
        private readonly XNamespace umlNamespace;
        private readonly AttributeEqualityComparer attributeComparer;
        private readonly string namespaceName;
        private readonly List<CodeTypeDeclaration> types;
        private readonly string outputPath;
        private readonly List<CodeTypeDeclaration> typeDeclarations;

        public ClassGenerator(string xmiPath, string outputPath)
        {
            Insist.IsNotNullOrWhiteSpace(outputPath, nameof(outputPath));
            this.outputPath = outputPath;

            this.xdoc = XDocument.Load(xmiPath);
            Insist.IsNotNull(this.xdoc.Root, nameof(this.xdoc.Root));

            this.xmiNamespace = this.xdoc.Root.GetNamespaceOfPrefix("xmi");
            Insist.IsNotNull(this.xmiNamespace, nameof(this.xmiNamespace));

            this.umlNamespace = this.xdoc.Root.GetNamespaceOfPrefix("uml");
            Insist.IsNotNull(this.umlNamespace, nameof(this.umlNamespace));

            this.attributeComparer = new AttributeEqualityComparer();

            this.namespaceName = "Test";
            //TODO: póki co nie wiadomo czy i jak używać przestrzeni nazw - robię listę typów => pakiety z UML
            this.types = new List<CodeTypeDeclaration>();
            this.typeDeclarations = new List<CodeTypeDeclaration>();
        }
        public string GenerateTypes()
        {                                             
            //dla każdego modelu <uml:Model>
            var umlModels = this.xdoc.Descendants(this.umlNamespace + "Model").ToList();
            foreach (var umlModel in umlModels)
            {
                //dla każdej klasy <packagedElement xmi:type='uml:Class'>:
                var xTypes = umlModel.Descendants()
                    .Where(i => i.Attributes().
                        Contains(new XAttribute(this.xmiNamespace + "type", "uml:Class"), this.attributeComparer)
                        || i.Attributes().Contains(new XAttribute(this.xmiNamespace + "type", "uml:DataType"), this.attributeComparer))
                    .ToList();

                //deklaracja typów
                foreach (var type in xTypes)
                {
                    var typeDeclaration = this.DeclareTypeFromXElement(type);
                    this.typeDeclarations.Add(typeDeclaration);
                }

                foreach (var type in xTypes)
                {
                    //deklaracja klasy
                    var ctd = this.PopulateTypeFromXElement(type);
                    this.types.Add(ctd);                                                                               
                }
                
                //dziedziczenie

                //po utworzeniu obiektów w liście typów, dla każdej klasy sprawdzam czy po czyms dziedziczy
                foreach (var type in xTypes)
                {
                    //klasa bazowa
                    //TODO: trzeba coś ustalić ws. dziedziczenia wielokrotnego => zadanie na tablicy
                    var xCurrClass = type
                        .Descendants("generalization")
                        .FirstOrDefault(i => i.Attributes()
                            .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Generalization"), this.attributeComparer));
                    if (xCurrClass != null)
                    {
                        var baseClassId = xCurrClass.ObligatoryAttributeValue("general");

                        Insist.IsNotNull(this.xmiNamespace, nameof(this.xmiNamespace));
                        var baseClassName = xTypes
                            .First(i => i.Attribute(this.xmiNamespace + "id")
                            .Value
                            .Equals(baseClassId))
                            .ObligatoryAttributeValue("name");

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

            return "Plik przetworzono pomyślnie";
        }

        private CodeTypeDeclaration DeclareTypeFromXElement(XElement xType)
        {
            //deklaracja typu
            return new CodeTypeDeclaration {Name = xType.ObligatoryAttributeValue("name")};
        }

        private CodeTypeDeclaration PopulateTypeFromXElement(XElement type)
        {
            var codeTypeDeclarationName = type.ObligatoryAttributeValue("name");
            var codeTypeDeclaration = this.typeDeclarations.Single(t => t.Name.Equals(codeTypeDeclarationName));

            //jaki to typ
            var sType = type.ObligatoryAttributeValue(this.xmiNamespace + "type");
            switch (sType)
            {
                case "uml:Class":
                    codeTypeDeclaration.IsClass = true;
                    break;
                case "uml:DataType":
                    codeTypeDeclaration.IsStruct = true;
                    break;
                default:
                    throw new Exception("Nieobsługiwany typ: " + sType);
            }

            codeTypeDeclaration.TypeAttributes = TypeAttributes.Public; //TODO: czy w UML mamy widoczność typu?                    
            

            //czy jest abstrakcyjna
            if (type.Attribute("isAbstract")?.Value == "true")
            {
                codeTypeDeclaration.TypeAttributes = codeTypeDeclaration.TypeAttributes | TypeAttributes.Abstract;
            }

            //czy są klasy zagnieżdżone
            var nestedClasses = type.Descendants("nestedClassifier").ToList();
            foreach (var nestedClass in nestedClasses)
            {
                var ctdNested = this.PopulateTypeFromXElement(nestedClass);
                codeTypeDeclaration.Members.Add(ctdNested);
            }

            #region pola, metody          

            //pola
            var attributes = type.Descendants("ownedAttribute")
                    .Where(i => i.Attributes()
                        .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Property"), this.attributeComparer))
                    .ToList();
            foreach (var attribute in attributes)
            {
                Insist.IsNotNull(attribute, nameof(attribute));
                //liczności
                var lv = attribute.Descendants("lowerValue").SingleOrDefault();
                var lowerValue = lv?.Attribute("value")?.Value ?? string.Empty;
                var uv = attribute.Descendants("upperValue").SingleOrDefault();
                var upperValue = uv?.Attribute("value")?.Value ?? string.Empty;

                //określenie typu pola                
                var cSharpType = this.GetXElementCsharpType(attribute, lowerValue, upperValue);
                CodeTypeReference typeRef = ExtendedCodeTypeReference.CreateForType(cSharpType);

                //deklaracja pola
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
            var operations = type.Descendants("ownedOperation")
                    .Where(i => i.Attributes()
                        .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Operation"), this.attributeComparer))
                    .ToList();
            foreach (var operation in operations)
            {
                var codeMemberMethod = new CodeMemberMethod {Name = operation.ObligatoryAttributeValue("name")};

                //typ zwracany
                var returnParameter = operation.Descendants("ownedParameter").FirstOrDefault(i => i.ObligatoryAttributeValue("direction").Equals("return"));
                var returnType = this.GetXElementCsharpType(returnParameter, "", "");
                CodeTypeReference ctr = ExtendedCodeTypeReference.CreateForType(returnType);
                codeMemberMethod.ReturnType = ctr;
                            

                //parametry wejściowe
                var parameters = operation.Descendants("ownedParameter").Where(i => i.Attribute("direction") == null || !i.ObligatoryAttributeValue("direction").Equals("return")).ToList();
                foreach(var xParameter in parameters) {
                    var parType = this.GetXElementCsharpType(xParameter, "", "");
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

        private ExtendedType GetXElementCsharpType(XElement xElement, string mplLowerVal, string mplUpperVal)
        {
            Insist.IsNotNull(xElement, nameof(xElement));

            var innerType = xElement.OptionalAttributeValue("type");
            if (innerType == null)
            {
                var xType = xElement.Descendants("type").FirstOrDefault();
                Insist.IsNotNull(xType, nameof(xType));
                var xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
                var umlType = xRefExtension.ObligatoryAttributeValue("referentPath").Split(':', ':').Last();
                var cSharpType = UmlBasicTypesMapper.UmlToCsharp(umlType, mplLowerVal, mplUpperVal);
                return cSharpType;
            }
            else
            {
                var xReturnTypeElement = this.GetElementById(innerType);
                var typeName = xReturnTypeElement.ObligatoryAttributeValue("name");
                return new ExtendedType(typeName);
            }
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