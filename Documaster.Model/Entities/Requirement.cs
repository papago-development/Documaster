using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Requirement : NamedEntity
    {
        [JsonIgnore]
        public virtual ICollection<ProjectTypeRequirement> ProjectTypeRequirements { get; set; } = new List<ProjectTypeRequirement>();

        [JsonIgnore]
        public virtual ICollection<InputDocument> InputDocuments { get; set; } = new List<InputDocument>();

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
