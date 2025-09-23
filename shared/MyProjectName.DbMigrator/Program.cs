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

        builder.AddSqlServerDbContext<AdministrationDbContext>(
            connectionName: MyProjectNameNames.AdministrationDb
        );
        builder.AddSqlServerDbContext<IdentityDbContext>(connectionName: MyProjectNameNames.IdentityServiceDb);
        builder.AddSqlServerDbContext<SaaSDbContext>(connectionName: MyProjectNameNames.SaaSDb);
        builder.AddSqlServerDbContext<ProjectsDbContext>(connectionName: MyProjectNameNames.ProjectsDb);

        builder.Configuration.AddAppSettingsSecretsJson();

        builder.Logging.AddSerilog();

        builder.Services.AddHostedService<DbMigratorHostedService>();

        var host = builder.Build();

        await host.RunAsync();
    }
}
