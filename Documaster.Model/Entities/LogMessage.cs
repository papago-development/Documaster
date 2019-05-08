using System;
namespace Documaster.Model.Entities
{
    public class LogMessage
    {
        public int Id { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public string Message { get; set; }
        public DateTime LogTime { get; set; }
    }
}
