using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreApplication.Models;

namespace StoreApplication.ViewModels
{
    public class CategoryViewModel
    {
        DataBaseContext context;
        public CategoryViewModel(DataBaseContext db)
        {
            context = db;
        }

        public Object GetCategories(int CategoryId)
        {
            if (CategoryId != 0)
                return new { Category = context.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault() };
            else
                return new { Categories = context.Categories };
        }

        public bool CreateCategory(Object dataItem)
        {
            try
            {
                Categories categoryJson = JsonConvert.DeserializeObject<Categories>(dataItem.ToString());
                Categories categoryToAdd = new Categories()
                {
                    CategoryName = categoryJson.CategoryName
                };
                context.Categories.Add(categoryToAdd);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateCategory(Object dataItem, int CategoryId)
        {
            try
            {
                Categories categoryJson = JsonConvert.DeserializeObject<Categories>(dataItem.ToString());
                if (categoryJson?.CategoryId != 0 && CategoryId != 0)
                {
                    Categories categoryToAdd = new Categories()
                    {
                        CategoryId = categoryJson.CategoryId,
                        CategoryName = categoryJson.CategoryName
                    };
                    context.Entry(categoryToAdd).State = EntityState.Modified;
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

        public bool DeleteCategory(int CategoryId)
        {
            try
            {
                foreach (var product in context.Products.Where(p => p.FK_Category == CategoryId).ToList())
                {
                    context.Entry(context.Products.Where(p => p.ProductId == product.ProductId).FirstOrDefault()).State = EntityState.Deleted;
                    context.SaveChanges();
                }
                context.Entry(context.Categories.Where(c => c.CategoryId == CategoryId).FirstOrDefault()).State = EntityState.Deleted;
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
