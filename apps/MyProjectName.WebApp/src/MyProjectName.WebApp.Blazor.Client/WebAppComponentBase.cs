using MyProjectName.WebApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace MyProjectName.WebApp.Blazor.Client;

public abstract class WebAppComponentBase : AbpComponentBase
{
    protected WebAppComponentBase()
    {
        LocalizationResource = typeof(WebAppResource);
    }
}
