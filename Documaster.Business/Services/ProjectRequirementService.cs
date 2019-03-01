using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class ProjectRequirementService : IProjectRequirementService
    {
        private readonly IGenericRepository<ProjectRequirement> _projectRequirementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProjectRequirementService(IGenericRepository<ProjectRequirement> projectRequirementRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRequirementRepository = projectRequirementRepository;
        }

        public IEnumerable<ProjectRequirement> GetRequirementsById(int id)
        {
            return _projectRequirementRepository.Get(x => x.ProjectId == id);
        }

        public IEnumerable<ProjectRequirement> GetProjectRequirementByProjectId(int id)
        {
            return _projectRequirementRepository.Get(x => x.ProjectId == id);
        }

        public bool DeleteProjectRequirement(ProjectRequirement projectRequirement)
        {
            return _projectRequirementRepository.Delete(projectRequirement.Id);
        }

        public IEnumerable<ProjectRequirement> GetListOfProjectRequirements(int id)
        {
            return _projectRequirementRepository.Get(x => x.ProjectId == id).ToList();
        }

        public ProjectRequirement CreateProjectRequirement(ProjectRequirement projectRequirement)
        {
            return _projectRequirementRepository.Create(projectRequirement);
        }

        public bool Save()
        {
            return _unitOfWork.SaveChanges();
        }

        public ProjectRequirement GetProjectRequirementById(int id)
        {
            return _projectRequirementRepository.Get(id);
        }

        public bool UpdateProjectRequirement(ProjectRequirement projectRequirement)
        {
            var updateProjectRequirement = _projectRequirementRepository
                .Update(projectRequirement, new List<string> { "IsReady" });
            _unitOfWork.SaveChanges();

            return updateProjectRequirement;
        }
    }
}
