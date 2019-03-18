using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Business.Services
{
    public interface IRequirementService
    {
        IEnumerable<Requirement> GetRequirements();
        Requirement CreateRequirement(Requirement requirement);
        Requirement GetRequirementById(int id);
        IQueryable<Requirement> GetAll();
        Requirement GetRequirementByNumber(int number);
        bool UpdateRequirement(Requirement requirement);
        bool DeleteRequirement(Requirement requirement);
    }
}
