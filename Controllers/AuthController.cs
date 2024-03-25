using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;

namespace BankAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly BankDBContext _context;
    private readonly IConfiguration _config;
    public AuthController(BankDBContext context, IConfiguration config)
    {
      _context = context;
      _config = config;
    }

    [HttpPost]
    public IActionResult AuthUser([FromBody] Client client)
    {
      var User = _context.Client.Where(u => u.CLogin == client.CLogin && u.CPassword == client.CPassword && u.IsActive).Select(u => u.ClientId).ToList();

      if (User.Count != 0)
      {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
          new Claim("client_id", User[0].ToString())
        };

        var Sectoken = new JwtSecurityToken(_config["Jwt:Issuer"],
          _config["Jwt:Issuer"],
          claims,
          expires: DateTime.Now.AddMinutes(120),
          signingCredentials: credentials
        );

        var token =  new JwtSecurityTokenHandler().WriteToken(Sectoken);

        return Ok(token);
      }
      else
        return Unauthorized();
    }
  }
}