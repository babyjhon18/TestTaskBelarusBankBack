using Microsoft.AspNetCore.Mvc;
using StoreApplication.AutherizationExctencion;
using StoreApplication.Models;
using StoreApplication.ViewModels;

namespace StoreApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : BaseController
    {
        private readonly ILogger<ProductController> _logger;
        private DataBaseContext _dataBaseContext;

        public ProductController(ILogger<ProductController> logger, DataBaseContext db) :
            base(logger, db)
        {
            _logger = logger;
            _dataBaseContext = db;
        }

        [HttpGet]
        [APIMultiplePolicysAuthorize("UserIsAdmin;ViewProduct")]
        public Object Get(int ProductId = 0)
        {
            return Status(new ProductsViewModel(_dataBaseContext).GetProducts(ProductId));
        }

        [HttpPost]
        [APIMultiplePolicysAuthorize("UserIsAdmin;CreateProduct")]
        public Object Post([FromBody] Object dataItem)
        {
            return Status(new ProductsViewModel(_dataBaseContext).CreateProduct(dataItem));
        }

        [HttpPut]
        [APIMultiplePolicysAuthorize("UserIsAdmin;UpdateProduct")]
        public Object Put([FromBody] Object dataItem, int ProductId)
        {
            return Status(new ProductsViewModel(_dataBaseContext).UpdateProducts(dataItem, ProductId));
        }

        [HttpDelete]
        [APIMultiplePolicysAuthorize("UserIsAdmin;DeleteProduct")]
        public Object Delete(int ProductId)
        {
            return Status(new ProductsViewModel(_dataBaseContext).DeleteProduct(ProductId));
        }
    }
}
