namespace UMLToMVCConverter.Domain
{
    using System.Collections.Generic;

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