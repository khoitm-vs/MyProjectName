using Serilog;
using MyProjectName.Administration.EntityFrameworkCore;
using MyProjectName.Projects.EntityFrameworkCore;
using MyProjectName.SaaS.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;

namespace MyProjectName.DbMigrator;

internal class Program
{
    private static async Task Main(string[] args)
    {
        MyProjectNameLogging.Initialize();

        var builder = Host.CreateApplicationBuilder(args);

        builder.AddServiceDefaults();

        builder.AddNpgsqlDbContext<AdministrationDbContext>(
            connectionName: MyProjectNameNames.AdministrationDb
        );
        builder.AddNpgsqlDbContext<IdentityDbContext>(connectionName: MyProjectNameNames.IdentityServiceDb);
        builder.AddNpgsqlDbContext<SaaSDbContext>(connectionName: MyProjectNameNames.SaaSDb);
        builder.AddNpgsqlDbContext<ProjectsDbContext>(connectionName: MyProjectNameNames.ProjectsDb);

        builder.Configuration.AddAppSettingsSecretsJson();

        builder.Logging.AddSerilog();

        builder.Services.AddHostedService<DbMigratorHostedService>();

        var host = builder.Build();

        await host.RunAsync();
    }
}
