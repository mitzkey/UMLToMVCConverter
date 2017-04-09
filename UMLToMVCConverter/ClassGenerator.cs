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
        public static string GenerateClassesFromXmi(string xmiPath)
        {
            string namespaceName = "Test";

            //ustawiamy zmienne potrzebne do generowania kodu
            //TODO: na razie każda klasa w osobnym pliku, docelowo możemy mieć klasę zagnieżdżoną np.
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";

            //TODO: póki co nie wiadomo czy i jak używać przestrzeni nazw - robię listę typów => pakiety z UML
            List<CodeTypeDeclaration> types = new List<CodeTypeDeclaration>();

            MyAttributeEqualityComparer attributeComparer = new MyAttributeEqualityComparer();

            XDocument xdoc = XDocument.Load(xmiPath);
            XNamespace xmi = xdoc.Root.GetNamespaceOfPrefix("xmi");
            XNamespace uml = xdoc.Root.GetNamespaceOfPrefix("uml");

            //dla każdego modelu <uml:Model>
            List<XElement> umlModels = xdoc.Descendants(uml + "Model").ToList();
            foreach (XElement umlModel in umlModels)
            {
                //dla każdej klasy <packagedElement xmi:type='uml:Class'>:
                List<XElement> classes = umlModel.Descendants("packagedElement")
                    .Where(i => i.Attributes().
                        Contains(new XAttribute(xmi + "type", "uml:Class"), attributeComparer))
                    .ToList();
                foreach (XElement _class in classes)
                {                   
                    //tworzymy klase

                    //TODO: namespace, też nie wiem jak w kontekście UML
                    CodeNamespace cns = new CodeNamespace(namespaceName);

                    //TODO: tworzymy wpisy 'using' - na razie nie wiem jak to może wyglądać od strony UML
                    cns.Imports.Add(new CodeNamespaceImport("System"));
                   
                    //deklaracja klasy
                    CodeTypeDeclaration ctd = new CodeTypeDeclaration();
                    ctd.IsClass = true;
                    ctd.Name = _class.Attribute("name").Value;
                    ctd.TypeAttributes = TypeAttributes.Public; //TODO: czy w UML mamy widoczność klasy?                    
                    cns.Types.Add(ctd);
                    targetUnit.Namespaces.Add(cns);
                    types.Add(ctd);

                    #region pola

                    //pole "ID" do utworzenia klucza głównego relacji przez EF
                    CodeMemberField id_field = new CodeMemberField(typeof(int), ctd.Name + "ID");
                    ctd.Members.Add(id_field);

                    List<XElement> fields = _class.Descendants("ownedAttribute")
                            .Where(i => i.Attributes()
                                .Contains(new XAttribute(xmi + "type", "uml:Property"), attributeComparer))
                            .ToList();
                        foreach (XElement field in fields)
                        {
                            //określenie typu pola
                            //TODO: i tu mamy problem z odczytaniem typu - jak odczytywać typy z xmi? poniższy odczyt jest dość 'dziki' => odczytujemy tylko 5 podstawowych
                            XElement xType = field.Descendants("type").FirstOrDefault();
                            XElement xExtension = xType.Descendants(xmi + "Extension").FirstOrDefault();
                            XElement xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
                            string UMLtype = xRefExtension.Attribute("referentPath").Value.Split(new char[]{':',':'}).Last();
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
                }
                
                //dziedziczenie

                //dla każdej klasy sprawdzam czy po czyms dziedziczy
                foreach (XElement _class in classes)
                {
                    //klasa bazowa
                    //TODO: trzeba coś ustalić ws. dziedziczenia wielokrotnego => zadanie na tablicy
                    XElement xCurrClass = _class.Descendants("generalization")
                        .Where(i => i.Attributes()
                            .Contains(new XAttribute(xmi + "type", "uml:Generalization"), attributeComparer))
                        .FirstOrDefault();
                    if (xCurrClass != null)
                    {
                        string base_class_id = xCurrClass.Attribute("general").Value;
                        string base_class_name = classes.Where(i => i.Attribute(xmi + "id").Value.Equals(base_class_id)).First().Attribute("name").Value;
                        string curr_class_name = _class.Attribute("name").Value;
                        CodeTypeDeclaration currClass = types.Where(i => i.Name == curr_class_name).FirstOrDefault();
                        CodeTypeDeclaration baseClass = types.Where(i => i.Name == base_class_name).FirstOrDefault();
                        CodeTypeReference ctr = new CodeTypeReference(baseClass.Name);
                        currClass.BaseTypes.Add(ctr);
                    }
                }

                //generowanie pliku kontekstu (DbContext)
                GenerateDbContextClass(types, namespaceName);

                //generowanie plików konrolerów
                GenerateControllers(types, namespaceName);

                //generowanie widoków
                GenerateViews(types, namespaceName);

                //generowanie klas modelów
                GenerateModels(types, namespaceName);
            }

            return "Plik przetworzono pomyślnie";
        }


        public static void GenerateDbContextClass(List<CodeTypeDeclaration> classes, string contextName)
        {
            DbContextTextTemplate tmpl = new DbContextTextTemplate(classes, contextName);
            string output = tmpl.TransformText();
            Directory.CreateDirectory("DAL");
            File.WriteAllText(@"DAL\"+contextName + "Context.cs", output);
        }

        public static void GenerateControllers(List<CodeTypeDeclaration> classes, string contextName)
        {
            foreach (CodeTypeDeclaration ctd in classes)
            {
                ControllerTextTemplate tmpl = new ControllerTextTemplate(ctd, contextName);
                string output = tmpl.TransformText();
                Directory.CreateDirectory("Controllers");
                File.WriteAllText(@"Controllers\" + ctd.Name + "Controller.cs", output);

            }            
        }

        public static void GenerateViews(List<CodeTypeDeclaration> classes, string contextName)
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

        public static void GenerateModels(List<CodeTypeDeclaration> classes, string contextName)
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