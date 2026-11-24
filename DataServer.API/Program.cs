using System.Text;
using DataServer.API.Data;
using DataServer.API.Entrypoints;
using DataServer.API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);
var connString = "Data Source=DataServer.db";
builder.Services.AddSqlite<DataServerContext>(
    connString,
    optionsAction: options => options.UseSeeding((context, _) =>
    {
        if (!context.Set<Server>().Any())
        {
            context.Set<Server>().AddRange(
                new Server {Id=1,Name="oscip05" },
                new Server {Id=2,Name="oslab15" }
            );
            context.SaveChanges();
        }
    })
);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidateAudience = true,
            ValidAudience = jwtSettings["Audience"],
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    }
);

builder.Services.AddAuthorization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngular", policy =>
        policy.WithOrigins("http://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.AddDataEntryPoints(); 
app.UseCors("AllowAngular");
app.Run();
