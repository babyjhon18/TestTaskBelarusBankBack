namespace StoreApplication.Models
{
    public class UserAccess
    {
        public Users User { get; set; }
        public List<AccessRoutes> UserAccessRoutes { get; set; }
    }
}
