using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.BaseEntities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

        //[ReadOnly( true )]
        [DisplayFormat(DataFormatString = "{0:dd/mm/yyyy}")]
        [Editable(true)]
        public DateTime CreationDate { get; set; }

        [Editable( false )]
        public DateTime LastUpdate { get; set; }
    }
}