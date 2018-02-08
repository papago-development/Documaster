using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Customer : NamedEntity
    {
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    }
}
