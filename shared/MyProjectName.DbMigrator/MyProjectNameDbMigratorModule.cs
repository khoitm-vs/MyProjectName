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
    }
}
