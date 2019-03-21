using Documaster.Model.BaseEntities;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Documaster.Model.Entities
{
    public class Category : NamedEntity
    {
        [Remote("DoesNumberExist", "Category", AdditionalFields = "Id", ErrorMessage = "Numarul exista deja")]
        [Required(ErrorMessage = "Numarul nu poate fi gol")]
        [Range(1, int.MaxValue, ErrorMessage = "Numerotarea incepe de la 1")]
        public int Number { get; set; }

        [Remote("DoesNameExist", "Category", AdditionalFields = "Id", ErrorMessage = "Numele exista deja")]
        public override string Name { get; set; }

        public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    }
}
