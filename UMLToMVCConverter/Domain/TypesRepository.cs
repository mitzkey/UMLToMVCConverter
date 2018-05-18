namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.Domain.Models;

    public class TypesRepository : ITypesRepository
    {
        private readonly List<TypeModel> types;

        public TypesRepository()
        {
            this.types = new List<TypeModel>();
        }

        public TypeModel GetTypeByXmiId(string xmiId)
        {
            return this.types.Single(x => x.XmiID == xmiId);
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
    }
}