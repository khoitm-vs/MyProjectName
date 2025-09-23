using Volo.Abp.Modularity;

namespace MyProjectName.SaaS;

[DependsOn(typeof(SaaSApplicationModule))]
[DependsOn(typeof(SaaSDomainTestModule))]
public class SaaSApplicationTestModule : AbpModule { }
