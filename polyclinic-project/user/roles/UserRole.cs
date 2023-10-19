using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.roles
{
    public class UserRole
    {
        public string Name { get; private set; }
        public List<string> Permissions { get; private set; }

        public UserRole(string name, List<string> permissions)
        {
            Name = name;
            Permissions = permissions;
        }

        public static class Roles
        {
            public static UserRole Patient { get; } = new UserRole("patient", new List<string>
            {
                UserPermissions.USER_VIEW_SELF,
                UserPermissions.USER_VIEW_DOCTORS,
                UserPermissions.USER_EDIT_SELF,
                UserPermissions.APPOINTMENT_VIEW_SELF,
                UserPermissions.APPOINTMENT_CREATE,
                UserPermissions.APPOINTMENT_CANCEL
            });

            public static UserRole Doctor { get; } = new UserRole("doctor", new List<string>
            {
                UserPermissions.USER_VIEW_SELF,
                UserPermissions.USER_EDIT_SELF,
                UserPermissions.APPOINTMENT_VIEW_SELF
            });

            public static UserRole Admin { get; } = new UserRole("admin", new List<string>
            {
                UserPermissions.USER_VIEW_SELF,
                UserPermissions.USER_VIEW_USERS,
                UserPermissions.USER_VIEW_PATIENTS,
                UserPermissions.USER_VIEW_DOCTORS,
                UserPermissions.USER_EDIT_SELF,
                UserPermissions.USER_EDIT,
                UserPermissions.USER_DELETE,
                UserPermissions.APPOINTMENT_VIEW_SELF,
                UserPermissions.APPOINTMENT_VIEW,
                UserPermissions.APPOINTMENT_CREATE,
                UserPermissions.APPOINTMENT_EDIT,
                UserPermissions.APPOINTMENT_CANCEL
            });
        }
    }
}
