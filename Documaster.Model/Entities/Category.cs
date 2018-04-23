using Documaster.Model.BaseEntities;
using System.Collections.Generic;


namespace Documaster.Model.Entities
{
    public class Category : NamedEntity
    {
        public virtual ICollection<Requirement> Requirements { get; set; } = new List<Requirement>();
    }
}
