using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.Entities
{
    public class Requirement : NamedEntity
    { 
        [Remote("DoesNumberExistWithName", "Requirement", AdditionalFields = "Number", ErrorMessage = "Categorie exista deja")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Remote("DoesNumberExistWithName", "Requirement", AdditionalFields = "CategoryId", ErrorMessage = "Numarul este deja asignat unei categorii")]
        [Required(ErrorMessage = "Campul Numar nu poate fi null.")]
        public int Number { get; set; }

        [Remote("DoesNameExist", "Requirement", ErrorMessage = "Numele exista deja")]
        [Required(ErrorMessage = "Campul Cerinta nu poate fi null.")]
        public override string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectRequirement> ProjectRequirements { get; set; } = new List<ProjectRequirement>();

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
