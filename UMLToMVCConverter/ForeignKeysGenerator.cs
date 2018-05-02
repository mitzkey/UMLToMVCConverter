namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using UMLToMVCConverter.ExtendedTypes;

    public class ForeignKeysGenerator : IForeignKeysGenerator
    {
        public void Generate(IEnumerable<Aggregation> aggregations)
        {
            foreach (var aggregation in aggregations)
            {
                var compositeType = aggregation.PrincipalType;
                var composedType = aggregation.DependentType;

                if (compositeType.PrimaryKeyAttributes.Count > 0)
                {
                    foreach (var compositeTypePrimaryKeyAttribute in compositeType.PrimaryKeyAttributes)
                    {
                        composedType.ForeignKeys.Add(
                            compositeType.Name + compositeTypePrimaryKeyAttribute.Name,
                            compositeTypePrimaryKeyAttribute);
                    }
                }
                else
                {
                    var foreignKeyName = compositeType.Name + "ID";

                    var generic = new ExtendedType(typeof(int), true);
                    var cSharpType = new ExtendedType(typeof(Nullable), true, true, new List<ExtendedType> { generic });
                    var foreignKeyType = ExtendedCodeTypeReference.CreateForType(cSharpType);
                    var foreignKeyProperty = new ExtendedCodeMemberProperty
                    {
                        Type = foreignKeyType,
                        Name = foreignKeyName,
                        HasSet = true
                    };

                    composedType.ForeignKeys.Add(foreignKeyName, foreignKeyProperty);
                }
            }
        }
    }
}