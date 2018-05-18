namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Domain.Models;

    public class EnumerationModelsFactory : IEnumerationModelsFactory
    {
        private readonly ITypesRepository typesRepository;

        public EnumerationModelsFactory(ITypesRepository typesRepository)
        {
            this.typesRepository = typesRepository;
        }

        public IEnumerable<Enumeration> Create()
        {
            foreach (var type in this.typesRepository.GetEnums())
            {
                yield return new Enumeration
                {
                    Name = type.Name,
                    Literals = type.Literals
                };
            }
        }
    }
}