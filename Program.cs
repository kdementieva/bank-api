using BankAPI;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);
var jwtIssuer = builder.Configuration.GetSection("Jwt:Issuer").Get<string>();
var jwtKey = builder.Configuration.GetSection("Jwt:Key").Get<string>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
 .AddJwtBearer(options =>
 {
  options.TokenValidationParameters = new TokenValidationParameters
  {
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = jwtIssuer,
    ValidAudience = jwtIssuer,
    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
  };
});

builder.Services.AddDbContext<BankDBContext>(options =>
{
  options.UseSqlServer(builder.Configuration.GetConnectionString("BankDB"));
});

builder.Services.AddCors(options => options.AddDefaultPolicy(builder =>
  builder.AllowAnyOrigin()
  .AllowAnyMethod()
  .AllowAnyHeader()
));


builder.Services.AddControllers();
builder.Services.AddAuthorization();
var app = builder.Build();


app.MapControllers();
app.UseAuthentication();
app.UseAuthorization();
app.UseCors();

app.Run();


