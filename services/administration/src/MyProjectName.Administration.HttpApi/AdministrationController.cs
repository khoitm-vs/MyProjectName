using MyProjectName.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyProjectName.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}
