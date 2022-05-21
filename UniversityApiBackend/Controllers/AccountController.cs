using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Helpers;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JwtSettings _jwtSettings;

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;

        }

        //
        //TODO  Change by real user DB

        private readonly UniversityDBContext _context;
         private IEnumerable<User> Logins = new List<User>()
        {
        
         new User()
        {
            Id = 1,
            Email= "martin@imaginagroup",
            Name= "Admin",
            Password= "Admin"
        },
        new User()
         {
            Id = 2,
            Email= "pepe@imaginagroup",
            Name= "User 1",
            Password= "pepe"
        }
        };
        [HttpPost]
        public IActionResult GetToken (UserLogins userlogins)
        {
            try
            {
                var Token = new UserTokens();


                //Search a user in context with LinQ

                var searchUser = (from user in _context.Users
                                  where user.Name == userlogins.UserName && user.Password == userlogins.Password
                                  select user).FirstOrDefault();

                Console.WriteLine("User found", searchUser);


                //var Valid = Logins.Any(user => user.Name.Equals(userlogins.UserName, StringComparison.OrdinalIgnoreCase));
            
                if (searchUser != null)
                {
                   // var user= Logins.FirstOrDefault(user =>user.Name.Equals(userlogins.UserName, StringComparison.OrdinalIgnoreCase));

                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        UserName = searchUser.Name,
                        EmailId = searchUser.Email,
                        Id = searchUser.Id,
                        GuidId = Guid.NewGuid(),
                    }, _jwtSettings) ;
                }
                else
                {
                    return BadRequest("Wrong Password");
                }

                return Ok(Token);
            
            }catch (Exception ex)
            {
                throw new Exception("Get Token Error", ex);
            }
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes =JwtBearerDefaults.AuthenticationScheme, Roles ="Administrator")]
        public IActionResult GetUserList()
        {
            return Ok(Logins);
        }
    }

}
