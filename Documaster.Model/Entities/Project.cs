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

        [Remote("DoesNameNumberCombinationExist", "Project", AdditionalFields = "Name, Id", ErrorMessage = "Aceasta combinatie Nume + Numar exista deja")]
        [Required(ErrorMessage = "Numarul nu poate fi gol")]
        public string Number { get; set; }

        public int ProjectStatusId { get; set; }

        [Remote("DoesNameNumberCombinationExist", "Project", AdditionalFields = "Number, Id", ErrorMessage = "Aceasta combinatie Nume + Numar exista deja")]
        [Required(ErrorMessage = "Numele nu poate fi gol")]
         public override string Name { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Created { get; set; }
         
        public virtual Customer Customer { get; set; }
        public virtual ProjectStatus ProjectStatus { get; set; }

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
