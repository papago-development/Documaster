//using System.Collections.Generic;
//using System.Web.Mvc;
//using Documaster.Business.Services;
//using Documaster.Model.Entities;

//namespace Documaster.Ui.Controllers
//{
//    public class CustomerController : Controller
//    {
//        private readonly IGenericEntityService<Customer> _entityService;

//        public CustomerController( IGenericEntityService<Customer> entityService )
//        {
//            _entityService = entityService;
//        }

//        [HttpGet]
//        public ActionResult Index()
//        {
//            var model = _entityService.GetAll();
//            return View( model );
//        }

//        [HttpGet]
//        public ActionResult Create()
//        {
//            return View();
//        }

//        [HttpPost]
//        public ActionResult Create( Customer customer )
//        {
//            _entityService.Create( customer );

//            return RedirectToAction( "Index" );
//        }

//        [HttpGet]
//        public ActionResult Edit( int id )
//        {
//            var model = _entityService.Get( id );
//            return View( model );
//        }

//        [HttpPost]
//        public ActionResult Edit( Customer customer )
//        {
//            _entityService.Update( customer, new List<string> {"Name", "Address"} );
//            return RedirectToAction( "Index" );
//        }

//        [HttpGet]
//        public ActionResult Delete( int id )
//        {
//            var model = _entityService.Get( id );
//            return View( model );
//        }

//        [HttpPost]
//        public ActionResult Delete( Customer customer )
//        {
//            _entityService.Delete( customer.Id );
//            return RedirectToAction( "Index" );
//        }
//    }
//}