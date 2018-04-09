namespace UMLToMVCConverter
{
    public class Application
    {
        private readonly IDataModelGenerator dataModelGenerator;

        public Application(IDataModelGenerator dataModelGenerator)
        {
            this.dataModelGenerator = dataModelGenerator;
        }

        public void Run()
        {
            this.dataModelGenerator.GenerateMvcFiles();
        }
    }
}
