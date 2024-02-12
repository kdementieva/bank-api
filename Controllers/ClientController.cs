using BankAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BankAPI.Controllers
{
  [ApiController]
  [Route("[controller]")]
  [Authorize (Roles= "client")]
  public class ClientController : ControllerBase
  {
    private readonly BankDBContext _context;
    public ClientController(BankDBContext context)
    {
      _context = context;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Client>> GetClients([FromQuery] int id)
    {
      var userId = HttpContext.User.FindFirstValue("client_id");
      if(Int32.Parse(userId) != id)
        return StatusCode(403);

      var client = _context.Client.Where(c => c.ClientId == id).Select(c => c).ToList();
      if(client.Count != 0)
        return Ok(client);
      else
        return StatusCode(500);
    }
  }
}