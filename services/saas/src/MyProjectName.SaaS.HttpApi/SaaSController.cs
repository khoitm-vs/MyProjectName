using MyProjectName.SaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyProjectName.SaaS;

public abstract class SaaSController : AbpControllerBase
{
    protected SaaSController()
    {
        LocalizationResource = typeof(SaaSResource);
    }
}
