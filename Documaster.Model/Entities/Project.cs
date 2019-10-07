using System;
using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Documaster.Model.Entities
{
    public class Project : NamedEntity
    {
        public DateTime? Expire { get; set; }

        public byte[] ProjectData { get; set; }

        public string Notes { get; set; }

        [Remote("DoesNameNumberCombinationExist", "Project", AdditionalFields = "Name, Id", ErrorMessage = "Aceasta combinatie Nume + Numar exista deja")]
        [Required(ErrorMessage = "Numarul nu poate fi gol")]
        public string Number { get; set; }

        public int ProjectStatusId { get; set; }

        [Remote("DoesNameNumberCombinationExist", "Project", AdditionalFields = "Number, Id", ErrorMessage = "Aceasta combinatie Nume + Numar exista deja")]
        [Required(ErrorMessage = "Numele nu poate fi gol")]
        public override string Name { get; set; }

        public string Address { get; set; }

        public DateTime Created { get; set; }

        public virtual Customer Customer { get; set; }

        public virtual ProjectStatus ProjectStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();

        public bool AllowNotification { get; set; }

        [NotMapped]
        public int ExpireYear { get; set; }

        [NotMapped]
        public int ExpireDay { get; set; }

        [NotMapped]
        public int ExpireMonth { get; set; }

        [NotMapped]
        public int CreatedYear { get; set; }

        [NotMapped]
        public int CreatedDay { get; set; }

        [NotMapped]
        public int CreatedMonth { get; set; }
    }
}
