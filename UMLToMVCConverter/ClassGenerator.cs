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
            //briefcase example - nie działa
            Briefcase bc = new Briefcase("Briefcase");
            bc.CreateCodeDomBriefcase();

            //msdn example
            Sample sample = new Sample();
            sample.AddFields();
            sample.AddProperties();
            sample.AddMethod();
            sample.AddConstructor();
            sample.AddEntryPoint();
            sample.GenerateCSharpCode("Sample");


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
                    //ustawiamy zmienne potrzebne do generowania kodu
                    //TODO: na razie każda klasa w osobnym pliku, docelowo możemy mieć klasę zagnieżdżoną np.
                    string fileName = _class.Attribute("name").Value.ToString() + ".cs";
                    Stream s = File.Open(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\" + fileName, FileMode.Create);
                    StreamWriter sw = new StreamWriter(s);

                    CSharpCodeProvider cscProvider = new CSharpCodeProvider();
                    ICodeGenerator cscg = cscProvider.CreateGenerator(sw);
                    CodeGeneratorOptions cop = new CodeGeneratorOptions();

                    //TODO: tworzymy wpisy 'using' - na razie nie wiem jak to może wyglądać od strony UML
                    CodeSnippetCompileUnit csu1 = new CodeSnippetCompileUnit("using System");
                    cscg.GenerateCodeFromCompileUnit(csu1, sw, cop);
                    sw.WriteLine();

                    //TODO: namespace, też nie wiem jak w kontekście UML
                    CodeNamespace cns = new CodeNamespace("Test01");

                    //deklaracja klasy
                    CodeTypeDeclaration ctd = new CodeTypeDeclaration();
                    cns.Types.Add(ctd);
                    ctd.IsClass = true;
                    ctd.Name = _class.Attribute("name").Value;
                    ctd.TypeAttributes = TypeAttributes.Public; //TODO: czy w UML mamy widoczność klasy?                    

                    //pola
                    List<XElement> fields = _class.Descendants("ownedAttribute")
                        .Where(i => i.Attributes()
                            .Contains(new XAttribute(xmi + "type", "uml:Property"), attributeComparer))
                        .ToList();
                    foreach (XElement field in fields)
                    {
                        //określenie typu pola
                        //TODO: i tu mamy problem z odczytaniem typu - jak odczytywać typy z xmi? poniższy odczyt jest dość 'dziki'
                        XElement xType = field.Descendants("type").FirstOrDefault();
                        XElement xExtension = xType.Descendants(xmi + "Extension").FirstOrDefault();
                        XElement xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
                        string UMLtype = xRefExtension.Attribute("referentPath").Value.Split(new char[]{':',':'}).Last();
                        Type cSharpType = UMLTypeMapper.UMLToCsharp(UMLtype);
                        sw.WriteLine();  
                        CodeMemberField cmf = new CodeMemberField(cSharpType, field.Attribute("name").Value);
                        
                        //widoczność
                        string UMLvisibility = field.Attribute("visibility").Value;
                        MemberAttributes cSharpVisibility = UMLVisibilityMapper.UMLToCsharp(UMLvisibility);
                        cmf.Attributes = cSharpVisibility;
                        ctd.Members.Add(cmf);
                    }

                    cscg.GenerateCodeFromNamespace(cns, sw, cop);
                    sw.Close();
                    s.Close();
                }
            }

            return "OK";
        }
    }
}
