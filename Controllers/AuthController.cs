using BankAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  public class AuthController : ControllerBase
  {
    private readonly BankDBContext _context;
    public AuthController(BankDBContext context)
    {
      _context = context;
    }

    [HttpPost]
    public IActionResult AuthUser([FromBody] Client client)
    {
      var User = _context.Client.Where(u => u.CLogin == client.CLogin && u.CPassword == client.CPassword).Select(u => u.ClientId).ToList();

      if (User.Count != 0)
      {
        return SignIn(new ClaimsPrincipal(new ClaimsIdentity(
          [
            new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            new Claim("my_role_claim_extravaganza", "client"),
            new Claim("client_id", User[0].ToString())
          ],
          "cookie",
          nameType:null,
          roleType: "my_role_claim_extravaganza"
        )),
        authenticationScheme: "cookie"
        );
      }
      else
        return Unauthorized();
    }
  }
}