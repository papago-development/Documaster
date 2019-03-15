using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Documaster.Model.BaseEntities
{
    public abstract class NamedEntity : BaseEntity
    {
        [Required]
        public virtual string Name { get; set; }
    }
}
