using Documaster.Model.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Documaster.Model.Entities
{
    public class AdditionalDocument : NamedEntity
    {
     //   public int Id { get; set; }
        public int ProjectId { get; set; }

        public string ContentType { get; set; }

        public byte[] DocumentData { get; set; }
    }
}
