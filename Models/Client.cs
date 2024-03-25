using System.ComponentModel.DataAnnotations;

namespace BankAPI.Models
{
  public class Client
  {
    [Key]
    public int ClientId { get; set; } = 0;
    public string FirstName { get; set; } = "";
    public string SecondName { get; set; } = "";
    public string LastName { get; set; } = "";
    public DateOnly DateOfBirth { get; set; } = DateOnly.MinValue;
    public string CLogin { get; set; } = "";
    public string CPassword { get; set; } = "";
    public string verifyCod { get; set; } = "";
    public bool IsActive { get; set; } = false;
  }
}
