using System;
using System.Collections.Generic;
using System.Linq;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public interface IProjectStatusService
    {
        IQueryable<ProjectStatus> GetAll();
        ProjectStatus CreateProjectStatus(ProjectStatus projectStatus);
        bool DeleteProjectStatus(ProjectStatus projectStatus);
        ProjectStatus GetProjectStatusById(int id);
        bool EditProjectStatus(ProjectStatus projectStatus);

    }
}
