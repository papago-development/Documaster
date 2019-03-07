using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Requirement : NamedEntity
    {
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        public int Number { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectRequirement> ProjectRequirements { get; set; } = new List<ProjectRequirement>();

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
