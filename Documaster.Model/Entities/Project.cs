using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Project : NamedEntity
    {
        [Column(TypeName = "datetime2")]
        public DateTime? Expire { get; set; }

        public byte[] ProjectData { get; set; }

        public virtual Customer Customer { get; set; }

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();

    }
}
