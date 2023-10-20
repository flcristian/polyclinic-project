using polyclinic_project.user.roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.model
{
    public enum UserType
    {
        DOCTOR,
        PATIENT,
        ADMIN,
        NONE
    }

    public static class UserTypeMappings
    {
        private static readonly Dictionary<UserType, string> _map = new Dictionary<UserType, string>
        {
            { UserType.DOCTOR, "Doctor" },
            { UserType.PATIENT, "Patient" },
            { UserType.ADMIN, "Admin" },
            { UserType.NONE, "None" },
        };

        private static readonly Dictionary<UserType, UserRole> _mapRoles = new Dictionary<UserType, UserRole>
        {
            { UserType.DOCTOR, UserRole.Roles.Doctor },
            { UserType.PATIENT, UserRole.Roles.Patient },
            { UserType.ADMIN, UserRole.Roles.Admin },
        };

        public static string GetString(this UserType value)
        {
            return _map.TryGetValue(value, out var result) ? result : throw new ArgumentException($"No mapping defined for {value}");
        }

        public static UserRole GetRole(this UserType value)
        {
            return _mapRoles.TryGetValue(value, out var result) ? result : throw new ArgumentException($"No mapping defined for {value}");
        }
    }
}
