using BasicAuthExample.WrapperModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

public class BasicAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public BasicAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock) 
        : base(options, logger, encoder, clock)
    {
    }


    protected override async Task HandleChallengeAsync(AuthenticationProperties properties)
    {
      await  WriteUnauthorizedResponse(Response);

    }
    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
            var credentials = Encoding.UTF8.GetString(Convert.FromBase64String(authHeader.Parameter)).Split(':');
           var username = credentials.FirstOrDefault();
            var password = credentials.LastOrDefault();

            if (!username.Equals("admin") && !password.Equals("admin123"))
            {
                return AuthenticateResult.Fail("Unauthorized");


            }
            else
            {
                var claims = new[] {
                new Claim(ClaimTypes.Name, username)
            };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return AuthenticateResult.Success(ticket);
            }


        }
        catch (Exception e)
        {

            return AuthenticateResult.Fail("Unauthorized");

        }
    }
    private static async Task WriteUnauthorizedResponse(HttpResponse response)
    {
        Console.WriteLine(response.Body);
        response.StatusCode = StatusCodes.Status401Unauthorized;
        response.ContentType = "application/json";
        using var writer = new Utf8JsonWriter(response.BodyWriter);
        writer.WriteStartObject();
        writer.WriteString("error", "Unauthorized");
        writer.WriteEndObject();
        await writer.FlushAsync();
    }
}