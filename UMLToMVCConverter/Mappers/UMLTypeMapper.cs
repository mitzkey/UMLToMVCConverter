using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMLToMVCConverter.ExtendedTypes;

namespace UMLToMVCConverter.Mappers
{
    class UMLTypeMapper
    {
        public static ExtendedType UMLToCsharp(string UMLType, string mplLowerVal, string mplUpperVal)
        {
            switch (UMLType.ToLower())
            {
                case "date": 
                    return GetDetailedType(typeof(DateTime), mplLowerVal, mplUpperVal);
                case "string":
                    return GetDetailedType(typeof(string), mplLowerVal, mplUpperVal);
                case "integer":
                    return GetDetailedType(typeof(int), mplLowerVal, mplUpperVal);
                case "double":
                    return GetDetailedType(typeof(double), mplLowerVal, mplUpperVal);
                case "void":
                    return null;
                case "int":
                    return GetDetailedType(typeof(int), mplLowerVal, mplUpperVal);
                default:
                    throw new Exception("Mapowanie typu UML: " + UMLType + " niezaimplementowane.");
            }

        }

        private static ExtendedType GetDetailedType(Type type, string mplLowerVal, string mplUpperVal)
        {
            ExtendedType returnType;
            if (mplLowerVal == "")
            {
                returnType = GetNullableType(type);
            }
            else
            {
                returnType = new ExtendedType(type);
            }

            if (mplUpperVal != "")
            {
                returnType = new ExtendedType(typeof(ICollection<>), true, new List<Type> {type});
            }
            return returnType;
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
