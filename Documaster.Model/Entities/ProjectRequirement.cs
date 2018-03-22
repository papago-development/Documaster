using Documaster.Model.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Model.Entities
{
    public class ProjectRequirement : BaseEntity
    {
        public int ProjectId { get; set; }
        public int RequirementId { get; set; }

        public Project Project { get; set; }
        public Requirement Requirement { get; set; }

    }
}
