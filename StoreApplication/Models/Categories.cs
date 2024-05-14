using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Categories
    {
        [Key]
        public int CategoryId { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
