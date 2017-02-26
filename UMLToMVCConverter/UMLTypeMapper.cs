using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UMLToMVCConverter
{
    class UMLTypeMapper
    {
        public static Type UMLToCsharp(string UMLType)
        {
            switch (UMLType.ToLower())
            {
                case "date": 
                    return typeof(DateTime);
                case "string":
                    return typeof(string);
                case "integer":
                    return typeof(int);
                case "double":
                    return typeof(double);
                default:
                    throw new Exception("Mapowanie typu UML: " + UMLType + " nie zaimplementowane.");
            }

        }
    }
}
