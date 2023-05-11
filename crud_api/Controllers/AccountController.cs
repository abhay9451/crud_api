using crud_api.Models;
using crud_api.Models.DbAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace crud_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class AccountController : ControllerBase
    {
        ApiResponse response = new ApiResponse();
        AdminDbAccess adminDb = new AdminDbAccess();

        public IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        [Route ("Login")]
        [HttpPost]
        public IActionResult LoginUser(AdminLogin admin)
        {
            try
            {
                var res = adminDb.LoginAdmin(admin);
                if(res == "ok")
                {
                    response.Ok = true;
                    response.Message = $"Login successfully";
                    response.Token = GenerateToken(admin);
                }
                else if (res == "fail")
                {
                    response.Ok = false;
                    response.Message = $"Invalid email or Password";
                }
                else
                {
                    response.Ok = false;
                    response.Message = res;
                }
            }
            catch(Exception ex)
            {
                response.Ok = false;
                response.Message = ex.Message;
            }
            return Ok(response);
        }
        public string GenerateToken(AdminLogin admin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
              {
             new Claim(ClaimTypes.Name, admin.Email)
              }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);

        }
    }
}
