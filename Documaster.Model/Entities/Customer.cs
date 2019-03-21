using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Documaster.Model.BaseEntities;

namespace Documaster.Model.Entities
{
    public class Customer : NamedEntity
    {
        public Customer()
        {
            LastUpdate = DateTime.Now;
            CreationDate = DateTime.Now;
        }

        [Key, ForeignKey("Project")]
        public override int Id { get; set; }

        [StringLength(100, ErrorMessage = "Campul nu poate contine mai mult de 100 de caractere")]
        public string Address { get; set; }
        public string Telephone { get; set; }

        [EmailAddress(ErrorMessage = "Adresa de Email nu este valida")]
        public string Email { get; set; }
        public string AdditionalInfo1 { get; set; }

        [StringLength(100, ErrorMessage = "Campul nu poate contine mai mult de 100 de caractere")]
        public string AdditionalInfo2 { get; set; }
        public Project Project { get; set; }
    }
}
