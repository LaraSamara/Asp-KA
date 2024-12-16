using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Workshop.API.Helper
{
    public class ExtractClaim
    {
        public static int? ExtractUserId(string Token)
        {
            try
            {
                var TokenHandler = new JwtSecurityTokenHandler();
                var JwtToken = TokenHandler.ReadJwtToken(Token);
                var UserIdClaim = JwtToken.Claims.FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier);
                if(UserIdClaim != null && int.TryParse(UserIdClaim.Value, out int UserId ))
                {
                    return UserId;
                }
                return null;
            }catch(Exception ex)
            {
                return null;
            }
        }
    }
}
