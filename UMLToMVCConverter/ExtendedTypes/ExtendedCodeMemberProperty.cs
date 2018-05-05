﻿namespace UMLToMVCConverter.ExtendedTypes
{
    using System;
    using System.CodeDom;
    using System.Collections.Generic;

    public class ExtendedCodeMemberProperty : CodeMemberProperty
    {
        private string defaultValueString;

        private IReadOnlyDictionary<Type, string> defaultValueFormats;

        public bool HasDefaultValue { get; private set; }

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

                this.HasDefaultValue = true;
            }
        }

        public bool IsDerived { get; set; }

        public bool IsID { get; set; }

        public bool IsVirtual { get; set; }
    }
}
