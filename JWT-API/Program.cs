using JWT_API.Data;
using JWT_API.Model;
using JWT_API.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
string cstring = builder.Configuration.GetSection("ConnectionStrings:DefualConnection").Value;
builder.Services.AddDbContext<JWT_APIContext>(options=>options.UseSqlServer(cstring));
builder.Services.AddScoped<ICustomerService, CustomerService>();
var _jwtsettings = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSettings>(_jwtsettings);
string authkey = builder.Configuration.GetSection("JWTSettings:securitykey").Value;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = true;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
       // ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        //ValidIssuer = "your-issuer",       // Change this to your own issuer
        //ValidAudience = "your-audience",   // Change this to your own audience
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authkey)) // Change this to your own secret key
    };
});







var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
