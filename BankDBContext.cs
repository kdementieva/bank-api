using BankAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BankAPI
{
  public class BankDBContext : DbContext
  {
    public BankDBContext(DbContextOptions<BankDBContext> options) : base(options) {}
    public DbSet<Client> Client { get; set; }
  }
}