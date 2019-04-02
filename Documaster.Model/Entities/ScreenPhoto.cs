using System;
using Documaster.Model.BaseEntities;
namespace Documaster.Model.Entities
{
    public class ScreenPhoto : NamedEntity
    {
        public byte[] DocumentData { get; set; }
        public string ContentType { get; set; }
        public string DocumentType { get; set; }
        public bool IsSelected {get;set;}
    }
}
