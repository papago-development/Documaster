using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class ProjectTemplate: BaseEntity
    {
        public int ProjectId { get; set; }
        public int TemplateId { get; set; }
        public string Text { get; set; }
    }
}
