using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Part2_SimpleRestApi.Helpers;
using Part2_SimpleRestApi.Models;
using Part2_SimpleRestApi.Services;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Text.Unicode;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add services to the container.
builder.Services.AddControllers();

// Register the HTTP client
builder.Services.AddHttpClient<IFakeStoreService, FakeStoreService>(client =>
{
    client.BaseAddress = new Uri("https://fakestoreapi.com/docs/");
});

// Register the service
builder.Services.AddScoped<IFakeStoreService, FakeStoreService>();

var jwtOptions = builder.Configuration.GetSection("JwtSettings").Get<JWT>();
builder.Services.AddSingleton(jwtOptions);


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ValidateIssuer = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidateAudience = true,
            ValidAudience = jwtOptions.Audience,
            ValidateLifetime = true
        };
    });


// Register JwtHelper
builder.Services.AddSingleton<JwtHelper>();

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
