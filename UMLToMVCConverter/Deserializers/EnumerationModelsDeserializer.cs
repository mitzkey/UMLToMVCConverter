namespace UMLToMVCConverter.Deserializers
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Deserializers.Interfaces;
    using UMLToMVCConverter.Models;
    using UMLToMVCConverter.Repositories.Interfaces;

    public class EnumerationModelsDeserializer : IEnumerationModelsDeserializer
    {
        private readonly ITypesRepository typesRepository;

        public EnumerationModelsDeserializer(ITypesRepository typesRepository)
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