using CautiousPotato.Server.Constants;

namespace CautiousPotato.Server.Extensions;

public static class HttpContextExtensions
{
    public static string? GetNonce(this HttpContext context)
    {
        if (context.Items[CspConstants.NonceKey] is not string nonce)
        {
            return null;
        }

        return nonce;
    }
}
