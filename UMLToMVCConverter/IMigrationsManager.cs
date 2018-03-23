namespace UMLToMVCConverter
{
    using System.Reflection;

    public interface IMigrationsManager
    {
        void AddAndRunMigrations(Assembly mvcProjectAssembly);
    }
}