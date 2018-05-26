namespace UMLToMVCConverter.Domain.Repositories
{
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.Domain.Models;
    using UMLToMVCConverter.Domain.Repositories.Interfaces;

    public class TypesRepository : ITypesRepository
    {
        private readonly List<TypeModel> types;
        private readonly List<TypeModel> typeDeclarations;

        public TypesRepository()
        {
            this.types = new List<TypeModel>();
            this.typeDeclarations = new List<TypeModel>();
        }

        public TypeModel GetTypeByXmiId(string xmiId)
        {
            return this.types.SingleOrDefault(x => x.XmiID == xmiId)
                ?? this.typeDeclarations.Single(x => x.XmiID == xmiId);
        }

        public void Add(TypeModel type)
        {
            this.types.Add(type);
        }

        public TypeModel GetTypeByName(string name)
        {
            return this.types.FirstOrDefault(i => i.Name == name);
        }

        public IEnumerable<TypeModel> GetEnums()
        {
            return this.types.Where(t => t.IsEnum);
        }

        public IEnumerable<TypeModel> GetAllTypes()
        {
            return this.types;
        }

        public void DeclareType(TypeModel typeDeclaration)
        {
            this.typeDeclarations.Add(typeDeclaration);
        }

        public TypeModel GetTypeDeclaration(string xTypeName)
        {
            return this.typeDeclarations.Single(t => t.Name.Equals(xTypeName));
        }
    }
}