namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.ExtendedTypes;

    public class TypesRepository : ITypesRepository
    {
        private readonly List<ExtendedCodeTypeDeclaration> types;

        public TypesRepository()
        {
            this.types = new List<ExtendedCodeTypeDeclaration>();
        }

        public ExtendedCodeTypeDeclaration GetTypeByXmiId(string xmiId)
        {
            return this.types.Single(x => x.XmiID == xmiId);
        }

        public void Add(ExtendedCodeTypeDeclaration type)
        {
            this.types.Add(type);
        }

        public ExtendedCodeTypeDeclaration GetTypeByName(string name)
        {
            return this.types.FirstOrDefault(i => i.Name == name);
        }

        public IEnumerable<ExtendedCodeTypeDeclaration> GetEnums()
        {
            return this.types.Where(t => t.IsEnum);
        }

        public IEnumerable<ExtendedCodeTypeDeclaration> GetAllTypes()
        {
            return this.types;
        }
    }
}