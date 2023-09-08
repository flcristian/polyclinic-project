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
        PACIENT,
        NONE
    }

    public static class UserTypeMappings
    {
        private static readonly Dictionary<UserType, string> _map = new Dictionary<UserType, string>
       {
        { UserType.DOCTOR, "Doctor" },
        { UserType.PACIENT, "Pacient" },
        { UserType.NONE, "None" },
       };

        public static string GetString(this UserType value)
        {
            return _map.TryGetValue(value, out var result) ? result : throw new ArgumentException($"No mapping defined for {value}");
        }
    }
}
