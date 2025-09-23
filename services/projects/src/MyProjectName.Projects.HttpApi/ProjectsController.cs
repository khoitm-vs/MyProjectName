using MyProjectName.Projects.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace MyProjectName.Projects;

public abstract class ProjectsController : AbpControllerBase
{
    protected ProjectsController()
    {
        LocalizationResource = typeof(ProjectsResource);
    }
}
