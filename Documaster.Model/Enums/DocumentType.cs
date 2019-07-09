using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Documaster.Model.Enums
{

    [JsonConverter(typeof(StringEnumConverter))]
    public enum DocumentType
    {
        [Display(Name = "Documente")]
        DisplayDocuments = 0,

        [Display(Name = "Note")]
        Notes = 1,

        [Display(Name = "Avize")]
        OutputDocuments = 2,

        [Display(Name = "Sabloane")]
        DisplayTemplate = 3
    }
}
