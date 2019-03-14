using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public interface IProjectService
    {
        IQueryable<Project> GetAllProjects();
        Project CreateProject(Project project);
        Project GetProjectById(int id);
        bool DeleteProject(Project project);
        bool UpdateProject(Project project);
    }
}
