using Microsoft.Extensions.Configuration;

namespace polyclinic_project_tests;

public static class TestConnectionString
{
    public static string GetConnection(string test)
    {
        string c = Directory.GetCurrentDirectory();
        IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
        string connectionString = configuration.GetConnectionString(test)!;
        return connectionString;
    }
}