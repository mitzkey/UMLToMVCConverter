﻿namespace UMLToEFConverter.Models.Builders
{
    using System;

    public class TypeReferenceBuilder
    {
        private Type type;
        private bool isBaseType;
        private bool isGeneric;
        private TypeReference generic;
        private string name;
        private string referenceTypeXmiId;
        private bool isCollection;
        private bool isNamedType;
        private bool isNullable;
        private bool isComplexType;
        private bool isPrimitive;

        public TypeReference Build()
        {
            return new TypeReference(
                this.type,
                this.name,
                this.isBaseType,
                this.isGeneric,
                this.generic,
                this.isCollection,
                this.referenceTypeXmiId,
                this.isNamedType,
                this.isNullable,
                this.isComplexType,
                this.isPrimitive);
        }

        public TypeReferenceBuilder SetType(Type type)
        {
            this.type = type;
            return this;
        }

        public TypeReferenceBuilder IsBaseType(bool isBaseType)
        {
            this.isBaseType = isBaseType;
            return this;
        }

        public TypeReferenceBuilder IsGeneric(bool isGeneric)
        {
            this.isGeneric = isGeneric;
            return this;
        }

        public TypeReferenceBuilder SetGeneric(TypeReference generic)
        {
            this.generic = generic;
            return this;
        }

        public TypeReferenceBuilder SetName(string name)
        {
            this.name = name;
            this.isNamedType = true;
            return this;
        }

        public TypeReferenceBuilder SetReferenceTypeXmiId(string xmiId)
        {
            this.referenceTypeXmiId = xmiId;
            return this;
        }

        public TypeReferenceBuilder IsCollection(bool isCollection)
        {
            this.isCollection = isCollection;
            return this;
        }

        public TypeReferenceBuilder IsNullable(bool isNullable)
        {
            this.isNullable = isNullable;
            return this;
        }

        public TypeReferenceBuilder IsComplexType(bool isComplexType)
        {
            this.isComplexType = isComplexType;
            return this;
        }

        public TypeReferenceBuilder IsPrimitive(bool isPrimitive)
        {
            this.isPrimitive = isPrimitive;
            return this;
        }
    }
}