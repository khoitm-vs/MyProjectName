using Microsoft.Extensions.Hosting;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace MyProjectName.Projects.EntityFrameworkCore;

[ConnectionStringName(MyProjectNameNames.ProjectsDb)]
public interface IProjectsDbContext : IEfCoreDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * DbSet<Question> Questions { get; }
     */
}
