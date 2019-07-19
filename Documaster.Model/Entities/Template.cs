using Documaster.Model.BaseEntities;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Documaster.Model.Entities
{
    public class Template : NamedEntity
    {
        public string Text { get; set; }

        [Required(ErrorMessage = "Numele nu poate fi gol")]
        [Remote("DoesNameExist", "Template", AdditionalFields = "Id", ErrorMessage = "Numele exista deja")]
        public override string Name { get; set; }
    }
}
