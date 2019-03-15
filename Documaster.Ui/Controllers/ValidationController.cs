using System.Web.Mvc;
using Documaster.Business.Services;
using Documaster.Model.BaseEntities;
using Documaster.Model.Entities;

namespace Documaster.Ui.Controllers
{
    public class ValidationController : Controller
    {
        private readonly INamedEntityService<ProjectStatus> _namedEntityService;

        public ValidationController(INamedEntityService<ProjectStatus> namedEntityService)
        {
            _namedEntityService = namedEntityService;
        }

       
    }
}

