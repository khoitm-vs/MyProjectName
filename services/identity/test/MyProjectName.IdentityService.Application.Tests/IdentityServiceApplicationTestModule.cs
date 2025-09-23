using Volo.Abp.Modularity;

namespace MyProjectName.IdentityService;

[DependsOn(typeof(IdentityServiceApplicationModule))]
[DependsOn(typeof(IdentityServiceDomainTestModule))]
public class IdentityServiceApplicationTestModule : AbpModule { }
