using Documaster.Model.Entities;
using System.Collections.Generic;

namespace Documaster.Ui.Models
{
    public class FileToUpdate
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public bool IsReady { get; set; }
        public string RequirementName { get; set; }
        public int ProjectId { get; set; }
        public int RequirementId { get; set; }
        public int ProjectRequirementId { get; set; }
        public string CategoryName { get; set; }
        public virtual List<OutputDocument> OutputDocuments { get; set; }
    }
}