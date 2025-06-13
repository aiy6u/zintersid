using System.ComponentModel;

namespace zintersid.Common.Constants
{
    public class AppPermissions
    {
        [Description("Create user permission")]
        public const string CreateUser = "Permissions.CreateUser";

        [Description("Update user permission")]
        public const string UpdateUser = "Permissions.UpdateUser";

        [Description("Delete user permission")]
        public const string DeleteUser = "Permissions.DeleteUser";

        [Description("View users permission")]
        public const string ViewUsers = "Permissions.ViewUsers";

        [Description("Create role permission")]
        public const string CreateRole = "Permissions.CreateRole";

        [Description("Update role permission")]
        public const string UpdateRole = "Permissions.UpdateRole";

        [Description("Delete role permission")]
        public const string DeleteRole = "Permissions.DeleteRole";

        [Description("View roles permission")]
        public const string ViewRoles = "Permissions.ViewRoles";

        [Description("Assign role to user permission")]
        public const string AssignRoleToUser = "Permissions.AssignRoleToUser";

        [Description("Remove role from user permission")]
        public const string RemoveRoleFromUser = "Permissions.RemoveRoleFromUser";

        [Description("Create permission")]
        public const string CreatePermission = "Permissions.CreatePermission";

        [Description("Update permission")]
        public const string UpdatePermission = "Permissions.UpdatePermission";

        [Description("Delete permission")]
        public const string DeletePermission = "Permissions.DeletePermission";

        [Description("View permissions")]
        public const string ViewPermissions = "Permissions.ViewPermissions";

        [Description("Assign permission to role")]
        public const string AssignPermissionToRole = "Permissions.AssignPermissionToRole";
    }
}