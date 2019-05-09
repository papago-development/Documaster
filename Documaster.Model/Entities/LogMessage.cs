using System;
namespace Documaster.Model.Entities
{
    public class LogMessage
    {
        public int Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Level { get; set; }
        public string Logger { get; set; }
        public string Message { get; set; }
        public string CallSite { get; set; }
        public string Exception { get; set; }
    }
}
