using MyProjectName.Administration.Localization;
using Volo.Abp.Application.Services;

namespace MyProjectName.Administration;

public abstract class AdministrationAppService : ApplicationService
{
    protected AdministrationAppService()
    {
        LocalizationResource = typeof(AdministrationResource);
        ObjectMapperContext = typeof(AdministrationApplicationModule);
    }
}
