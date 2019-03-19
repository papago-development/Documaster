using System;
using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.Entities
{
    public class Project : NamedEntity
    {
        public DateTime? Expire { get; set; }

        public byte[] ProjectData { get; set; }

        public string Notes { get; set; }

        [Remote("DoesNameExistWithNumber", "Project", AdditionalFields = "Name", ErrorMessage = "Numarul este deja asignat unui proiect")]
        [Required(ErrorMessage = "Campul Numar nu poate fi null.")]
        public string Number { get; set; }

        public int ProjectStatusId { get; set; }

        [Remote("", "", AdditionalFields = "Number", ErrorMessage = "Numele exista deja")]
        [Required(ErrorMessage = "Campul Nume nu poate fi null.")]
        public override string Name { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
