using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.BaseEntities
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required]
        public string Name { get; set; }
    }
}
