using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class ProjectRequirement : BaseEntity
    {
        public int ProjectId { get; set; }
        public int RequirementId { get; set; }
        public bool IsReady { get; set; }

        public virtual Project Project { get; set; }
        public virtual Requirement Requirement { get; set; }

    }
}
