﻿namespace UMLToEFConverter.Common.XmiTools.Interfaces
{
    using System.Collections.Generic;
    using System.Xml.Linq;
    using UMLToEFConverter.Models;

    public interface IXmiWrapper
    {
        Multiplicity GetMultiplicity(XElement attribute);
        string GetPrimitiveUmlType(XElement xProperty);
        IEnumerable<XElement> GetXAttributes(XElement type);
        XElement GetXTypeGeneralization(XElement type);
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
        IEnumerable<XElement> GetAssociationEndsXElements(XElement xAssociation);
        string GetElementsId(XElement xElement);
        IEnumerable<XElement> GetXAggregations(XElement xUmlModel);
        IEnumerable<XElement> GetLiterals(XElement xType);
        XElementType GetXElementType(XElement xElement);
        XElement GetOppositeAssociationEnd(string associationId, string xElementId);
        AssociationKind GetAggregationKind(string aggregationKindString);
        XElement GetXOwner(XElement xElement);
        XElement GetAssociationsEndOwningType(XElement xAssociationEnd);
        IEnumerable<XElement> GetXProperties(XElement xType);
    }
}