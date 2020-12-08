using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Auth_Service_Without_DB.Models;
using Auth_Service_Without_DB.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Auth_Service_Without_DB.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(AuthController));
        //comment added to test
        //sample
        private IConfiguration _config;
        private IAuthRepo _repo;
        public AuthController(IConfiguration configuration, IAuthRepo repo)
        {
            _repo = repo;
            _config = configuration;
        }


        [HttpPost("login")]
        public IActionResult Login(User user)
        {
            _log4net.Info("Login Initiated for user " + user.Username);
            var result = _repo.Login(user);
            if (result == null)
            {
                _log4net.Info("User does not exist");
                return NotFound();
            }
            else
            {
                var token = GenerateJSONWebToken(user);
                _log4net.Info("Successfully logged In and token returned for user " + user.Username);
                return Ok(new { token = token,user=result });
            }
        }
        private string GenerateJSONWebToken(User userInfo)
        {
            _log4net.Info("Token Generation Started");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
           
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                null,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
                );
            
            return new JwtSecurityTokenHandler().WriteToken(token).ToString();
        }

    }
}