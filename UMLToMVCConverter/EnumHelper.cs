namespace UMLToMVCConverter
{
    using System;
    using System.Collections.Generic;
    using System.CodeDom;
    using System.Linq;

    class EnumHelper
    {
        public static IEnumerable<Enum> GetFlags(Enum input)
        {
            return Enum.GetValues(input.GetType())
                .Cast<Enum>()
                .Where(value => input.HasFlag(value) 
                    && !value.Equals(MemberAttributes.Family) 
                    && !value.Equals(MemberAttributes.FamilyOrAssembly)
                    && !value.Equals(MemberAttributes.FamilyAndAssembly)
                    && !value.Equals(MemberAttributes.Final)
                    && !value.Equals(MemberAttributes.Assembly));
        }
    }
}
