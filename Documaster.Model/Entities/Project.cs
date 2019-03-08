using System;
using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Project : NamedEntity
    {
        public DateTime? Expire { get; set; }

        public byte[] ProjectData { get; set; }

        public string Notes { get; set; }

        public string Number { get; set; }

        public int ProjectStatusId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
