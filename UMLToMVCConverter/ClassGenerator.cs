using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.CodeDom;
using System.IO;
using System.Reflection;
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using UMLToMVCConverter.CodeTemplates;

namespace UMLToMVCConverter
{
    class ClassGenerator
    {
        XDocument xdoc;
        XNamespace xmi_namespace;
        XNamespace uml_namespace;
        MyAttributeEqualityComparer attributeComparer;
        string namespaceName;
        List<CodeTypeDeclaration> types;

        public ClassGenerator(string xmiPath)
        {
            xdoc = XDocument.Load(xmiPath);
            xmi_namespace = xdoc.Root.GetNamespaceOfPrefix("xmi");
            uml_namespace = xdoc.Root.GetNamespaceOfPrefix("uml");
            attributeComparer = new MyAttributeEqualityComparer();
            namespaceName = "Test";
            //TODO: póki co nie wiadomo czy i jak używać przestrzeni nazw - robię listę typów => pakiety z UML
            types = new List<CodeTypeDeclaration>();
        }
        public string GenerateClasses()
        {                                             
            //dla każdego modelu <uml:Model>
            List<XElement> umlModels = xdoc.Descendants(uml_namespace + "Model").ToList();
            foreach (XElement umlModel in umlModels)
            {
                //dla każdej klasy <packagedElement xmi:type='uml:Class'>:
                List<XElement> classes = umlModel.Descendants("packagedElement")
                    .Where(i => i.Attributes().
                        Contains(new XAttribute(xmi_namespace + "type", "uml:Class"), attributeComparer))
                    .ToList();

                foreach (XElement _class in classes)
                {
                    //deklaracja klasy
                    CodeTypeDeclaration ctd = GenerateClassFromXElement(_class);
                    types.Add(ctd);                                                                               
                }
                
                //dziedziczenie

                //po utworzeniu obiektów w liście typów, dla każdej klasy sprawdzam czy po czyms dziedziczy
                foreach (XElement _class in classes)
                {
                    //klasa bazowa
                    //TODO: trzeba coś ustalić ws. dziedziczenia wielokrotnego => zadanie na tablicy
                    XElement xCurrClass = _class.Descendants("generalization")
                        .Where(i => i.Attributes()
                            .Contains(new XAttribute(xmi_namespace + "type", "uml:Generalization"), attributeComparer))
                        .FirstOrDefault();
                    if (xCurrClass != null)
                    {
                        string base_class_id = xCurrClass.Attribute("general").Value;
                        string base_class_name = classes.Where(i => i.Attribute(xmi_namespace + "id").Value.Equals(base_class_id)).First().Attribute("name").Value;
                        string curr_class_name = _class.Attribute("name").Value;
                        CodeTypeDeclaration currClass = types.Where(i => i.Name == curr_class_name).FirstOrDefault();
                        CodeTypeDeclaration baseClass = types.Where(i => i.Name == base_class_name).FirstOrDefault();
                        CodeTypeReference ctr = new CodeTypeReference(baseClass.Name);
                        currClass.BaseTypes.Add(ctr);
                    }
                }

                
            }

            //generowanie plików
            GenerateFiles(types, namespaceName);

            return "Plik przetworzono pomyślnie";
        }

        private CodeTypeDeclaration GenerateClassFromXElement(XElement _class)
        {
            //deklaracja klasy
            CodeTypeDeclaration ctd = new CodeTypeDeclaration();
            ctd.IsClass = true;
            ctd.Name = _class.Attribute("name").Value;
            ctd.TypeAttributes = TypeAttributes.Public; //TODO: czy w UML mamy widoczność klasy?                    
            

            //czy jest abstrakcyjna
            if (_class.Attribute("isAbstract") != null && _class.Attribute("isAbstract").Value == "true")
            {
                ctd.TypeAttributes = ctd.TypeAttributes | TypeAttributes.Abstract;
            }

            //czy są klasy zagnieżdżone
            List<XElement> nestedClasses = _class.Descendants("nestedClassifier").ToList();
            foreach (XElement nestedClass in nestedClasses)
            {
                CodeTypeDeclaration ctd_nested = GenerateClassFromXElement(nestedClass);
                ctd.Members.Add(ctd_nested);
            }

            #region pola            

            List<XElement> fields = _class.Descendants("ownedAttribute")
                    .Where(i => i.Attributes()
                        .Contains(new XAttribute(xmi_namespace + "type", "uml:Property"), attributeComparer))
                    .ToList();
            foreach (XElement field in fields)
            {
                //określenie typu pola
                //TODO: i tu mamy problem z odczytaniem typu - jak odczytywać typy z xmi? poniższy odczyt jest dość 'dziki' => odczytujemy tylko 5 podstawowych
                XElement xType = field.Descendants("type").FirstOrDefault();
                XElement xExtension = xType.Descendants(xmi_namespace + "Extension").FirstOrDefault();
                XElement xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
                string UMLtype = xRefExtension.Attribute("referentPath").Value.Split(new char[] { ':', ':' }).Last();
                Type cSharpType = UMLTypeMapper.UMLToCsharp(UMLtype);

                //deklaracja pola
                CodeMemberField cmf = new CodeMemberField(cSharpType, field.Attribute("name").Value);

                //widoczność
                string UMLvisibility = field.Attribute("visibility").Value;
                MemberAttributes cSharpVisibility = UMLVisibilityMapper.UMLToCsharp(UMLvisibility);
                cmf.Attributes = cSharpVisibility;
                ctd.Members.Add(cmf);
            }
            #endregion

            return ctd;
        }

        private void GenerateFiles(List<CodeTypeDeclaration> types, string namespaceName)
        {
            //dla każdej klasy generowanie klasy modeli
            GenerateModels(types, namespaceName);

            //dla każdej klasy nieabstrakcyjnej generowanie plików kontrolerów, widoków, wpisu w pliku kontekstu danych
            List<CodeTypeDeclaration> nonAbstractTypes = types.Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)).ToList();
            //generowanie pliku kontekstu (DbContext)
            GenerateDbContextClass(nonAbstractTypes, namespaceName);
            //generowanie plików konrolerów
            GenerateControllers(nonAbstractTypes, namespaceName);
            //generowanie widoków
            GenerateViews(nonAbstractTypes, namespaceName);
        }


        private void GenerateDbContextClass(List<CodeTypeDeclaration> classes, string contextName)
        {
            DbContextTextTemplate tmpl = new DbContextTextTemplate(classes, contextName);
            string output = tmpl.TransformText();
            Directory.CreateDirectory("DAL");
            File.WriteAllText(@"DAL\"+contextName + "Context.cs", output);
        }

        private void GenerateControllers(List<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (CodeTypeDeclaration ctd in classes)
            {
                ControllerTextTemplate tmpl = new ControllerTextTemplate(ctd, contextName);
                string output = tmpl.TransformText();
                Directory.CreateDirectory("Controllers");
                File.WriteAllText(@"Controllers\" + ctd.Name + "Controller.cs", output);

            }            
        }

        private void GenerateViews(List<CodeTypeDeclaration> classes, string contextName)
        {
            //widoki - listy
            foreach (CodeTypeDeclaration ctd in classes)
            {
                ViewIndexTextTemplate tmpl = new ViewIndexTextTemplate(ctd, contextName);
                string output = tmpl.TransformText();
                Directory.CreateDirectory(@"Views\"+ctd.Name);
                File.WriteAllText(@"Views\" + ctd.Name + @"\Index.cshtml", output);

            }
        }

        private void GenerateModels(List<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (CodeTypeDeclaration ctd in classes)
            {
                ModelClassTextTemplate tmpl = new ModelClassTextTemplate(ctd, contextName);
                string output = tmpl.TransformText();
                Directory.CreateDirectory(@"Models");
                File.WriteAllText(@"Models\" + ctd.Name + ".cs", output);
            }
        }
    }
}