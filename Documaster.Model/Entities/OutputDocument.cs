using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class OutputDocument : NamedEntity
    {
        public int ProjectId { get; set; }

        public int? RequirementId { get; set; }

        public byte[] DocumentData { get; set; }

        public string ContentType { get; set; }

        public string DocumentType { get; set; }

        public virtual Project Project { get; set; }

        public virtual Requirement Requirement { get; set; }
    }
}
