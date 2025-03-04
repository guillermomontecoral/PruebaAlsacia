using Application.Dtos;
using Application.UseCases.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SistemaGestionTareas.Controllers
{
    [Route("api/login")]
    [ApiController]
    [Produces("application/json")]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IValidateLogin _validateLogin;

        public LoginController(IConfiguration configuration, IValidateLogin validateLogin)
        {
            _configuration = configuration;
            _validateLogin = validateLogin;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> Login([FromBody] UserDto user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest("User cannot be null");
                }

                var id = await _validateLogin.Validate(user.Email, user.Password);
    
                var token = GenerateToken(id);
                return Ok(token);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        private string GenerateToken(int id)
        {
            try
            {
                var claims = new[]
                {
                     new Claim(JwtRegisteredClaimNames.Sub, Convert.ToString(id)),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),
                    signingCredentials: creds);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
                return tokenString;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
