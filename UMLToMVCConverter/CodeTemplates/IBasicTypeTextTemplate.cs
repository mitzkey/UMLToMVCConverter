﻿namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.Domain.Models;

    public interface IBasicTypeTextTemplate
    {
        string TransformText(TypeModel type);
    }
}