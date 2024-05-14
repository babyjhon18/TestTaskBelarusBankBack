using Microsoft.AspNetCore.Authorization;
using StoreApplication.Models;

namespace StoreApplication.AutherizationExctencion
{
    public static class APIAuthorizationExtension
    {
        public static AuthorizationOptions AddICTAPIPolicies(this AuthorizationOptions options, DataBaseContext db)
        {
            var data = db.AccessRoutes.ToList();
            options.AddPolicy("UserIsAdmin", policy =>
            {
                policy.RequireClaim("IsAdmin", "True");
            });
            foreach (var controllerAction in data)
            {
                options.AddPolicy(controllerAction.RouteName.ToString(), policy =>
                {
                    policy.RequireClaim("Route", controllerAction.RouteName.ToString());
                });
            }
            return options;
        }
    }
}
