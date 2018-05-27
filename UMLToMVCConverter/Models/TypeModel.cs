namespace UMLToMVCConverter.Models
{
    using System.CodeDom;
    using System.Collections.Generic;

    public class TypeModel
    {
        public string Name { get; }

        public List<Property> PrimaryKeyAttributes { get; set; }

        public bool HasComplexKey => this.PrimaryKeyAttributes.Count > 1;

        public bool HasKey => this.PrimaryKeyAttributes.Count > 0;

        public string XmiID { get; set; }

        public Dictionary<string, Property> ForeignKeys { get; set; }

        public Dictionary<int, string> Literals { get; set; }

        public List<Method> Methods { get; set; }

        public List<Property> Properties { get; set; }

        public bool IsClass { get; set; }

        public bool IsStruct { get; set; }

        public bool IsEnum { get; set; }

        public string Visibility { get; set; }

        public bool IsAbstract { get; set; }

        public List<TypeModel> NestedClasses { get; set; }

        public List<CodeTypeReference> BaseTypes { get; private set; }

        public string BaseClassName => this.BaseTypes.Count > 0
            ? this.BaseTypes[0].BaseType
            : null;


        public TypeModel(string name, bool isClass, string visibility)
        {
            this.Name = name;
            this.IsClass = isClass;
            this.Visibility = visibility;
            this.InitializeCollections();
        }

        public TypeModel(string name)
        {
            this.Name = name;
            this.InitializeCollections();
        }

        private void InitializeCollections()
        {
            this.Methods = new List<Method>();
            this.Properties = new List<Property>();
            this.ForeignKeys = new Dictionary<string, Property>();
            this.PrimaryKeyAttributes = new List<Property>();
            this.Literals = new Dictionary<int, string>();
            this.BaseTypes = new List<CodeTypeReference>();
            this.NestedClasses = new List<TypeModel>();
        }
    }
}
