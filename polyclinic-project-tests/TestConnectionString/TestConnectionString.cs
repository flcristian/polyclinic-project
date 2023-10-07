using Microsoft.Extensions.Configuration;

namespace polyclinic_project_tests.TestConnectionString;

public interface ITestConnectionString
{
    public static string GetConnection()
    {
        string c = Directory.GetCurrentDirectory();
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
        string connectionString = configuration.GetConnectionString("Test")!;
        return connectionString;
    }
}