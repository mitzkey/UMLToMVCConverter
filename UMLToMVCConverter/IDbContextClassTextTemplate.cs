namespace UMLToMVCConverter
{
    using System.CodeDom;
    using System.Collections.Generic;

    public interface IDbContextClassTextTemplate
    {
        string TransformText(List<CodeTypeDeclaration> standaloneEntityTypes);
    }
}