using System.Collections.Generic;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.Entities
{
    public class Requirement : NamedEntity
    {
        [Remote("DoesCategoryNumberCombinationExist", "Requirement", AdditionalFields = "Number, Id")]
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }

        [Remote("DoesCategoryNumberCombinationExist", "Requirement", AdditionalFields = "CategoryId, Id", ErrorMessage = "Acest numar exista deja in cadrul categoriei")]
        [Required(ErrorMessage = "Numarul nu poate fi gol")]
        [Range(1, int.MaxValue, ErrorMessage = "Numerotarea incepe de la 1")]
        public int Number { get; set; }

        [Remote("DoesNameExist", "Requirement", AdditionalFields = "Id", ErrorMessage = "Numele exista deja")]
        [Required(ErrorMessage = "Numele nu poate fi gol")]
        public override string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProjectRequirement> ProjectRequirements { get; set; } = new List<ProjectRequirement>();

        [JsonIgnore]
        public virtual ICollection<OutputDocument> OutputDocuments { get; set; } = new List<OutputDocument>();
    }
}
