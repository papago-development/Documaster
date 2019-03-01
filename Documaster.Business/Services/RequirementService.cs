using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Documaster.Model.Entities;

namespace Documaster.Business.Services
{
    public class RequirementService : IRequirementService
    {
        private readonly IGenericRepository<Requirement> _requirementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RequirementService(IGenericRepository<Requirement> requirementRepository,
                                  IUnitOfWork unitOfWork)
        {
            _requirementRepository = requirementRepository;
            _unitOfWork = unitOfWork;
        }

        public Requirement CreateRequirement(Requirement requirement)
        {
            var newRequirement = _requirementRepository.Create(requirement);
            _unitOfWork.SaveChanges();
            return newRequirement;
        }

        public Requirement GetRequirementById(int id)
        {
           return _requirementRepository.Get(id); 
        }

        public IEnumerable<Requirement> GetRequirements()
        {
            return _requirementRepository.GetAll().OrderBy(x => x.Category.Name).ThenBy(x => x.Name).ToList();
        }

        public bool UpdateRequirement(Requirement requirement)
        {
            var updateRequirement = _requirementRepository.Update(requirement, new List<string> { "Name", "CategoryId" });
            _unitOfWork.SaveChanges();
            return updateRequirement;
        }

        public bool DeleteRequirement(Requirement requirement)
        {
            var deletedRequirement = _requirementRepository.Delete(requirement.Id);
            _unitOfWork.SaveChanges();
            return deletedRequirement;
        }
    }
}
