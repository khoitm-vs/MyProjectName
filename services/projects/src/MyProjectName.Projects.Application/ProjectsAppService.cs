using MyProjectName.Projects.Localization;
using Volo.Abp.Application.Services;

namespace MyProjectName.Projects;

public abstract class ProjectsAppService : ApplicationService
{
    protected ProjectsAppService()
    {
        LocalizationResource = typeof(ProjectsResource);
        ObjectMapperContext = typeof(ProjectsApplicationModule);
    }
}
