namespace UMLToEFConverter.Generators
{
    using System.Collections.Generic;
    using UMLToEFConverter.Generators.Interfaces;
    using UMLToEFConverter.Models;
    using UMLToEFConverter.Models.Repositories.Interfaces;

    public class EnumerationModelsGenerator : IEnumerationModelsGenerator
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