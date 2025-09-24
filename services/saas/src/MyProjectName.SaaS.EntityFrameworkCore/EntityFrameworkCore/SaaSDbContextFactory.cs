using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace MyProjectName.SaaS.EntityFrameworkCore;

public class SaaSDbContextFactory : IDesignTimeDbContextFactory<SaaSDbContext>
{
    public SaaSDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();

        var builder = new DbContextOptionsBuilder<SaaSDbContext>().UseSqlServer(
            GetConnectionStringFromConfiguration()
        );

        return new SaaSDbContext(builder.Options);
    }

    private static string GetConnectionStringFromConfiguration()
    {
        return BuildConfiguration().GetConnectionString(SaaSDbProperties.ConnectionStringName);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../MyProjectName.SaaS.HttpApi.Host/"))
        .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
