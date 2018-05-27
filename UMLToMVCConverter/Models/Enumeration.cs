namespace UMLToMVCConverter.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Enumeration
    {
        public string Name { get; set; }

        public Dictionary<int, string> Literals { get; set; }

        public string LiteralsToObjectsStringEnumeration
        {
            get
            {
                var literalsToObjects = this.Literals
                    .Select(x => $"new {this.Name} {{ ID = {x.Key}, Name = \"{x.Value}\" }}").ToArray();
                return string.Join(",\r\n\t\t\t\t\t\t\t", literalsToObjects);
            }
        }
    }
}