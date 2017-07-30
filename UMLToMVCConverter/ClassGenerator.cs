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
using UMLToMVCConverter.ExtendedTypes;
using UMLToMVCConverter.Mappers;

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
        public string GenerateTypes()
        {                                             
            //dla każdego modelu <uml:Model>
            List<XElement> umlModels = xdoc.Descendants(uml_namespace + "Model").ToList();
            foreach (XElement umlModel in umlModels)
            {
                //dla każdej klasy <packagedElement xmi:type='uml:Class'>:
                List<XElement> xTypes = umlModel.Descendants("packagedElement")
                    .Where(i => i.Attributes().
                        Contains(new XAttribute(xmi_namespace + "type", "uml:Class"), attributeComparer)
                        || i.Attributes().Contains(new XAttribute(xmi_namespace + "type", "uml:DataType"), attributeComparer))
                    .ToList();

                foreach (XElement _type in xTypes)
                {
                    //deklaracja klasy
                    CodeTypeDeclaration ctd = GenerateTypeFromXElement(_type);
                    types.Add(ctd);                                                                               
                }
                
                //dziedziczenie

                //po utworzeniu obiektów w liście typów, dla każdej klasy sprawdzam czy po czyms dziedziczy
                foreach (XElement _type in xTypes)
                {
                    //klasa bazowa
                    //TODO: trzeba coś ustalić ws. dziedziczenia wielokrotnego => zadanie na tablicy
                    XElement xCurrClass = _type.Descendants("generalization")
                        .Where(i => i.Attributes()
                            .Contains(new XAttribute(xmi_namespace + "type", "uml:Generalization"), attributeComparer))
                        .FirstOrDefault();
                    if (xCurrClass != null)
                    {
                        string base_class_id = xCurrClass.Attribute("general").Value;
                        string base_class_name = xTypes.Where(i => i.Attribute(xmi_namespace + "id").Value.Equals(base_class_id)).First().Attribute("name").Value;
                        string curr_class_name = _type.Attribute("name").Value;
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

        private CodeTypeDeclaration GenerateTypeFromXElement(XElement _type)
        {
            //deklaracja typu
            CodeTypeDeclaration ctd = new CodeTypeDeclaration();

            //jaki to typ
            string type = _type.Attribute(xmi_namespace + "type").Value; 
            switch (type) {
                case "uml:Class":
                    ctd.IsClass = true;
                    break;
                case "uml:DataType":
                    ctd.IsStruct = true;
                    break;
                default:
                    throw new Exception("Nieobsługiwany typ: " + type);
            }
                

            ctd.Name = _type.Attribute("name").Value;
            ctd.TypeAttributes = TypeAttributes.Public; //TODO: czy w UML mamy widoczność typu?                    
            

            //czy jest abstrakcyjna
            if (_type.Attribute("isAbstract") != null && _type.Attribute("isAbstract").Value == "true")
            {
                ctd.TypeAttributes = ctd.TypeAttributes | TypeAttributes.Abstract;
            }

            //czy są klasy zagnieżdżone
            List<XElement> nestedClasses = _type.Descendants("nestedClassifier").ToList();
            foreach (XElement nestedClass in nestedClasses)
            {
                CodeTypeDeclaration ctd_nested = GenerateTypeFromXElement(nestedClass);
                ctd.Members.Add(ctd_nested);
            }

            #region pola, metody          

            //pola
            List<XElement> attributes = _type.Descendants("ownedAttribute")
                    .Where(i => i.Attributes()
                        .Contains(new XAttribute(xmi_namespace + "type", "uml:Property"), attributeComparer))
                    .ToList();
            foreach (XElement attribute in attributes)
            {
                //liczności
                XElement lv = attribute.Descendants("lowerValue").SingleOrDefault();
                string lowerValue = lv == null ? "" : lv.Attribute("value") == null ? "" : lv.Attribute("value").Value;
                XElement uv = attribute.Descendants("upperValue").SingleOrDefault();
                string upperValue = uv == null ? "" : uv.Attribute("value") == null ? "" : uv.Attribute("value").Value;

                //określenie typu pola                
                ExtendedType cSharpType = GetXElementCsharpType(attribute, lowerValue, upperValue);

                //deklaracja pola
                CodeMemberProperty cmp = new CodeMemberProperty();
                CodeTypeReference typeRef = new ExtendedCodeTypeReference(cSharpType);
                cmp.Type = typeRef;
                cmp.Name = attribute.Attribute("name").Value;

                //widoczność
                string UMLvisibility = attribute.Attribute("visibility").Value;
                MemberAttributes cSharpVisibility = UMLVisibilityMapper.UMLToCsharp(UMLvisibility);
                cmp.Attributes = cSharpVisibility;               

                ctd.Members.Add(cmp);
            }

            //metody
            List<XElement> operations = _type.Descendants("ownedOperation")
                    .Where(i => i.Attributes()
                        .Contains(new XAttribute(xmi_namespace + "type", "uml:Operation"), attributeComparer))
                    .ToList();
            foreach (XElement operation in operations)
            {
                CodeMemberMethod cmm = new CodeMemberMethod();

                cmm.Name = operation.Attribute("name").Value;

                //typ zwracany
                XElement returnParameter = operation.Descendants("ownedParameter").Where(i => i.Attribute("direction").Value.Equals("return")).FirstOrDefault();
                if (returnParameter == null)
                {

                }
                ExtendedType returnType = GetXElementCsharpType(returnParameter, "", "");
                if (returnType != null)
                {
                    CodeTypeReference ctr = new ExtendedCodeTypeReference(GetXElementCsharpType(returnParameter, "", ""));
                    cmm.ReturnType = ctr;
                }
                            

                //parametry wejściowe
                List<XElement> parameters = operation.Descendants("ownedParameter").Where(i => i.Attribute("direction") == null || !i.Attribute("direction").Value.Equals("return")).ToList();
                foreach(XElement xParameter in parameters) {
                    ExtendedType parType = GetXElementCsharpType(xParameter, "", "");
                    string parName = xParameter.Attribute("name").Value;
                    CodeParameterDeclarationExpression parameter = new ExtendedCodeParameterDeclarationExpression(parType, parName);
                    cmm.Parameters.Add(parameter);
                }

                //widoczność
                string UMLvisibility = operation.Attribute("visibility").Value;
                MemberAttributes cSharpVisibility = UMLVisibilityMapper.UMLToCsharp(UMLvisibility);
                cmm.Attributes = cSharpVisibility;
                ctd.Members.Add(cmm);
            }

            #endregion

            return ctd;
        }

        private ExtendedType GetXElementCsharpType(XElement xElement, string mplLowerVal, string mplUpperVal)
        {
            XElement xType = xElement.Descendants("type").FirstOrDefault();
            if (xType == null)
            {
                return null;
            }
            XElement xRefExtension = xType.Descendants("referenceExtension").FirstOrDefault();
            string UMLtype = xRefExtension.Attribute("referentPath").Value.Split(new char[] { ':', ':' }).Last();
            ExtendedType cSharpType = UMLTypeMapper.UMLToCsharp(UMLtype, mplLowerVal, mplUpperVal);
            return cSharpType;
        }

        private void GenerateFiles(List<CodeTypeDeclaration> types, string namespaceName)
        {
            //dla każdej klasy generowanie klasy modeli
            GenerateModels(types, namespaceName);

            /*tylko niektóre typy będą niezależnymi obiektami, nie będą nimi:
             * - klasy abstrakcyjne
             * - typy wartościowe
            */
            List<CodeTypeDeclaration> standaloneEntityTypes = types.
                Where(i => !i.TypeAttributes.HasFlag(TypeAttributes.Abstract)
                    && !i.IsStruct)
                .ToList();
            //generowanie pliku kontekstu (DbContext)
            GenerateDbContextClass(standaloneEntityTypes, namespaceName);
            //generowanie plików konrolerów
            GenerateControllers(standaloneEntityTypes, namespaceName);
            //generowanie widoków
            GenerateViews(standaloneEntityTypes, namespaceName);
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