namespace UMLToMVCConverter.Domain.Models
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;

    public class ExtendedCodeMemberProperty : CodeMemberProperty
    {
        private string defaultValueString;

        private IReadOnlyDictionary<Type, string> defaultValueFormats;

        public ExtendedCodeMemberProperty(string name, ExtendedCodeTypeReference codeTypeReference, ITypesRepository typesRepository)
        {
            this.Name = name;
            this.Type = codeTypeReference;
            if (codeTypeReference.ExtType.IsReferencingXmiDeclaredType)
            {
                this.ReferencingType = typesRepository.GetTypeByXmiId(codeTypeReference.ExtType.ReferenceTypeXmiID);
            }
        }

        public ExtendedCodeTypeDeclaration ReferencingType { get; set; }

        public bool HasDefaultValueString { get; private set; }

        public ExtendedCodeTypeReference ExtendedTypeReference => (ExtendedCodeTypeReference) this.Type;

        public string DefaultValueString
        {
            get
            {
                var codeTypeReference = (ExtendedCodeTypeReference)base.Type;
                var type = codeTypeReference.ExtType.Type;
                var result = this.defaultValueFormats[type];
                if (result == null)
                {
                    throw new NotSupportedException("Default value for type: " + type + " not supported.");
                }
                return result;
            }

            set
            {
                this.defaultValueString = value;

                this.defaultValueFormats = new Dictionary<Type, string> {
                    { typeof(string), "\"" + this.defaultValueString + "\"" },
                    { typeof(int), this.defaultValueString },
                    { typeof(double), this.defaultValueString + "d" },
                    { typeof(bool), this.defaultValueString.ToLower() }
                };

                this.HasDefaultValueString = true;
            }
        }

        public bool IsDerived { get; set; }

        public bool IsID { get; set; }

        public bool IsVirtual { get; set; }

        public bool HasDefaultValueKey => this.DefaultValueKey != null;

        public int? DefaultValueKey { get; set; }

        public bool IsReferencingType => this.ReferencingType != null;

        public bool IsReferencingEnumType => this.IsReferencingType && this.ReferencingType.IsEnum;
    }
}
