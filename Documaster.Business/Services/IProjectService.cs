using Documaster.Model.Entities;
using System.Linq;

namespace Documaster.Business.Services
{
    public interface IProjectService
    {
        IQueryable<Project> GetAllProjects();
        Project CreateProject(Project project);
        Project GetProjectById(int id);
        bool DeleteProject(Project project);
        bool UpdateProject(Project project);
        bool UpdateProjectNotes(Project project);
        bool DoesNameNumberCombinationExist(Project project);
    }
}
