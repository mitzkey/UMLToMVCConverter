﻿namespace UMLToMVCConverter.UMLHelpers
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain;

    public interface IUmlTypesHelper
    {
        ExtendedType GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
        bool IsEnum(XElement type);
    }
}