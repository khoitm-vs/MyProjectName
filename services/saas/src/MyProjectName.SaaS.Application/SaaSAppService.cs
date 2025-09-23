using MyProjectName.SaaS.Localization;
using Volo.Abp.Application.Services;

namespace MyProjectName.SaaS;

public abstract class SaaSAppService : ApplicationService
{
    protected SaaSAppService()
    {
        LocalizationResource = typeof(SaaSResource);
        ObjectMapperContext = typeof(SaaSApplicationModule);
    }
}
