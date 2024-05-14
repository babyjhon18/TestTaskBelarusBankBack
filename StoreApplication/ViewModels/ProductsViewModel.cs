using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreApplication.Models;
using StoreApplication.Utils;

namespace StoreApplication.ViewModels
{
    public class ProductsViewModel
    {
        DataBaseContext context;
        public ProductsViewModel(DataBaseContext db)
        {
            context = db;
        }

        public Object GetProducts(int ProductId)
        {
            if (ProductId != 0)
                return new { Products = context.Products.Where(p => p.ProductId == ProductId).FirstOrDefault() };
            else
                return new { 
                    Products = context.Products,
                    Categories = context.Categories
                };
        }

        public bool CreateProduct(Object dataItem)
        {
            try
            {
                Products productJson = JsonConvert.DeserializeObject<Products>(dataItem.ToString());
                Products productToAdd = new Products()
                {
                    ProductName = productJson.ProductName,
                    ProductDescription = productJson.ProductDescription,
                    FK_Category = productJson.FK_Category,
                    ProductPrice = productJson.ProductPrice,
                    GeneralNote = productJson.GeneralNote,
                    SpecialNote = productJson.SpecialNote   
                };
                context.Products.Add(productToAdd);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateProducts(Object dataItem, int ProductId)
        {
            try
            {
                Products productJson = JsonConvert.DeserializeObject<Products>(dataItem.ToString());
                if (productJson?.ProductId != 0 && ProductId != 0)
                {
                    Products productToAdd = new Products()
                    {
                        ProductId = productJson.ProductId,
                        ProductName = productJson.ProductName,
                        ProductDescription = productJson.ProductDescription,
                        FK_Category = productJson.FK_Category,
                        ProductPrice = productJson.ProductPrice,
                        GeneralNote = productJson.GeneralNote,
                        SpecialNote = productJson.SpecialNote
                    };
                    context.Entry(productToAdd).State = EntityState.Modified;
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteProduct(int ProductId)
        {
            try
            {
                context.Entry(context.Products.Where(p => p.ProductId == ProductId).FirstOrDefault()).State = EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
