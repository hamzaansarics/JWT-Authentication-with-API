using JWT_API.Data;
using JWT_API.Model;
using JWT_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWT_API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly JWT_APIContext _context;
        private readonly JWTSettings settings;
        public UserController(JWT_APIContext context,IOptions<JWTSettings> options)
        {
            _context = context;
            settings = options.Value;
        }
        [HttpPost("Authanticate")]
        public IActionResult Authanticate(UserModel userModel)
        {
            var result = _context.users.FirstOrDefault(x => x.UserName == userModel.UserName && x.Password == userModel.Password);
            if(result!=null)
            {
                var tokenhandler = new JwtSecurityTokenHandler();
                var tokenkey = Encoding.UTF8.GetBytes(settings.securitykey);
                var tokendescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(
                        new Claim[]
                        {
                            new Claim(ClaimTypes.Name, userModel.UserName),
                        }),
                    Expires = DateTime.Now.AddMinutes(2),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenkey), SecurityAlgorithms.HmacSha256)
                };
                var token = tokenhandler.CreateToken(tokendescriptor);
                string finaltoken = tokenhandler.WriteToken(token);
                return Ok(finaltoken);
            }
            return Unauthorized();
           
        }
    }
}
