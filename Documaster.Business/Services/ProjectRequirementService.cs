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

        public ProjectRequirementService(IGenericRepository<Requirement> projectRequirementRepository, IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _projectRequirementRepository = projectRequirementRepository;
        }

        public IEnumerable<ProjectRequirement> GetRequirementById(int id)
        {
            return _projectRequirementRepository.Get(x => x.ProjectId == id);
        }
    }
}
