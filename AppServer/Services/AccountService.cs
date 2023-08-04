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
using MyAppServer;

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
                user = new User { Email = email, RoleId = 2, FirstLogin = true };
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

        public async Task<ServerResponse<UserSigninResponse>> SignInUserAsync(UserEmailCode userEmailCode)
        {
            var user = await GetUserWithRolesAsync(userEmailCode.Email);

            if (user is null)
                return new ServerResponse<UserSigninResponse>(HttpStatusCode.BadRequest, "Could not find the user with that email");

            var codes = user.ConfirmationCode.Split(":");

            DateTime codeCreated = new DateTime(int.Parse(codes[1]), int.Parse(codes[2]), int.Parse(codes[3]), int.Parse(codes[4]), int.Parse(codes[5]), 0);

            if (DateTime.Now < codeCreated)
                return new ServerResponse<UserSigninResponse>(HttpStatusCode.Unauthorized, "Verification token has expired");

            if (codes.First() != userEmailCode.Code)
                return new ServerResponse<UserSigninResponse>(HttpStatusCode.Unauthorized, "Wrong code");

            var token = CreateJWT(user);

            ServerResponse<UserSigninResponse> response;

            if (user.FirstLogin)
            {
                response = new ServerResponse<UserSigninResponse>(HttpStatusCode.OK, "Sign in is a success", new UserSigninResponse { JwtToken = token, FirstSignin = true });
                user.FirstLogin = false;
                _db.Users.Update(user);
                await _db.SaveChangesAsync();
            }
            else
            {
                response = new ServerResponse<UserSigninResponse>(HttpStatusCode.OK, "Sign in is a success", new UserSigninResponse { JwtToken = token, FirstSignin = false });
            }

            return response;
        }

        public async Task<ServerResponse<object>> UpdateUserInfoAsync(UserUpdate userUpdate, string email)
        {
            var user = await GetUserWithRolesAsync(email);

            if (user is null)
                return new ServerResponse<object>(HttpStatusCode.BadRequest, "Could not find the user");

            user.Name = userUpdate.Name;
            user.LastName = userUpdate.LastName;
            user.CompanyName = userUpdate.Company;

            _db.Users.Update(user);

            return await _db.SaveChangesAsync() > 0 ?
                new ServerResponse<object>(HttpStatusCode.OK, "Update was a success") :
                new ServerResponse<object>(HttpStatusCode.InternalServerError, "Could not update the user");
        }


        /* in class use function */

        private async Task<User> GetUserWithRolesAsync(string email)
        {
            return await _db.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
        }

        private string CreateJWT(User user)
        {
            var secretkey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(JwtSecrets.Key)); // NOTE: SAME KEY AS USED IN Program.cs FILE
            var credentials = new SigningCredentials(secretkey, SecurityAlgorithms.HmacSha256);

            var claims = new[] // NOTE: could also use List<Claim> here
			{
				new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Email), // NOTE: this will be the "User.Identity.Name" value
			};

            var token = new JwtSecurityToken(issuer: JwtSecrets.Issuer, audience: JwtSecrets.Audience, claims: claims, expires: DateTime.Now.AddMonths(1), signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
