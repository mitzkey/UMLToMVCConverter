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

namespace UMLToMVCConverter
{
    class ClassGenerator
    {
        public static string GenerateClassesFromXmi(string xmiPath)
        {         
            //ustawiamy zmienne potrzebne do generowania kodu
            //TODO: na razie każda klasa w osobnym pliku, docelowo możemy mieć klasę zagnieżdżoną np.
            CodeCompileUnit targetUnit = new CodeCompileUnit();
            CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
            CodeGeneratorOptions options = new CodeGeneratorOptions();
            options.BracingStyle = "C";

            //TODO: póki co nie wiadomo czy i jak używać przestrzeni nazw - robię listę typów
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
                    CodeNamespace cns = new CodeNamespace("Test01");

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

                    //pola
                    List<XElement> fields = _class.Descendants("ownedAttribute")
                        .Where(i => i.Attributes()
                            .Contains(new XAttribute(xmi + "type", "uml:Property"), attributeComparer))
                        .ToList();
                    foreach (XElement field in fields)
                    {
                        //określenie typu pola
                        //TODO: i tu mamy problem z odczytaniem typu - jak odczytywać typy z xmi? poniższy odczyt jest dość 'dziki'
                        //jest jakiś chaos jeśli chodzi o typy danych w standardzie XMI=> http://www.empowertec.de/blog/2008/05/13/the-mess-called-xmi-xml-metadata-interchange/
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
                }
                
                //dziedziczenie

                //dla każdej klasy sprawdzam czy po czyms dziedziczy
                foreach (XElement _class in classes)
                {
                    //klasa bazowa
                    //TODO: czy w UML może być więcej niż 1 element <generalization>? czy mamy dopuszczone dziedziczenie wielokrotne?
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

                //generuję kod każdej klasy
                //TODO: generowanie osobnych plików dla klas
/*                foreach (XElement _class in classes)
                {
                    string fileName = _class.Attribute("name").Value.ToString() + ".cs";
                    using (StreamWriter sourceWriter = new StreamWriter(fileName))
                    {
                        provider.GenerateCodeFromCompileUnit(targetUnit,
                            sourceWriter, options);
                    }
                }*/

                //generuję kod przestrzeni nazw w jednym pliku
                string fileName = "Test.cs";
                using (StreamWriter sourceWriter = new StreamWriter(fileName))
                {
                    provider.GenerateCodeFromCompileUnit(targetUnit,
                        sourceWriter, options);
                }
            }

            return "Plik przetworzono pomyślnie";
        }
    }
}
