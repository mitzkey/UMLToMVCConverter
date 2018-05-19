﻿namespace UMLToMVCConverter.UMLHelpers
{
    using System.Xml.Linq;
    using UMLToMVCConverter.Domain;
    using UMLToMVCConverter.Domain.Models;

    public interface IUmlTypesHelper
    {
        TypeReference GetXElementCsharpType(XElement xElement);
        bool IsAbstract(XElement type);
        bool IsClass(XElement type);
        bool IsStruct(XElement type);
        bool IsEnum(XElement type);
    }
}