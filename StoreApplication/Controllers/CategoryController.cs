using Microsoft.AspNetCore.Mvc;
using StoreApplication.AutherizationExctencion;
using StoreApplication.Models;
using StoreApplication.ViewModels;

namespace StoreApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : BaseController
    {
        private readonly ILogger<CategoryController> _logger;
        private DataBaseContext _dataBaseContext;

        public CategoryController(ILogger<CategoryController> logger, DataBaseContext db) :
            base(logger, db)
        {
            _logger = logger;
            _dataBaseContext = db;
        }

        [HttpGet]
        [APIMultiplePolicysAuthorize("UserIsAdmin;ViewCategories")]
        public Object Get(int CategoryId = 0)
        {
            return Status(new CategoryViewModel(_dataBaseContext).GetCategories(CategoryId));
        }

        [HttpPost]
        [APIMultiplePolicysAuthorize("UserIsAdmin;CreateCategory")]
        public Object Post([FromBody] Object dataItem)
        {
            return Status(new CategoryViewModel(_dataBaseContext).CreateCategory(dataItem));
        }

        [HttpPut]
        [APIMultiplePolicysAuthorize("UserIsAdmin;UpdateCategory")]
        public Object Put([FromBody] Object dataItem, int CategoryId)
        {
            return Status(new CategoryViewModel(_dataBaseContext).UpdateCategory(dataItem, CategoryId));
        }

        [HttpDelete]
        [APIMultiplePolicysAuthorize("UserIsAdmin;DeleteCategory")]
        public Object Delete(int CategoryId)
        {
            return Status(new CategoryViewModel(_dataBaseContext).DeleteCategory(CategoryId));
        }
    }
}
