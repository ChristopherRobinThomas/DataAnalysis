using DataServer.API.Data;
using DataServer.API.dtos;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.EntityFrameworkCore;
using DataServer.API.Models;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;


namespace DataServer.API.EntryPoints;

public static class UserEntrypoints
{
    static string GenerateJwtToken(string name, IConfiguration config)
{
    var jwtSettings = config.GetSection("JwtSettings");
    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!));
    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

    var claims = new[]
    {
        new Claim(ClaimTypes.Name, name),
    };

    var token = new JwtSecurityToken(
        issuer: jwtSettings["Issuer"],
        audience: jwtSettings["Audience"],
        claims: claims,
        expires: DateTime.UtcNow.AddMinutes(double.Parse(jwtSettings["ExpirationMinutes"]!)),
        signingCredentials: creds
    );

    return new JwtSecurityTokenHandler().WriteToken(token);
}
    public static void AddUserEntryPoints(this WebApplication app, IConfiguration config)
    {
        app.MapPost("/login/", async (UserCredentials credentials, DataServerContext context, IPasswordHasher<User> hasher) =>
        {
            // Get user By name
            var users = await context.Users.Where(e => e.Name == credentials.Username).AsNoTracking().ToListAsync();
            if(users == null) return Results.NotFound();
            if(users.Count != 1) return Results.InternalServerError();
            var user = users[0];

            var result = hasher.VerifyHashedPassword(user, user.Password, credentials.Password);
            if(result == PasswordVerificationResult.Failed) return Results.Unauthorized();

            var token = GenerateJwtToken(credentials.Username, config);
            return Results.Ok(new {token});
        });
        app.MapPost("/register/", async (UserCredentials credentials, DataServerContext context, IPasswordHasher<User> hasher) =>
        {
            // TODO a lot of securtity stuff todo
            var user = new User { Name = credentials.Username };
            user.Password = hasher.HashPassword(user, credentials.Password);
            context.Add(user);
            await context.SaveChangesAsync();
            return Results.Ok();
        });

        
    }
}
