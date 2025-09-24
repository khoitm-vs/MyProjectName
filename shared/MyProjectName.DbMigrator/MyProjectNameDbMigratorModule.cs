using Microsoft.EntityFrameworkCore;
using MyProjectName.Administration;
using MyProjectName.Administration.EntityFrameworkCore;
using MyProjectName.IdentityService;
using MyProjectName.IdentityService.EntityFrameworkCore;
using MyProjectName.Projects;
using MyProjectName.Projects.EntityFrameworkCore;
using MyProjectName.SaaS;
using MyProjectName.SaaS.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.OpenIddict.Tokens;

namespace MyProjectName.DbMigrator;

[DependsOn(typeof(AbpAutofacModule))]
[DependsOn(typeof(AbpBackgroundJobsAbstractionsModule))]
[DependsOn(typeof(AdministrationEntityFrameworkCoreModule))]
[DependsOn(typeof(AdministrationApplicationContractsModule))]
[DependsOn(typeof(IdentityServiceEntityFrameworkCoreModule))]
[DependsOn(typeof(IdentityServiceApplicationContractsModule))]
[DependsOn(typeof(ProjectsEntityFrameworkCoreModule))]
[DependsOn(typeof(ProjectsApplicationContractsModule))]
[DependsOn(typeof(SaaSEntityFrameworkCoreModule))]
[DependsOn(typeof(SaaSApplicationContractsModule))]
//[DependsOn(typeof(WebAppEntityFrameworkCoreModule))]
//[DependsOn(typeof(WebAppApplicationContractsModule))]
public class MyProjectNameDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        Configure<TokenCleanupOptions>(options => options.IsCleanupEnabled = false);
        var configuration = context.Services.GetConfiguration();

        Configure<AbpDbContextOptions>(options =>
        {
            // C?u hình DbContext m?c ??nh (n?u có)
            options.Configure(context =>
            {
                context.DbContextOptions.UseSqlServer();
            });

            // C?u hình cho t?ng DbContext c?a microservice
            options.Configure<AdministrationDbContext>(context =>
            {
                context.DbContextOptions.UseSqlServer(configuration.GetConnectionString(AdministrationDbProperties.ConnectionStringName));
            });

            options.Configure<IdentityServiceDbContext>(context =>
            {
                context.DbContextOptions.UseSqlServer(configuration.GetConnectionString(IdentityServiceDbProperties.ConnectionStringName));
            });

            options.Configure<SaaSDbContext>(context =>
            {
                context.DbContextOptions.UseSqlServer(configuration.GetConnectionString(SaaSDbProperties.ConnectionStringName));
            });

            options.Configure<ProjectsDbContext>(context =>
            {
                context.DbContextOptions.UseSqlServer(configuration.GetConnectionString(ProjectsDbProperties.ConnectionStringName));
            });
        });
    }
}
