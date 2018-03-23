namespace UMLToMVCConverter
{
    public interface IStartupCsConfigurator
    {
        void SetUpStartupCsDbContextUse(string contextName);
    }
}