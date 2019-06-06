using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Documaster.Model.Enums
{
    public enum DocumentType
    {
        [Display(Name = "Documente")]
        DisplayDocuments = 0,

        [Display(Name = "Note")]
        Notes = 1,

        [Display(Name = "Avize")]
        OutputDocuments = 2
    }
}
