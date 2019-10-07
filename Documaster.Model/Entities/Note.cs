using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class Note: BaseEntity
    {
        public int CustomizeTabId { get; set; }

        public int ProjectId { get; set; }

        public string Text { get; set; }

        public virtual Project Project { get; set; }

    }
}
