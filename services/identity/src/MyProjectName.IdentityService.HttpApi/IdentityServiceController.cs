using MyProjectName.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyProjectName.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}
