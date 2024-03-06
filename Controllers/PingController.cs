using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BankAPI.Controllers
{
  [ApiController]
  [Route ("[controller]")]
  [Authorize (Roles= "client")]
  public class PingController : ControllerBase
  {
    [HttpGet]
    public IActionResult pingOk()
    {
      return Ok();
    }
  }
}