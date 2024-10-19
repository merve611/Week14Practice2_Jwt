using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Week14Practice2_Jwt.Context;
using Week14Practice2_Jwt.Manager;
using Week14Practice2_Jwt.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuer = true,      //Issuer validasyonu yap
            ValidIssuer = builder.Configuration["Jwt:Issuer"],      //appsettingsdeki de�er
            ValidateAudience = true,    //Audience validasyonu yap
            ValidAudience = builder.Configuration["Jwt:Audience"],  //appsettingsdeki de�er
            ValidateLifetime = true,    //ge�erlilik zaman� validasyonu yap
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"]!))    //appsettingsdeki key �nlem null gelemez demek, ? null gelebilir demek




        };
    });

var cs = builder.Configuration.GetConnectionString("default");

builder.Services.AddDbContext<PatikaDbContext>(options =>
options.UseSqlServer(cs));

builder.Services.AddScoped<IUserService, UserManager>();
builder.Services.AddDataProtection();

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
