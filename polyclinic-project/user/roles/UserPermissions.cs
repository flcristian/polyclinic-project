using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.roles
{
    public class UserPermissions
    {
        public const string USER_VIEW_SELF = "user:view_self";
        public const string USER_VIEW_USERS = "user:view_users";
        public const string USER_VIEW_PATIENTS = "user:view_patients";
        public const string USER_VIEW_DOCTORS = "user:view_doctors";
        public const string USER_EDIT_SELF = "user:edit_self";
        public const string USER_EDIT = "user:edit";
        public const string USER_DELETE = "user:delete";

        public const string APPOINTMENT_VIEW_SELF = "appointment:view_self";
        public const string APPOINTMENT_VIEW = "appointment:view";
        public const string APPOINTMENT_CREATE = "appointment:create";
        public const string APPOINTMENT_EDIT = "appointment:edit";
        public const string APPOINTMENT_CANCEL = "appointment:cancel";
    }
}
