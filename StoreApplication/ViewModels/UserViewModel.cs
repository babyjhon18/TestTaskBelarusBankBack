using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using StoreApplication.Controllers;
using StoreApplication.Models;
using StoreApplication.Utils;
using System.Data;

namespace StoreApplication.ViewModels
{
    public class UserViewModel
    {
        DataBaseContext context;
        public UserViewModel(DataBaseContext db)
        {
            context = db;
        }

        public Object GetUsers(int UserId)
        {
            if (UserId != 0)
                return new { Users = context.Users.Where(u => u.UserId == UserId).FirstOrDefault() };
            else
                return new { Users = context.Users };
        }

        public bool CreateUser(Object dataItem)
        {
            try
            {
                Users userJson = JsonConvert.DeserializeObject<Users>(dataItem.ToString());
                var userPassword = SysUtils.GetHash(userJson.UserName, userJson.UserPassword);
                Users userToAdd = new Users()
                {
                    UserName = userJson.UserName,
                    UserRole = userJson.UserRole,
                    IsAdmin = userJson.IsAdmin,
                    IsBlocked = userJson.IsBlocked,
                    UserPassword = userPassword
                };
                context.Users.Add(userToAdd);
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool UpdateUsers(Object dataItem, int UserId)
        {
            try
            {
                Users userToAdd = new Users();
                Users userJson = JsonConvert.DeserializeObject<Users>(dataItem.ToString());
                var userPassword = SysUtils.GetHash(userJson.UserName, userJson.UserPassword);
                if (userJson?.UserId != 0 && UserId != 0)
                {
                    userToAdd = new Users()
                    {
                        UserId = userJson.UserId,
                        UserName = userJson.UserName,
                        UserPassword = userPassword,
                        UserRole = userJson.UserRole,
                        IsAdmin = userJson.IsAdmin,
                        IsBlocked = userJson.IsBlocked,
                    };
                    context.Entry(userToAdd).State = EntityState.Modified;
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

        public bool DeleteUser(int UserId)
        {
            try
            {
                context.Entry(context.Users.Where(u => u.UserId == UserId).FirstOrDefault()).State = EntityState.Deleted;
                context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public UserAccess Validate(string username, string password)
        {
            Users user = context.Users.Where(u => u.UserName == username).FirstOrDefault();
            if (user != null)
            {
                var userPassword = SysUtils.GetHash(username, password);
                if (user.UserPassword == userPassword)
                {
                    UserAccess userAccess = new UserAccess();
                    userAccess.UserAccessRoutes = context.AccessRoutes.Where(ua => ua.RouteRoleAccess <= user.UserRole).ToList();
                    userAccess.User = user;
                    return userAccess;

                }
                else
                    return null;
            }
            else
                return null;
        }
    }
}
