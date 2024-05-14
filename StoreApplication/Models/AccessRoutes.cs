using System.ComponentModel.DataAnnotations;

namespace StoreApplication.Models
{
    public class AccessRoutes
    {
        [Key]
        public int RouteId { get; set; }
        public string RouteName { get; set; } = string.Empty;
        public int RouteRoleAccess { get; set; }
    }
}
