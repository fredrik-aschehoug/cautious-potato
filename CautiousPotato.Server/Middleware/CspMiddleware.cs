
using CautiousPotato.Server.Constants;
using System.Security.Cryptography;
using System.Text;

namespace CautiousPotato.Server.Middleware;

public class CspMiddleware(IWebHostEnvironment env) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var nonce = GenerateNounce();
        var policy = GetPolicy(nonce);

        context.Response.Headers.Append("Content-Security-Policy", policy);
        context.Items.TryAdd(CspConstants.NonceKey, nonce);

        await next(context);
    }

    private string GetPolicy(string nonce)
    {
        var policyBuilder = new StringBuilder();

        policyBuilder
            .Append($"script-src 'self' 'wasm-unsafe-eval' 'nonce-{nonce}';")
            .Append(" ")
            .Append($"style-src 'self' 'unsafe-inline';") // required by fluent
            .Append(" ")
            .Append("base-uri 'self';")
            .Append(" ")
            .Append("default-src 'self';")
            .Append(" ")
            .Append("frame-ancestors 'none';");

        if (env.IsDevelopment())
        {
            policyBuilder
                .Append(" ")
                .Append("connect-src 'self' ws: wss:;");
        }

        return policyBuilder.ToString();
    }

    private static string GenerateNounce()
    {
        using var rng = RandomNumberGenerator.Create();
        var nonceBytes = new byte[32];
        rng.GetBytes(nonceBytes);
        return Convert.ToBase64String(nonceBytes);
    }
}
