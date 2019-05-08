using System;
using System.Web.Mvc;
using Documaster.Model.Entities;
using Documaster.Data.DataAccess;
namespace Documaster.Business.Services
{
    public class ExceptionHandlerAttribute: FilterAttribute, IExceptionFilter
    {
        private readonly IDbContext _dbContext;
        public ExceptionHandlerAttribute(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled)
            {
                ExceptionLogger logger = new ExceptionLogger()
                {
                    ExceptionMessage = filterContext.Exception.Message,
                    ExceptionStackTrace = filterContext.Exception.StackTrace,
                    ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                    LogTime = DateTime.Now
                };

                _dbContext.(logger);

            }
        }
    }
}
