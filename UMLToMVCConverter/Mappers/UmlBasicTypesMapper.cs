using System;
using System.Collections.Generic;
using UMLToMVCConverter.ExtendedTypes;

namespace UMLToMVCConverter.Mappers
{
    public class UmlBasicTypesMapper
    {
        public static ExtendedType UmlToCsharp(string umlType, string multiplicityLowerBound, string multiplicityUpperBound)
        {
            switch (umlType.ToLower())
            {
                case "date": 
                    return GetDetailedType(typeof(DateTime), multiplicityLowerBound, multiplicityUpperBound);
                case "string":
                    return GetDetailedType(typeof(string), multiplicityLowerBound, multiplicityUpperBound);
                case "integer":
                    return GetDetailedType(typeof(int), multiplicityLowerBound, multiplicityUpperBound);
                case "double":
                    return GetDetailedType(typeof(double), multiplicityLowerBound, multiplicityUpperBound);
                case "void":
                    return ExtendedType.Void;
                case "int":
                    return GetDetailedType(typeof(int), multiplicityLowerBound, multiplicityUpperBound);
                case "boolean":
                    return GetDetailedType(typeof(bool), multiplicityLowerBound, multiplicityUpperBound);
                default:
                    throw new Exception("Mapowanie typu UML: " + umlType + " niezaimplementowane.");
            }

        }

        private static ExtendedType GetDetailedType(Type type, string multiplicityLowerBound, string multiplicityUpperBound)
        {
            if (!string.IsNullOrWhiteSpace(multiplicityUpperBound) 
                && (multiplicityUpperBound == "*"
                    || Convert.ToInt32(multiplicityUpperBound) > 1))
            {
                return new ExtendedType(typeof(ICollection<>), true, new List<Type> { type }, true);
            }

            if (string.IsNullOrWhiteSpace(multiplicityLowerBound) || Convert.ToInt32(multiplicityLowerBound) == 0)
            {
                return GetNullableType(type);
            }

            return new ExtendedType(type);
        }

        private static ExtendedType GetNullableType(Type type)
        {            
            Type returnType = Nullable.GetUnderlyingType(type);
            if (returnType != null)
            {
                return new ExtendedType(type);
            }
            else
            {
                if (type.IsValueType)
                    return new ExtendedType(typeof(Nullable), true, new List<Type> { type });
                else
                    return new ExtendedType(type);
            }   
        }
    }
}
