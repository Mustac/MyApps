using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PosAppServer.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http.HttpResults;
using Shared;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using MyAppServer.Services;
using System.Security.Cryptography;

namespace PosAppServer.Services
{
    public class AccountService
    {
        private readonly AppDbContext _db;
        private readonly MailJetService _mailJetService;

        public AccountService(AppDbContext db, MailJetService mailJetService)
        {
            _db = db;
            _mailJetService = mailJetService;
        }

        public async Task<ServerResponse<object>> VerifyEmailAsync(string email)
        {
            User user = await GetUserWithRolesAsync(email);

            if (user is null)
            {
                user = new User { Email = email, RoleId = 1, FirstLogin = true };
                await _db.Users.AddAsync(user);
                await _db.SaveChangesAsync();
                user = await GetUserWithRolesAsync(email);

                if (user is null)
                    return new ServerResponse<object>(HttpStatusCode.InternalServerError, "Something went wrong when creating new account");
            }

            Random random = new Random();
            string result = "";

            for (int i = 0; i < 6; i++)
            {
                int randomNumber = random.Next(0, 10); // Generate random number between 0 and 9
                result += randomNumber.ToString();
            }

            user.ConfirmationCode = $"{result}:{DateTime.Now.Year}:{DateTime.Now.Month}:{DateTime.Now.Day}:{DateTime.Now.Hour}:{DateTime.Now.Minute}";

            if (!await _mailJetService.SendAsync(user.Email, "Login code", $"<h1>{result}</h1>"))
                return new ServerResponse<object>(HttpStatusCode.InternalServerError, "Email could not be sent");

            _db.Users.Update(user);

            if (await _db.SaveChangesAsync() == 0)
                return new ServerResponse<object>(HttpStatusCode.InternalServerError, "Could not update the user");

            return new ServerResponse<object>(HttpStatusCode.OK, "Success");
        }

        public async Task<ServerResponse<string>> SignInUserAsync(UserEmailCode userEmailCode)
        {
            var user = await GetUserWithRolesAsync(userEmailCode.Email);

            if (user is null)
                return new ServerResponse<string>(HttpStatusCode.BadRequest, "Could not find the user with that email");

            var codes = user.ConfirmationCode.Split(":");

            DateTime codeCreated = new DateTime(int.Parse(codes[1]), int.Parse(codes[2]), int.Parse(codes[3]), int.Parse(codes[4]), int.Parse(codes[5]), 0);

            if (DateTime.Now < codeCreated)
                return new ServerResponse<string>(HttpStatusCode.Unauthorized, "Verification token has expired");

             if(codes.First() != userEmailCode.Code)
                return new ServerResponse<string>(HttpStatusCode.Unauthorized, "Wrong code");

            var token = CreateJWT(user);

            return new ServerResponse<string>(HttpStatusCode.OK, "Sign in is a success", token);
        }



        private async Task<User> GetUserWithRolesAsync(string email)
        {
            return await _db.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        private string CreateJWT(User user)
        {
            var secretkey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWTKey")));
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, user.Email), 
                new Claim(ClaimTypes.Role, user.Role.Name)
            };

            var token = new JwtSecurityToken(claims: claims, expires: DateTime.Now.AddYears(100), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
