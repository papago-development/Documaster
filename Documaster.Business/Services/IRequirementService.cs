using Documaster.Model.Entities;
using System.Collections.Generic;

namespace Documaster.Business.Services
{
    public interface IRequirementService
    {
        IEnumerable<Requirement> GetRequirements();
        Requirement CreateRequirement(Requirement requirement);
        Requirement GetRequirementById(int id);
        bool UpdateRequirement(Requirement requirement);
        bool DeleteRequirement(Requirement requirement);
        bool DoesCategoryNumberCombinationExist(Requirement requirement);
    }
}
