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
  public class ClientController : ControllerBase
  {

  static string GenerateCode(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        return new string(Enumerable.Repeat(chars, length)
          .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    static void SendVerificationEmail(string recipientEmail, string verificationCode)
    {
      string senderEmail = "apiservice32423@outlook.com";
      string password = "bB!1234567890!";

      SmtpClient smtpClient = new SmtpClient {
        Host = "smtp.office365.com",
        Port = 587,
        Credentials = new NetworkCredential(senderEmail, password),
        EnableSsl = true
      };

      MailMessage mail = new MailMessage(senderEmail, recipientEmail)
      {
        Subject = "Kod potwierdzający rejestrację",
        Body = $"Link do potwierdzenia: http://localhost:5500/code.html. Twój kod potwierdzający rejestrację to: {verificationCode}"
      };

      smtpClient.Send(mail);
    }

    private readonly BankDBContext _context;
    public ClientController(BankDBContext context)
    {
      _context = context;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetClients()
    {
      var claims = User.Claims;
      var clientIdClaim = claims.FirstOrDefault(c => c.Type == "client_id");
      if (clientIdClaim == null)
      {
        return StatusCode(403);
      }

      var clientIdValue = clientIdClaim.Value;

      var client = _context.Client.Where(c => c.ClientId == Int32.Parse(clientIdValue)).Select(c => c).ToList();
      if(client.Count != 0)
        return Ok(client);
      else
        return StatusCode(500);
    }


    [HttpPost]
    public IActionResult NewClient([FromBody] Client client)
    {

        if (ModelState.IsValid)
        {
            var existingClient = _context.Client.FirstOrDefault(u => u.CLogin == client.CLogin);

            if (existingClient == null)
            {
              string generatedCode = GenerateCode(8);
              client.verifyCod = generatedCode;
                _context.Client.Add(client);
                _context.SaveChanges();

                try
                {
                  SendVerificationEmail(client.CLogin, generatedCode);
                }
                catch
                {
                  BadRequest("Error with send email verification");
                }

                return Ok("Client added successfully.");
            }

            else
            {
                return BadRequest("Client with this login already exists.");
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }


  }
}


