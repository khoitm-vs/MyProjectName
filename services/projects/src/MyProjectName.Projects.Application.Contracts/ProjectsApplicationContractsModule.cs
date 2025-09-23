using Volo.Abp.Application;
using Volo.Abp.Authorization;
using Volo.Abp.Modularity;

namespace MyProjectName.Projects;

[DependsOn(typeof(ProjectsDomainSharedModule))]
[DependsOn(typeof(AbpDddApplicationContractsModule))]
[DependsOn(typeof(AbpAuthorizationModule))]
public class ProjectsApplicationContractsModule : AbpModule { }
