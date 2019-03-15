using Documaster.Model.BaseEntities;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Documaster.Model.Entities
{
    public class Category : NamedEntity
    {
        [Remote("DoesNumberExist", "Category", ErrorMessage = "Numarul exista deja")]
        public int Number { get; set; }

        [Remote("DoesNameExist", "Category", ErrorMessage = "Numele exista deja")]
        public override string Name { get; set; }
        public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    }
}
