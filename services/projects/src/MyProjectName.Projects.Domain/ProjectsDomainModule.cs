using Volo.Abp.Domain;
using Volo.Abp.Modularity;

namespace MyProjectName.Projects;

[DependsOn(typeof(AbpDddDomainModule))]
[DependsOn(typeof(ProjectsDomainSharedModule))]
public class ProjectsDomainModule : AbpModule { }
