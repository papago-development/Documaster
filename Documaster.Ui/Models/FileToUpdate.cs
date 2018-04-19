using Documaster.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Documaster.Ui.Models
{
    public class FileToUpdate
    {
        public int Id { get; set; }

        public string FileName { get; set; }
        public bool Status { get; set; }
        public string RequirementName { get; set; }
        public int ProjectId { get; set; }
        public int RequirementId { get; set; }
        public virtual List<OutputDocument> OutputDocuments { get; set; }
    }
}