using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Logging;
using MyProjectName.Administration.EntityFrameworkCore;
using MyProjectName.MultiTenancy;
using MyProjectName.SaaS.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc.UI.MultiTenancy;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Modularity;
using Volo.Abp.VirtualFileSystem;

namespace MyProjectName.SaaS;

[DependsOn(typeof(AbpAspNetCoreMvcUiMultiTenancyModule))]
[DependsOn(typeof(AdministrationEntityFrameworkCoreModule))]
[DependsOn(typeof(SaaSApplicationModule))]
[DependsOn(typeof(SaaSEntityFrameworkCoreModule))]
[DependsOn(typeof(SaaSHttpApiModule))]
[DependsOn(typeof(MyProjectNameMicroserviceModule))]
[DependsOn(typeof(MyProjectNameServiceDefaultsModule))]
public class SaaSHttpApiHostModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        var hostingEnvironment = context.Services.GetHostingEnvironment();
        var configuration = context.Services.GetConfiguration();
        Configure<AbpDbContextOptions>(options =>
        {
            options.UseSqlServer();
        });
        context.ConfigureMicroservice(MyProjectNameNames.SaaSApi);

        if (hostingEnvironment.IsDevelopment())
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                options.FileSets.ReplaceEmbeddedByPhysical<SaaSDomainSharedModule>(
                    Path.Combine(
                        hostingEnvironment.ContentRootPath,
                        string.Format(
                            "..{0}..{0}src{0}MyProjectName.SaaS.Domain.Shared",
                            Path.DirectorySeparatorChar
                        )
                    )
                );
                options.FileSets.ReplaceEmbeddedByPhysical<SaaSDomainModule>(
                    Path.Combine(
                        hostingEnvironment.ContentRootPath,
                        string.Format(
                            "..{0}..{0}src{0}MyProjectName.SaaS.Domain",
                            Path.DirectorySeparatorChar
                        )
                    )
                );
                options.FileSets.ReplaceEmbeddedByPhysical<SaaSApplicationContractsModule>(
                    Path.Combine(
                        hostingEnvironment.ContentRootPath,
                        string.Format(
                            "..{0}..{0}src{0}MyProjectName.SaaS.Application.Contracts",
                            Path.DirectorySeparatorChar
                        )
                    )
                );
                options.FileSets.ReplaceEmbeddedByPhysical<SaaSApplicationModule>(
                    Path.Combine(
                        hostingEnvironment.ContentRootPath,
                        string.Format(
                            "..{0}..{0}src{0}MyProjectName.SaaS.Application",
                            Path.DirectorySeparatorChar
                        )
                    )
                );
            });
        }
    }

    public override void OnApplicationInitialization(ApplicationInitializationContext context)
    {
        IdentityModelEventSource.ShowPII = true;
        var app = context.GetApplicationBuilder();
        var env = context.GetEnvironment();

        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseAbpRequestLocalization();
        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseCorrelationId();
        app.MapAbpStaticAssets();
        app.UseRouting();
        app.UseCors();
        app.UseAuthentication();

        if (MultiTenancyConsts.IsEnabled)
        {
            app.UseMultiTenancy();
        }

        app.UseUnitOfWork();
        app.UseDynamicClaims();
        app.UseAuthorization();
        app.UseSwagger();
        app.UseAbpSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "SaaS API");
            var configuration = context.GetConfiguration();
            options.OAuthClientId(configuration["AuthServer:SwaggerClientId"]);
            options.OAuthClientSecret(configuration["AuthServer:SwaggerClientSecret"]);
        });

        app.UseAuditing();
        app.UseAbpSerilogEnrichers();
        app.UseConfiguredEndpoints();
    }
}
