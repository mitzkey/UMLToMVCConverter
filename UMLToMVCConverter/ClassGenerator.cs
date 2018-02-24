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

namespace UMLToMVCConverter
{
    internal class ClassGenerator
    {
        private readonly XDocument xdoc;
        private readonly XNamespace xmiNamespace;
        private readonly XNamespace umlNamespace;
        private readonly MyAttributeEqualityComparer attributeComparer;
        private readonly string namespaceName;
        private readonly List<CodeTypeDeclaration> types;

        public ClassGenerator(string xmiPath)
        {
            this.xdoc = XDocument.Load(xmiPath);
            Insist.IsNotNull(this.xdoc.Root, nameof(this.xdoc.Root));

            this.xmiNamespace = this.xdoc.Root.GetNamespaceOfPrefix("xmi");
            Insist.IsNotNull(this.xmiNamespace, nameof(this.xmiNamespace));

            this.umlNamespace = this.xdoc.Root.GetNamespaceOfPrefix("uml");
            Insist.IsNotNull(this.umlNamespace, nameof(this.umlNamespace));

            this.attributeComparer = new MyAttributeEqualityComparer();

            this.namespaceName = "Test";
            //TODO: póki co nie wiadomo czy i jak używać przestrzeni nazw - robię listę typów => pakiety z UML
            this.types = new List<CodeTypeDeclaration>();
        }
        public string GenerateTypes()
        {                                             
            //dla każdego modelu <uml:Model>
            var umlModels = this.xdoc.Descendants(this.umlNamespace + "Model").ToList();
            foreach (var umlModel in umlModels)
            {
                //dla każdej klasy <packagedElement xmi:type='uml:Class'>:
                var xTypes = umlModel.Descendants("packagedElement")
                    .Where(i => i.Attributes().
                        Contains(new XAttribute(this.xmiNamespace + "type", "uml:Class"), this.attributeComparer)
                        || i.Attributes().Contains(new XAttribute(this.xmiNamespace + "type", "uml:DataType"), this.attributeComparer))
                    .ToList();

                foreach (var type in xTypes)
                {
                    //deklaracja klasy
                    var ctd = this.GenerateTypeFromXElement(type);
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

        private CodeTypeDeclaration GenerateTypeFromXElement(XElement type)
        {
            //deklaracja typu
            var ctd = new CodeTypeDeclaration();

            //jaki to typ
            var sType = type.ObligatoryAttributeValue(this.xmiNamespace + "type");

            switch (sType) {
                case "uml:Class":
                    ctd.IsClass = true;
                    break;
                case "uml:DataType":
                    ctd.IsStruct = true;
                    break;
                default:
                    throw new Exception("Nieobsługiwany typ: " + sType);
            }
            
            ctd.Name = type.ObligatoryAttributeValue("name");
            ctd.TypeAttributes = TypeAttributes.Public; //TODO: czy w UML mamy widoczność typu?                    
            

            //czy jest abstrakcyjna
            if (type.Attribute("isAbstract")?.Value == "true")
            {
                ctd.TypeAttributes = ctd.TypeAttributes | TypeAttributes.Abstract;
            }

            //czy są klasy zagnieżdżone
            var nestedClasses = type.Descendants("nestedClassifier").ToList();
            foreach (var nestedClass in nestedClasses)
            {
                var ctdNested = this.GenerateTypeFromXElement(nestedClass);
                ctd.Members.Add(ctdNested);
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

                //deklaracja pola
                var cmp = new CodeMemberProperty();                
                CodeTypeReference typeRef = new ExtendedCodeTypeReference(cSharpType);
                cmp.Type = typeRef;
                cmp.Name = attribute.ObligatoryAttributeValue("name");

                //widoczność
                var umlVisibility = attribute.ObligatoryAttributeValue("visibility");
                var cSharpVisibility = UMLVisibilityMapper.UMLToCsharp(umlVisibility);
                cmp.Attributes = cSharpVisibility;    
           
                //czy statyczny?
                var xIsStatic = attribute.Attribute("isStatic");
                if (xIsStatic != null && xIsStatic.Value == "true")
                {
                    cmp.Attributes = cmp.Attributes | MemberAttributes.Static;                    
                }

                //czy tylko do odczytu
                var xIsReadonly = attribute.Attribute("isReadOnly");
                if (xIsReadonly != null && xIsReadonly.Value == "true") 
                {
                    cmp.HasSet = false;
                }
                else
                {
                    cmp.HasSet = true;
                }

                ctd.Members.Add(cmp);
            }

            //metody
            var operations = type.Descendants("ownedOperation")
                    .Where(i => i.Attributes()
                        .Contains(new XAttribute(this.xmiNamespace + "type", "uml:Operation"), this.attributeComparer))
                    .ToList();
            foreach (var operation in operations)
            {
                var cmm = new CodeMemberMethod {Name = operation.ObligatoryAttributeValue("name")};


                //typ zwracany
                var returnParameter = operation.Descendants("ownedParameter").FirstOrDefault(i => i.ObligatoryAttributeValue("direction").Equals("return"));
                if (returnParameter == null)
                {

                }
                var returnType = this.GetXElementCsharpType(returnParameter, "", "");
                if (returnType != null)
                {
                    CodeTypeReference ctr = new ExtendedCodeTypeReference(this.GetXElementCsharpType(returnParameter, "", ""));
                    cmm.ReturnType = ctr;
                }
                            

                //parametry wejściowe
                var parameters = operation.Descendants("ownedParameter").Where(i => i.Attribute("direction") == null || !i.ObligatoryAttributeValue("direction").Equals("return")).ToList();
                foreach(var xParameter in parameters) {
                    var parType = this.GetXElementCsharpType(xParameter, "", "");
                    var parName = xParameter.ObligatoryAttributeValue("name");
                    CodeParameterDeclarationExpression parameter = new ExtendedCodeParameterDeclarationExpression(parType, parName);
                    cmm.Parameters.Add(parameter);
                }

                //widoczność
                var umlVisibility = operation.ObligatoryAttributeValue("visibility");
                var cSharpVisibility = UMLVisibilityMapper.UMLToCsharp(umlVisibility);
                cmm.Attributes = cSharpVisibility;
                ctd.Members.Add(cmm);
            }

            #endregion

            return ctd;
        }

        private ExtendedType GetXElementCsharpType(XElement xElement, string mplLowerVal, string mplUpperVal)
        {
            var xType = xElement.Descendants("type").FirstOrDefault();
            if (xType == null)
            {
                return null;
            }
            var xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
            var umlType = xRefExtension.ObligatoryAttributeValue("referentPath").Split(':', ':').Last();
            var cSharpType = UMLTypeMapper.UMLToCsharp(umlType, mplLowerVal, mplUpperVal);
            return cSharpType;
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
            Directory.CreateDirectory("DAL");
            File.WriteAllText(@"DAL\"+contextName + "Context.cs", output);
        }

        private void GenerateControllers(List<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (var ctd in classes)
            {
                var tmpl = new ControllerTextTemplate(ctd, contextName);
                var output = tmpl.TransformText();
                Directory.CreateDirectory("Controllers");
                File.WriteAllText(@"Controllers\" + ctd.Name + "Controller.cs", output);

            }            
        }

        private void GenerateViews(List<CodeTypeDeclaration> classes, string contextName)
        {
            //widoki - listy
            foreach (var ctd in classes)
            {
                var tmpl = new ViewIndexTextTemplate(ctd, contextName);
                var output = tmpl.TransformText();
                Directory.CreateDirectory(@"Views\"+ctd.Name);
                File.WriteAllText(@"Views\" + ctd.Name + @"\Index.cshtml", output);

            }
        }

        private void GenerateModels(List<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (var ctd in classes)
            {
                var tmpl = new ModelClassTextTemplate(ctd, contextName);
                var output = tmpl.TransformText();
                Directory.CreateDirectory(@"Models");
                File.WriteAllText(@"Models\" + ctd.Name + ".cs", output);
            }
        }
    }
}