using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using bookStore.Data;
using Microsoft.EntityFrameworkCore;

namespace bookStore2.Middlewares
{
    public class IPWhitelistMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<IPWhitelistMiddleware> _logger;
        private readonly IServiceProvider _serviceProvider;

        public IPWhitelistMiddleware(RequestDelegate next, ILogger<IPWhitelistMiddleware> logger, IServiceProvider serviceProvider)
        {
            _next = next;
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress?.ToString();

            using (var scope = _serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDBContext>();

                // Check if the IP address is in the whitelist
                var isWhitelisted = await dbContext.WhitelistedIPs
                    .AnyAsync(ip => ip.IPAddress == ipAddress);

                if (!isWhitelisted)
                {
                    _logger.LogWarning($"Unauthorized access attempt from IP: {ipAddress}");
                    context.Response.StatusCode = StatusCodes.Status403Forbidden;
                    await context.Response.WriteAsync("Forbidden: Unauthorized.");
                    return;
                }
            }

            // Proceed to the next middleware if authorized
            await _next(context);
        }
    }
}