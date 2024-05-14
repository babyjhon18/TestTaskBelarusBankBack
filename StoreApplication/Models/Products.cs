using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class Products
    {
        [Key]
        public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int FK_Category { get; set; }
        public string ProductDescription { get; set; } = string.Empty;
        public double ProductPrice { get; set; }
        public string GeneralNote { get; set; } = string.Empty;
        public string SpecialNote { get; set; } = string.Empty;
    }
}
