using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class ProjectType : NamedEntity
    {
        public string Text { get; set; }


        [JsonIgnore]
        public virtual ICollection<Project> Projects { get; set; } = new List<Project>();

        [JsonIgnore]
        public virtual ICollection<ProjectTypeRequirement> ProjectTypeRequirements { get; set; } = new List<ProjectTypeRequirement>();
    }
}
