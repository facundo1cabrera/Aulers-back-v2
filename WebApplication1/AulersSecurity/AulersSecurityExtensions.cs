using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AulersApi.AulersSecurity
{
    public static class AulersSecurityExtensions
    {
        public static IServiceCollection AddAulersSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationHandler, IsSuperUserAuthorizationHandler>();
            services.AddSingleton<IAuthorizationHandler, IsOwnResourceAuthorizationHandler>();

            services.AddOptions<AuthorizationOptions>()
                .Configure(o =>
                {
                    var simpleAuthenticationPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser()
                        .Build();

                    var onlySuperUserPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .AddRequirements(new AulersAuthorizationRequirement()
                        {
                            AllowSuperUser = true
                        })
                        .RequireAuthenticatedUser()
                        .Build();

                    var ownResourceOrSuperUserPolicy = new AuthorizationPolicyBuilder()
                        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .AddRequirements(new AulersAuthorizationRequirement()
                        {
                            AllowSuperUser = true,
                            AllowOwnResource = true
                        })
                        .RequireAuthenticatedUser()
                        .Build();

                    o.DefaultPolicy = simpleAuthenticationPolicy;

                    o.AddPolicy(Policies.ONLY_SUPERUSER, onlySuperUserPolicy);
                    o.AddPolicy(Policies.OWN_RESOURCE_OR_SUPERUSER, ownResourceOrSuperUserPolicy);
                });

            services.AddOptions<JwtBearerOptions>(JwtBearerDefaults.AuthenticationScheme)
                .Configure<IOptions<AulersSecurityOptions>>((o, securityOptions) =>
                {
                    o.SaveToken = true;
                    o.TokenValidationParameters = new TokenValidationParameters()
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                    };
                });

            services.AddAuthentication()
                .AddJwtBearer();

            services.AddAuthorization();

            return services;
        }
    }
}
