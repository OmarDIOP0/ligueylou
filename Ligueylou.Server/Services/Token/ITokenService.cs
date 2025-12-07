using System.Security.Claims;

namespace Ligueylou.Server.Services.Token
{
    public interface ITokenService
    {
        Tuple<string, DateTime> GenerateAccessToken(IEnumerable<Claim> claims);
        string GenerateRefreshToken();
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}
