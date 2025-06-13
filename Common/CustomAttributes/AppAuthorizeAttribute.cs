using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using zintersid.Common.Constants;

namespace zintersid.Common.CustomAttributes
{
    public class AppAuthorizeAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly string[] _permissionNames;

        public AppAuthorizeAttribute(params string[] permissionNames)
        {
            _permissionNames = permissionNames;
        }

        // public void OnAuthorization(AuthorizationFilterContext context)
        // {
        //     var userManager = context.HttpContext.RequestServices.GetRequiredService<UserManager<AppUser>>();
        //     var dbContext = context.HttpContext.RequestServices.GetRequiredService<AppDbContext>();

        //     var userId = userManager.GetUserId(context.HttpContext.User);

        //     var hasPermission = dbContext.AppUserPermissions
        //         .Any(up => up.UserId == userId && _permissionNames.Contains(up.Permission.Name)) ||
        //         dbContext.UserRoles
        //         .Where(ur => ur.UserId == userId)
        //         .SelectMany(ur => ur.Role.RolePermissions ?? new List<AppRolePermission>())
        //         .Any(rp => _permissionNames.Contains(rp.Permission.Name));

        //     if (!hasPermission)
        //     {
        //         context.Result = new ForbidResult();
        //     }
        // }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var user = context.HttpContext.User;
            var userPermissions = user.Claims
                .Where(c => c.Type == AppUserClaims.Permissions)
                .Select(c => c.Value)
                .ToList();

            var hasPermission = _permissionNames.Any(userPermissions.Contains);

            if (!hasPermission)
            {
                context.Result = new ForbidResult();
            }
        }
    }
}