namespace UMLToMVCConverter.ExtensionMethods
{
    using Newtonsoft.Json.Linq;

    public static class JObjectExtensions
    {
        public static void AddOrUpdate(this JObject jObject, JProperty property)
        {
            if (jObject.ContainsKey(property.Name))
            {
                jObject[property.Name] = property.Value;
            }
            else
            {
                jObject.Add(property);
            }
        }
    }
}
