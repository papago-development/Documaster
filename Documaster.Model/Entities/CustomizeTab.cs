using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class CustomizeTab: NamedEntity, IOrderedEntity
    {
        [Remote("DoesNameExist", "CustomizeTab", AdditionalFields = "Id", ErrorMessage = "Numele exista deja")]
        public override string Name { get; set; }

        [Required(ErrorMessage = "Tipul nu poate fi gol")]
        public string Type { get; set; }

        [Remote("DoesNumberExist", "CustomizeTab", AdditionalFields = "Id", ErrorMessage = "Numarul exista deja")]
        [Required(ErrorMessage = "Numarul nu poate fi gol")]
        [Range(1, int.MaxValue, ErrorMessage = "Numerotarea incepe de la 1")]
        public int Number { get; set; }

        public int GetOrder()
        {
            return Number;
        }

        public void SetOrder(int value)
        {
            Number = value;
        }
    }
}
