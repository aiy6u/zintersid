using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using zintersid.Data.Entities;
using zintersid.Services.UserPermissions;
using zintersid.Common.Constants;

namespace zintersid.Services.Identity
{
    public class AppSignInManager : SignInManager<AppUser>
    {
        private readonly IUserPermissionAppService _userPermissionAppService;

        public AppSignInManager(
            UserManager<AppUser> userManager,
            IHttpContextAccessor contextAccessor,
            IUserClaimsPrincipalFactory<AppUser> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor,
            ILogger<SignInManager<AppUser>> logger,
            IAuthenticationSchemeProvider schemes,
            IUserConfirmation<AppUser> confirmation,
            IUserPermissionAppService userPermissionAppService)
            : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
        {
            _userPermissionAppService = userPermissionAppService;
        }

        public override async Task<ClaimsPrincipal> CreateUserPrincipalAsync(AppUser user)
        {
            var principal = await base.CreateUserPrincipalAsync(user);

            if (principal.Identity is not ClaimsIdentity identity)
                return principal;

            // Remove existing Permissions claims if any
            var existingClaims = identity.FindAll(AppUserClaims.Permissions).ToList();
            foreach (var claim in existingClaims)
            {
                identity.RemoveClaim(claim);
            }

            // Get all relevant permissions for the user
            var permissions = await _userPermissionAppService.GetAllRelevantPermissionsByUserIdAsync(user.Id);

            // Add permissions as claims
            foreach (var permission in permissions)
            {
                if (!string.IsNullOrEmpty(permission.Name))
                {
                    identity.AddClaim(new Claim(AppUserClaims.Permissions, permission.Name));
                }
            }

            return principal;
        }
    }
}