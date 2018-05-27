﻿namespace UMLToMVCConverter.Deserializers.Interfaces
{
    using System.Collections.Generic;
    using UMLToMVCConverter.Models;

    public interface IEnumerationModelsDeserializer
    {
        IEnumerable<Enumeration> Create();
    }
}