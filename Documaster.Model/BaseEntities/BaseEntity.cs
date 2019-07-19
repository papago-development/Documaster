using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.BaseEntities
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }

        [ReadOnly( true )]
        public DateTime CreationDate { get; set; }

        [Editable( false )]
        public DateTime LastUpdate { get; set; }
    }
}