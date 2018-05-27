﻿namespace UMLToMVCConverter.CodeTemplates
{
    using UMLToMVCConverter.CodeTemplates.Interfaces;
    using UMLToMVCConverter.Models;

    public partial class ModelClassTextTemplate : IModelClassTextTemplate
    {
        private readonly MvcProject mvcProject;
        private readonly IBasicTypeTextTemplate basicTypeTextTemplate;
        TypeModel type;

        public ModelClassTextTemplate(MvcProject mvcProject, IBasicTypeTextTemplate basicTypeTextTemplate)
        {
            this.mvcProject = mvcProject;
            this.basicTypeTextTemplate = basicTypeTextTemplate;
        }

        public string TransformText(TypeModel typeModel)
        {
            this.type = typeModel;
            return this.TransformText();
        }
    }
}
