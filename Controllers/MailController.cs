using System.Security.Permissions;
using BankAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Mail;

namespace BankAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
      public class MailController : ControllerBase
    {
        private readonly BankDBContext _context;

        public MailController(BankDBContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult verifyCod([FromBody] Client client)
        {

            var User = _context.Client.Where(u => u.verifyCod == client.verifyCod).Select(u => u).ToList();
            
            if (User.Count == 0)
            {
                return BadRequest("Wprowadzony kod potwierdzający jest nieprawidłowy.");
            }

           
            User[0].IsActive = true;

            _context.SaveChanges();

            return Ok("Konto zostało pomyślnie potwierdzone.");
        }
    }
}