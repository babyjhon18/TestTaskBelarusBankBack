using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoreApplication.Models;

namespace StoreApplication.Controllers
{
    public class BaseController : Controller
    {
        public DataBaseContext db;
        private readonly ILogger _logger;
        public BaseController(ILogger logger, DataBaseContext context)
        {
            _logger = logger;
            db = context;
        }

        public object Status(dynamic dataItem)
        {
            try
            {
                if (dataItem is Boolean)
                {
                    if (Convert.ToBoolean(dataItem))
                    {
                        _logger.LogInformation("Entity added successfully at {DT}", DateTime.UtcNow.ToLongTimeString());
                        Response.StatusCode = 200;
                    }
                    else
                    {
                        _logger.LogWarning("Entity not modified at {DT}", DateTime.UtcNow.ToLongTimeString());
                        Response.StatusCode = 304;
                    }
                }
                else
                {
                    if (dataItem == null)
                    {
                        Response.StatusCode = 400;
                        _logger.LogError("Not found at {DT}", DateTime.UtcNow.ToLongTimeString());
                        return new EmptyResult();
                    }
                }
                _logger.LogInformation("Data successfully received at {DT}", DateTime.UtcNow.ToLongTimeString());
                return dataItem;
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                _logger.LogError(ex.Message + " at {DT}", DateTime.UtcNow.ToLongTimeString());
                return new EmptyResult();
            }
        }
    }
}
