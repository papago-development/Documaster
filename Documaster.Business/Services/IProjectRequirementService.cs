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
        IEnumerable<ProjectRequirement> GetRequirementById(int id);
    }
}
