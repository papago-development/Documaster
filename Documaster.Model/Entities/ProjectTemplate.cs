using System.ComponentModel.DataAnnotations;
using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class ProjectTemplate : NamedEntity
    {
        public int ProjectId { get; set; }

        [Required(ErrorMessage = "Numele nu poate fi gol")]
        public override string Name { get; set; }

        public string Text { get; set; }
    }
}
