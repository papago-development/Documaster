using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public interface IProjectRequirementService
    {
        IEnumerable<ProjectRequirement> GetRequirementsById(int id);
        IEnumerable<ProjectRequirement> GetProjectRequirementByProjectId(int id);
        bool DeleteProjectRequirement(ProjectRequirement projectRequirement);
        IEnumerable<ProjectRequirement> GetListOfProjectRequirements(int id);

        ProjectRequirement CreateProjectRequirement(ProjectRequirement projectRequirement);
        ProjectRequirement GetProjectRequirementById(int id);
        bool UpdateProjectRequirement(ProjectRequirement projectRequirement);
        bool Save();
    }
}
