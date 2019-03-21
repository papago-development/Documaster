using System.Collections.Generic;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public interface IProjectStatusService
    {
        IEnumerable<ProjectStatus> GetAll();
        ProjectStatus CreateProjectStatus(ProjectStatus projectStatus);
        bool DeleteProjectStatus(ProjectStatus projectStatus);
        ProjectStatus GetProjectStatusById(int id);
        bool EditProjectStatus(ProjectStatus projectStatus);

    }
}
