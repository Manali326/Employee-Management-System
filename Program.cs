using EmployeeManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

byte[] secretBytes = new byte[64];
using (var random = RandomNumberGenerator.Create())
{
    random.GetBytes(secretBytes);

}
string secretKey=Convert.ToBase64String(secretBytes);

// Add services to the container.
builder.Services.AddDbContext<EmployeeContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("DbCon")));
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer= true,
            ValidateAudience=false,
            ValidateLifetime=true,
            ValidateIssuerSigningKey = true,
            ValidIssuer="FreeTrained",
            IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))

        };
    });
builder.Services.AddAuthentication();
builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.Password.RequiredLength=6;
    options.Password.RequireNonAlphanumeric=false;
    options.Password.RequireDigit=false;
    options.Password.RequireUppercase=false;
    options.Password.RequireLowercase=false;

})
    .AddEntityFrameworkStores<EmployeeContext>()
    .AddDefaultTokenProviders();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseAuthorization();

app.MapControllers();

app.Run();
