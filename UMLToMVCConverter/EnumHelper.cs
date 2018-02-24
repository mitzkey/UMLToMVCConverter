using System;
using System.Collections.Generic;
using System.CodeDom;

namespace UMLToMVCConverter
{
    class EnumHelper
    {
        public static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value) 
                    && !value.Equals(MemberAttributes.Family) 
                    && !value.Equals(MemberAttributes.FamilyOrAssembly)
                    && !value.Equals(MemberAttributes.FamilyAndAssembly))
                    yield return value;
        }
    }
}
