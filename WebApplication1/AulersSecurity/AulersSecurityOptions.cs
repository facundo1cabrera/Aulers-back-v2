using Microsoft.IdentityModel.Tokens;

namespace AulersApi.AulersSecurity
{
    public class AulersSecurityOptions
    {
        public IEnumerable<SecurityKey> SigningKeys { get; set; } = new SecurityKey[0];
    }
}
