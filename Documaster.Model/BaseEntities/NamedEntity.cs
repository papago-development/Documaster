using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.BaseEntities
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required(ErrorMessage = "Numele nu poate fi gol")]
        [StringLength(1000, ErrorMessage = "Campul nu poate contine mai mult de 1000 de caractere")]
        public virtual string Name { get; set; }
    }
}
