using Microsoft.AspNetCore.Authorization;

namespace AulersApi.AulersSecurity
{
    public class AulersAuthorizationRequirement : IAuthorizationRequirement
    {
        public bool AllowSuperUser { get; init; }
        public bool AllowOwnResource { get; init; }
    }
  
}
