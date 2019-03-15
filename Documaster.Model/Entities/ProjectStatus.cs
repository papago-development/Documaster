using System;
using Documaster.Model.BaseEntities;
using System.Web.Mvc;

namespace Documaster.Model.Entities
{
    public class ProjectStatus: NamedEntity
    {
       [Remote("DoesNameExist", "ProjectStatus", ErrorMessage = "Numele exista deja")]
       public override string Name { get; set; }

       public string Color { get; set; }
    }
}
