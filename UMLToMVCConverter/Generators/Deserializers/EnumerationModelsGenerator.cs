namespace UMLToMVCConverter.Generators.Deserializers
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Generators.Deserializers.Interfaces;
    using UMLToMVCConverter.Models;
    using UMLToMVCConverter.Models.Repositories.Interfaces;

    public class EnumerationModelsGenerator : IEnumerationModelsDeserializer
    {
        private readonly ITypesRepository typesRepository;

        public EnumerationModelsGenerator(ITypesRepository typesRepository)
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