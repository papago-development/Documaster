using System.Collections.Generic;

namespace Documaster.Ui.Models
{
    public class AssignedCategory
    {
        public int Id { get; set; }
      
        public string Name { get; set; }
     
        public  IList<AssignedRequirement> AssignedRequirements { get; set; } = new List<AssignedRequirement>();
    }
}