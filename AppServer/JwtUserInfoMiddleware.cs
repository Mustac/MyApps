using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class JwtUserInfoMiddleware
{
    private readonly RequestDelegate _next;

    public JwtUserInfoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (token != null)
        {
            var handler = new JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

            var email = jsonToken.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;

            // You can extract other information from the token as needed.

            // Set the extracted information in the HttpContext for access in the controller.
            context.Items["UserEmail"] = email;
            // You can set other information in the HttpContext as needed.
        }

        await _next(context);
    }
}
