﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.constants
{
    public class Constants
    {
        // DATE FORMATS
        public static readonly string STANDARD_DATE_FORMAT = "dd.MM.yyyy HH:mm";
        public static readonly string SQL_DATE_FORMAT = "yyyy-MM-dd HH:mm";

        #region EXCEPTION_MESSAGES

        // GENERAL
        public static readonly string ID_ALREADY_USED = "This id is already used.";

        // USER
        public static readonly string USER_DOES_NOT_EXIST = "This user does not exist.";
        public static readonly string PACIENT_DOES_NOT_EXIST = "This pacient does not exist.";
        public static readonly string DOCTOR_DOES_NOT_EXIST = "This doctor does not exist.";
        public static readonly string USER_NOT_MODIFIED = "User was not modified, it doesn't require to be updated.";
        public static readonly string EMAIL_ALREADY_USED = "This email is already used.";
        public static readonly string PHONE_ALREADY_USED = "This phone number is already used.";
        public static readonly string USER_NOT_DOCTOR = "This user is not a doctor.";

        // APPOINTMENT
        public static readonly string APPOINTMENT_DOES_NOT_EXIST = "This appointment does not exist.";
        public static readonly string NO_APPOINTMENT_SCHEDULED = "No appointment is scheduled in that date.";
        public static readonly string INVALID_APPOINTMENT_DATES = "Start date can't be after or the same with the end date.";
        public static readonly string ANOTHER_APPOINTMENT_ALREADY_SCHEDULED = "Another appointment is already scheduled in that date.";
        public static readonly string APPOINTMENT_NOT_MODIFIED = "Appointment was not modified, it doesn't require to be updated.";

        // USER APPOINTMENT
        public static readonly string USER_APPOINTMENT_DOES_NOT_EXIST = "This user appointment does not exist.";

        #endregion
    }
}
