using Microsoft.AspNetCore.Authorization;

namespace AulersApi.AulersSecurity
{
    public class IsSuperUserAuthorizationHandler: AuthorizationHandler<AulersAuthorizationRequirement>
    {
        private readonly ILogger<IsSuperUserAuthorizationHandler> _logger;

        public IsSuperUserAuthorizationHandler(ILogger<IsSuperUserAuthorizationHandler> logger)
        {
            _logger = logger;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AulersAuthorizationRequirement requirement)
        {
            if (requirement.AllowSuperUser && IsSuperUser(context))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }

        private bool IsSuperUser(AuthorizationHandlerContext context)
        {
            if (!context.User.HasClaim(c => c.Type.Equals(AulersSecurityDefaults.SUPERUSER_JWT_KEY)))
            {
                _logger.LogDebug("The token hasn't super user permissions.");
                return false;
            }

            var isSuperUser = bool.Parse(context.User.FindFirst(c => c.Type.Equals(AulersSecurityDefaults.SUPERUSER_JWT_KEY)).Value);
            if (isSuperUser)
            {
                return true;
            }

            _logger.LogDebug("The token super user permissions is false.");
            return false;
        }
    }
}
