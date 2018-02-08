using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class ProjectTypeRequirement : BaseEntity
    {
        public int ProjectTypeId { get; set; }
        public int RequirementId { get; set; }

        public ProjectType ProjectType { get; set; }
        public Requirement Requirement { get; set; }
    }
}
