using BankAPI;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

builder.Services.AddDbContext<BankDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB"));
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder => builder.WithOrigins("http://127.0.0.1:5500").AllowAnyMethod().AllowAnyHeader().AllowCredentials()));

builder.Services.AddAuthentication().AddCookie("cookie");
builder.Services.AddAuthorization();
var app = builder.Build();

app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.Run();


