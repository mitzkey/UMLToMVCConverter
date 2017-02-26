 using System;
     using System.CodeDom;
     using System.CodeDom.Compiler;
     using System.Reflection;
     using System.IO;
     using Microsoft.CSharp;
     using Microsoft.VisualBasic;

namespace UMLToMVCConverter
   {
	/// 

	/// Summary description for Briefcase.
	/// 

	public class Briefcase
{
	//Member Variables		
	private string m_strFileName;	
	private string m_Suffix = ".cs";

	public Briefcase(string strFileName)
	{
		m_strFileName = strFileName;		
	}	
		
	public void CreateCodeDomBriefcase()
	{
	 //Initialize CodeDom Variables			
	 Stream s = File.Open(m_strFileName + m_Suffix, FileMode.Create);
	 StreamWriter sw = new StreamWriter(s);
			
	 CSharpCodeProvider cscProvider = new CSharpCodeProvider();
	 ICodeGenerator cscg = cscProvider.CreateGenerator(sw);			
	 CodeGeneratorOptions cop = new CodeGeneratorOptions();				

	 //Create Class Using Statements
	 CodeSnippetCompileUnit csu1 = new CodeSnippetCompileUnit("using System");
	 CodeSnippetCompileUnit csu2 = new CodeSnippetCompileUnit("using System.IO");	
	 cscg.GenerateCodeFromCompileUnit(csu1, sw, cop);
	 cscg.GenerateCodeFromCompileUnit(csu2, sw, cop);
	 sw.WriteLine();
			
	 //Create Class Namespaces
	 CodeNamespace cnsCodeDom = new CodeNamespace("CodeDom");				
						
	 //Create Class Declaration
	 CodeTypeDeclaration ctd = new CodeTypeDeclaration();
	 ctd.IsClass = true;		
	 ctd.Name = "Briefcase";
	 ctd.TypeAttributes = TypeAttributes.Public;
			
	 //Create Class Member Fields	
	 sw.WriteLine();				
	 CodeMemberField cmfBriefcaseName = new CodeMemberField("string","m_BriefcaseName");
	 cmfBriefcaseName.Attributes = MemberAttributes.Private;				
	 ctd.Members.Add(cmfBriefcaseName);			
			
	 CodeMemberField cmfBriefcaseTitle = new CodeMemberField("string", "m_BriefcaseTitle");
	 cmfBriefcaseTitle.Attributes = MemberAttributes.Private;
	 ctd.Members.Add(cmfBriefcaseTitle);					
			
	 CodeMemberField cmfBriefcaseID = new CodeMemberField("int", "m_cmfBriefcaseID");
	 cmfBriefcaseID.Attributes = MemberAttributes.Private;			
	 ctd.Members.Add(cmfBriefcaseID);

	 CodeMemberField cmfBriefcaseSectionID = new CodeMemberField("int", "m_BriefcaseSectionID");
	 cmfBriefcaseSectionID.Attributes = MemberAttributes.Private;	
	 ctd.Members.Add(cmfBriefcaseSectionID);

	 CodeMemberField cmfBriefcaseFolderID = new CodeMemberField("int", "m_BriefcaseFolderID");
	 cmfBriefcaseFolderID.Attributes = MemberAttributes.Private;
	 ctd.Members.Add(cmfBriefcaseFolderID);

	 CodeMemberField cmfBriefcaseItemID = new CodeMemberField("int", "m_BriefcaseItemID");
	 cmfBriefcaseItemID.Attributes = MemberAttributes.Private;
	 ctd.Members.Add(cmfBriefcaseItemID);					

	 //Create Class Constructor				
	 CodeConstructor ccon = new CodeConstructor();
	 ccon.Attributes = MemberAttributes.Public;
	 ccon.Statements.Add(new CodeSnippetStatement("//"));
	 ccon.Statements.Add(new CodeSnippetStatement("// TODO: Add constructor logic here"));
	 ccon.Statements.Add(new CodeSnippetStatement("//"));					
	 ctd.Members.Add(ccon);						

	 //Create Class BriefcaseName Property
	 CodeMemberProperty mpBriefcaseName = new CodeMemberProperty();
	 mpBriefcaseName.Attributes = MemberAttributes.Private;
	 mpBriefcaseName.Type = new CodeTypeReference("string");
	 mpBriefcaseName.Name = "BriefcaseName";				
	 mpBriefcaseName.HasGet = true;			
	 mpBriefcaseName.GetStatements.Add(new CodeSnippetExpression("return m_BriefcaseName"));
	 mpBriefcaseName.HasSet = true;
	 mpBriefcaseName.SetStatements.Add(new CodeSnippetExpression("m_BriefcaseName = value"));
	 ctd.Members.Add(mpBriefcaseName);			
			
	 //Create Class BriefcaseTitle Property
	 CodeMemberProperty mpBriefcaseTitle = new CodeMemberProperty();
	 mpBriefcaseTitle.Attributes = MemberAttributes.Private;
	 mpBriefcaseTitle.Type = new CodeTypeReference("string");
	 mpBriefcaseTitle.Name = "BriefcaseTitle";			
	 mpBriefcaseTitle.HasGet = true;
	  mpBriefcaseTitle.GetStatements.Add(new CodeSnippetExpression("return m_BriefcaseTitle"));
	  mpBriefcaseTitle.HasSet = true;
	  mpBriefcaseTitle.SetStatements.Add(new CodeSnippetExpression("m_BriefcaseTitle = value"));
	  ctd.Members.Add(mpBriefcaseTitle);

	  //Create Class BriefcaseID Property
	  CodeMemberProperty mpBriefcaseID = new CodeMemberProperty();
  mpBriefcaseID.Attributes = MemberAttributes.Private;
	  mpBriefcaseID.Type = new CodeTypeReference("int");
	  mpBriefcaseID.Name = "BriefcaseID";		
	  mpBriefcaseID.HasGet = true;
	  mpBriefcaseID.GetStatements.Add(new CodeSnippetExpression("m_BriefcaseID"));
	  mpBriefcaseID.HasSet = true;
	  mpBriefcaseID.SetStatements.Add(new CodeSnippetExpression("m_BriefcaseID = value"));
	  ctd.Members.Add(mpBriefcaseID);

	  //Create Class BriefcaseSection Property
	  CodeMemberProperty mpBriefcaseSection = new CodeMemberProperty();
	  mpBriefcaseSection.Attributes = MemberAttributes.Private;
	  mpBriefcaseSection.Type = new CodeTypeReference("int");
	  mpBriefcaseSection.Name = "BriefcaseSection";				
	  mpBriefcaseSection.HasGet = true;
	  mpBriefcaseSection.GetStatements.Add(new CodeSnippetExpression
		  ("return m_BriefcaseSectionID"));
	  mpBriefcaseSection.HasSet = true;
	  mpBriefcaseSection.SetStatements.Add(new CodeSnippetExpression
		  ("m_BriefcaseSectionID = value"));
	  ctd.Members.Add(mpBriefcaseSection);

	  //Create Class BriefcaseFolder Property
	  CodeMemberProperty mpBriefcaseFolder = new CodeMemberProperty();
	  mpBriefcaseFolder.Attributes = MemberAttributes.Private;
	  mpBriefcaseFolder.Type = new CodeTypeReference("int");
	  mpBriefcaseFolder.Name = "BriefcaseFolder";			
	  mpBriefcaseFolder.HasGet = true;
	  mpBriefcaseFolder.GetStatements.Add(new CodeSnippetExpression("return m_BriefcaseFlderID"));
	  mpBriefcaseFolder.HasSet = true;
	  mpBriefcaseFolder.SetStatements.Add(new CodeSnippetExpression("m_BriefcaseFolderID = value"));
	  ctd.Members.Add(mpBriefcaseFolder);

	  //Create Class BriefcaseItem Property
	  CodeMemberProperty mpBriefcaseItem = new CodeMemberProperty();
	  mpBriefcaseItem.Attributes = MemberAttributes.Private;
	  mpBriefcaseItem.Type = new CodeTypeReference("string");
	  mpBriefcaseItem.Name = "BriefcaseItem";		
	  mpBriefcaseItem.HasGet = true;
	  mpBriefcaseItem.GetStatements.Add(new CodeSnippetExpression("return m_BriefcaseItemID"));
	  mpBriefcaseItem.HasSet = true;
	  mpBriefcaseItem.SetStatements.Add(new CodeSnippetExpression("m_BriefcaseItemID = value"));
	  ctd.Members.Add(mpBriefcaseItem);			

	  //Create Class GetBriefcaseName Method
	  CodeMemberMethod mtd1 = new CodeMemberMethod();
	  mtd1.Name = "GetBriefcaseName";
	  mtd1.ReturnType = new CodeTypeReference("String");
	  mtd1.Attributes = MemberAttributes.Public;
	  mtd1.Statements.Add(new CodeSnippetStatement("return BriefcaseName;"));
	  ctd.Members.Add(mtd1);
		
	  //Create Class GetBriefcaseTitle Method
	  CodeMemberMethod mtd2 = new CodeMemberMethod();
	  mtd2.Name = "GetBriefcaseTitle";
	  mtd2.ReturnType = new CodeTypeReference("String");
	  mtd2.Attributes = MemberAttributes.Public;
	  mtd2.Statements.Add(new CodeSnippetStatement("return BriefcaseTitle;"));
	  ctd.Members.Add(mtd2);

	  //Create Class GetBriefcaseID Method
	  CodeMemberMethod mtd3 = new CodeMemberMethod();
	  mtd3.Name = "GetBriefcaseID";
	  mtd3.ReturnType = new CodeTypeReference("Int");
	  mtd3.Attributes = MemberAttributes.Public;
	  mtd3.Statements.Add(new CodeSnippetStatement("return BriefcaseID;"));
	  ctd.Members.Add(mtd3);

	  //Create Class GetBriefcaseSection Method
	  CodeMemberMethod mtd4 = new CodeMemberMethod();
	  mtd4.Name = "GetBriefcaseSectionID";
	  mtd4.ReturnType = new CodeTypeReference("Int");
	  mtd4.Attributes = MemberAttributes.Public;
	  mtd4.Statements.Add(new CodeSnippetStatement("return BriefcaseSectionID;"));
	  ctd.Members.Add(mtd4);

	  //Create Class GetBriefcaseFolder Method
	  CodeMemberMethod mtd5 = new CodeMemberMethod();
	  mtd5.Name = "GetBriefcaseFolderID";
	  mtd5.ReturnType = new CodeTypeReference("Int");
  mtd5.Attributes = MemberAttributes.Public;
	  mtd5.Statements.Add(new CodeSnippetStatement("return BriefcaseFolderID;"));
	  ctd.Members.Add(mtd5);

	  //Create Class GetBriefcaseItem Method
	  CodeMemberMethod mtd6 = new CodeMemberMethod();
	  mtd6.Name = "GetBriefcaseItemID";			
	  mtd6.ReturnType = new CodeTypeReference("Int");
	  mtd6.Attributes = MemberAttributes.Public;
	  mtd6.Statements.Add(new CodeSnippetStatement("return BriefcaseItemID;"));
	  ctd.Members.Add(mtd6);

	  //Generate Source Code File
	  cscg.GenerateCodeFromNamespace(cnsCodeDom, sw, cop);

	  //Close StreamWriter
	  sw.Close();
	  s.Close();
  }
	    }
   }