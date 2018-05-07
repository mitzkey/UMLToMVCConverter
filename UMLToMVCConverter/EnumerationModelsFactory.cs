namespace UMLToMVCConverter
{
    using System.Collections.Generic;
    using System.Linq;
    using UMLToMVCConverter.ExtendedTypes;
    using UMLToMVCConverter.Interfaces;

    public class EnumerationModelsFactory : IEnumerationModelsFactory
    {
        public IEnumerable<EnumerationModel> Create(List<ExtendedCodeTypeDeclaration> types)
        {
            foreach (var type in types.Where(t => t.IsEnum))
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