using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreApplication.AutherizationExctencion;
using StoreApplication.Models;
using StoreApplication.ViewModels;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoreApplication.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : BaseController
    {
        private const string SECRET_KEY = "this is my custom Secret key for authentication";
        public static readonly SymmetricSecurityKey SINGIN_KEY = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(LoginController.SECRET_KEY));

        public LoginController(ILogger<LoginController> logger, DataBaseContext db) :
            base(logger, db)
        {
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Login([FromBody] UserCreditnals creditnals)
        {
            UserAccess person = new UserViewModel(db).Validate(creditnals.UserName, creditnals.Password) as UserAccess;
            if (person == null)
            {
                return Forbid();
            }
            if (person.User.IsBlocked)
            {
                return BadRequest();
            }
            var identity = GetIdentity(person);
            var now = DateTime.UtcNow;
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
            audience: AuthOptions.AUDIENCE,
            notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            var _claimsKeyValue = new List<KeyValuePair<string, string>>();
            foreach (var _claim in identity.Claims)
            {
                _claimsKeyValue.Add(new KeyValuePair<string, string>(_claim.Type.ToString(), _claim.Value.ToString()));
            }
            var response = new
            {
                userId = person.User.UserId,
                access_token = encodedJwt,
                username = creditnals.UserName,
                claims = _claimsKeyValue,
                userRole = person.User.UserRole,
                isAdmin = person.User.IsAdmin,
            };
            Response.Cookies.Append("Authorization", encodedJwt);
            return new JsonResult(response);
        }

        private ClaimsIdentity GetIdentity(UserAccess person)
        {
            if (person != null)
            {
                if (person.User.IsAdmin == true)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, "Admin"),
                    };
                    claims.Add(new Claim("UserId", person.User.UserId.ToString()));
                    claims.Add(new Claim("IsAdmin", person.User.IsAdmin.ToString()));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    this.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                    return claimsIdentity;
                }
                else
                {
                    var claims = new List<Claim>();
                    foreach (var claim in person.UserAccessRoutes)
                    { 
                        claims.Add(new Claim("Route", claim.RouteName));
                    }
                    claims.Add(new Claim("Username", person.User.UserName));
                    claims.Add(new Claim("UserId", person.User.UserId.ToString()));
                    claims.Add(new Claim("IsAdmin", person.User.IsAdmin.ToString()));
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    this.HttpContext.User = new ClaimsPrincipal(new ClaimsIdentity(claims));
                    return claimsIdentity;
                }
            }
            return null;
        }
    }
}