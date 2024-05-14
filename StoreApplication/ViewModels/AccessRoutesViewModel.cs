using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreApplication.Models;


namespace StoreApplication.ViewModels
{
    public class AccessRoutesViewModel
    {
        DataBaseContext context;
        public AccessRoutesViewModel(DataBaseContext db)
        {
            context = db;
        }

        public Object GetAccessRoutes(int RouteId)
        {
            if (RouteId != 0)
                return new { AccessRoute = context.AccessRoutes.Where(ac => ac.RouteId == RouteId).FirstOrDefault() };
            else
                return new { AccessRoutes = context.AccessRoutes };
        }

        //public bool CreateAccessRoute(Object dataItem)
        //{
        //    try
        //    {
        //        AccessRoutes accessRouteJson = JsonConvert.DeserializeObject<AccessRoutes>(dataItem.ToString());
        //        AccessRoutes accessRouteToAdd = new AccessRoutes()
        //        {
        //            RouteName = accessRouteJson.RouteName,
        //            RouteRoleAccess = accessRouteJson.RouteRoleAccess,
        //        };
        //        context.AccessRoutes.Add(accessRouteToAdd);
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool UpdateAccessRoutes(Object dataItem, int accessRouteId)
        //{
        //    try
        //    {
        //        AccessRoutes accessRouteUpdate = new AccessRoutes();
        //        AccessRoutes accessRouteJson = JsonConvert.DeserializeObject<AccessRoutes>(dataItem.ToString());
        //        if (accessRouteJson?.RouteId != 0 && accessRouteId != 0)
        //        {
        //            accessRouteUpdate = new AccessRoutes()
        //            {
        //                RouteId = accessRouteJson.RouteId,
        //                RouteName = accessRouteJson.RouteName,
        //                RouteRoleAccess = accessRouteJson.RouteRoleAccess,
        //            };
        //            context.Entry(accessRouteUpdate).State = EntityState.Modified;
        //            context.SaveChanges();
        //            return true;
        //        }
        //        return false;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public bool DeleteAccessRoute(int AccessRouteId)
        //{
        //    try
        //    {
        //        context.Entry(context.AccessRoutes.Where(acc => acc.RouteId == AccessRouteId).FirstOrDefault()).State = EntityState.Deleted;
        //        context.SaveChanges();
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}
