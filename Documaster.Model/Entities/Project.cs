using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Project : NamedEntity
    {
        public int CustomerId { get; set; }

        public int ProjectTypeId { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual ProjectType ProjectType { get; set; }

        [JsonIgnore]
        public virtual ICollection<InputDocument> InputDocuments { get; set; } = new List<InputDocument>();

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
