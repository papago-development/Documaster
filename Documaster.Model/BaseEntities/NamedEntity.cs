using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.BaseEntities
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required(ErrorMessage = "Numele nu poate fi gol")]
        public virtual string Name { get; set; }
    }
}
