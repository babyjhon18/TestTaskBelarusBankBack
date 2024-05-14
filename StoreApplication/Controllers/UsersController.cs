using Microsoft.AspNetCore.Mvc;
using StoreApplication.AutherizationExctencion;
using StoreApplication.Models;
using StoreApplication.ViewModels;

namespace StoreApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : BaseController
    {
        private readonly ILogger<UsersController> _logger;
        private DataBaseContext _dataBaseContext;

        public UsersController(ILogger<UsersController> logger, DataBaseContext db) :
            base(logger, db)
        {
            _logger = logger;
            _dataBaseContext = db;
        }

        [HttpGet]
        [APIMultiplePolicysAuthorize("UserIsAdmin;ViewUsers")]
        public Object Get(int UserId = 0)
        {
            return Status(new UserViewModel(_dataBaseContext).GetUsers(UserId));
        }

        [HttpPost]
        [APIMultiplePolicysAuthorize("UserIsAdmin;CreateUser")]
        public Object Post([FromBody] Object dataItem)
        {
            return Status(new UserViewModel(_dataBaseContext).CreateUser(dataItem));
        }

        [HttpPut]
        [APIMultiplePolicysAuthorize("UserIsAdmin;UpdateUser")]
        public Object Put([FromBody] Object dataItem, int UserId)
        {
            return Status(new UserViewModel(_dataBaseContext).UpdateUsers(dataItem, UserId));
        }

        [HttpDelete]
        [APIMultiplePolicysAuthorize("UserIsAdmin;DeleteUser")]
        public Object Delete(int UserId)
        {
            return Status(new UserViewModel(_dataBaseContext).DeleteUser(UserId));
        }

    }
}
