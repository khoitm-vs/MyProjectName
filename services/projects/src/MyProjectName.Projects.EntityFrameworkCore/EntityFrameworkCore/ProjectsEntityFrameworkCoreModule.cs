using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore.SqlServer;
using Volo.Abp.Modularity;

namespace MyProjectName.Projects.EntityFrameworkCore;

[DependsOn(typeof(ProjectsDomainModule))]
[DependsOn(typeof(AbpEntityFrameworkCoreSqlServerModule))]
[DependsOn(typeof(MyProjectNameSharedModule))]
public class ProjectsEntityFrameworkCoreModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // https://www.npgsql.org/efcore/release-notes/6.0.html#opting-out-of-the-new-timestamp-mapping-logic
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

        Configure<AbpDbConnectionOptions>(options =>
        {
            options.Databases.Configure(MyProjectNameNames.ProjectsDb, db => { });
        });

        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });

        context.Services.AddAbpDbContext<ProjectsDbContext>(options =>
        {
            options.AddDefaultRepositories(true);
        });
    }
}
