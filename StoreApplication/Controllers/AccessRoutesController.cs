using Microsoft.AspNetCore.Mvc;
using StoreApplication.AutherizationExctencion;
using StoreApplication.Models;
using StoreApplication.ViewModels;

namespace StoreApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccessRoutesController : BaseController
    {
        private readonly ILogger<AccessRoutesController> _logger;
        private DataBaseContext _dataBaseContext;

        public AccessRoutesController(ILogger<AccessRoutesController> logger, DataBaseContext db):
            base(logger, db)
        {
            _logger = logger;
            _dataBaseContext = db;
        }

        [HttpGet]
        [APIMultiplePolicysAuthorize("UserIsAdmin")]
        public Object Get(int RouteId = 0)
        {
            return Status(new AccessRoutesViewModel(_dataBaseContext).GetAccessRoutes(RouteId));
        }

        //[HttpPost]
        //[APIMultiplePolicysAuthorize("UserIsAdmin")]
        //public Object Post([FromBody] Object dataItem)
        //{
        //    return Status(new AccessRoutesViewModel(_dataBaseContext).CreateAccessRoute(dataItem));
        //}

        //[HttpPut]
        //[APIMultiplePolicysAuthorize("UserIsAdmin")]
        //public Object Put([FromBody] Object dataItem, int RouteId)
        //{
        //    return Status(new AccessRoutesViewModel(_dataBaseContext).UpdateAccessRoutes(dataItem, RouteId));
        //}

        //[HttpDelete]
        //[APIMultiplePolicysAuthorize("UserIsAdmin")]
        //public Object Delete(int RouteId)
        //{
        //    return Status(new AccessRoutesViewModel(_dataBaseContext).DeleteAccessRoute(RouteId));
        //}

    }
}