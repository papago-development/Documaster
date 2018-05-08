using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class Project : NamedEntity
    {
        public DateTime? Expire { get; set; }

        [Display(Name = "Project")]
        public byte[] ProjectData { get; set; }

        public virtual Customer Customer { get; set; }


        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();

    }
}
