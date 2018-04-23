using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Documaster.Model.BaseEntities;
using Newtonsoft.Json;

namespace Documaster.Model.Entities
{
    public class ProjectDocument : NamedEntity
    {
        public DateTime? Expire { get; set; }

        public int ProjectId { get; set; }

        public byte[] DocumentData { get; set; }

        public virtual Project Project { get; set; }

    }
}
