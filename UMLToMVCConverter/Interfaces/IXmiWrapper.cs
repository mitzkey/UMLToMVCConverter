﻿namespace UMLToMVCConverter.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    public interface IXmiWrapper
    {
        Multiplicity GetMultiplicity(XElement attribute);
        string GetPrimitiveUmlType(XElement xProperty);
        IEnumerable<XElement> GetXAttributes(XElement type);
        XElement GetXClassGeneralization(XElement type);
        XElement GetXElementById(string id);
        IEnumerable<XElement> GetXOperations(XElement type);
        IEnumerable<XElement> GetXParameters(XElement operation);
        XElement GetXReturnParameter(XElement operation);
        IEnumerable<XElement> GetXTypes(XElement umlModel);
        IEnumerable<XElement> GetXUmlModels();
        bool IsOfPrimitiveType(XElement xElement);
        bool IsUmlProperty(XElement xElement);
        string ObligatoryAttributeValueWithNamespace(XElement type, string s);
        IEnumerable<XElement> GetXAssociations(XElement umlModel);
        Tuple<XElement, XElement> GetAssociationEnds(XElement xAssociation);
        string GetElementsId(XElement xElement);
    }
}