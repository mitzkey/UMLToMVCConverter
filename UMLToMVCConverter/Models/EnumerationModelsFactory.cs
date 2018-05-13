namespace UMLToMVCConverter.Models
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Interfaces;

    public class EnumerationModelsFactory : IEnumerationModelsFactory
    {
        private readonly ITypesRepository typesRepository;

        public EnumerationModelsFactory(ITypesRepository typesRepository)
        {
            this.typesRepository = typesRepository;
        }

        public IEnumerable<EnumerationModel> Create()
        {
            foreach (var type in this.typesRepository.GetEnums())
            {
                yield return new EnumerationModel
                {
                    Name = type.Name,
                    Literals = type.Literals
                };
            }
        }
    }
}