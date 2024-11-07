using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using bookStore2.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

namespace bookStore.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IUserRepository _userRepo;
        public BasicAuthenticationHandler(
            IOptionsMonitor<AuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder,
        IUserRepository userRepo,
        ISystemClock clock) 
        : base(options, logger, encoder, clock
        ){
            _userRepo = userRepo;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        string authorizationHeader = Request.Headers["Authorization"];
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        if (!authorizationHeader.StartsWith("basic ", StringComparison.OrdinalIgnoreCase))
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        var token = authorizationHeader.Substring(6);
        var credentialAsString = Encoding.UTF8.GetString(Convert.FromBase64String(token));
        
        var credentials = credentialAsString.Split(":");
        if (credentials?.Length != 2)
        {
            return AuthenticateResult.Fail("Unauthorized");
        }
        
        var username = credentials[0];
        var password = credentials[1];
        
        // if (username != "ransboak@gmail.com" && password != "Admin12345#")
        // {
        //     return AuthenticateResult.Fail("Authentication failed");
        // }
        var user = await _userRepo.ValidateUser(username, password);

        if (user == null)
            {
                // Check if user is not found or invalid credentials
                return AuthenticateResult.Fail("Unauthorized");
            }
        
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, username)
        };
        var identity = new ClaimsIdentity(claims, "Basic");
        var claimsPrincipal = new ClaimsPrincipal(identity);
        return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }
    }
}