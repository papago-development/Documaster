using System.Collections.Generic;

namespace Documaster.Ui.Models
{
    public class ProjectTypeViewModel : Model.Entities.ProjectType
    {
        public IEnumerable<int> ProjectTypeRequirementIds { get; set; }
    }
}