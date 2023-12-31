﻿using polyclinic_project.appointment.model;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.service;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.view.interfaces;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace polyclinic_project.view
{
    public class ViewAdmin : IView
    {
        private User _user;
        private IUserCommandService _userCommandService;
        private IUserQueryService _userQueryService;
        private IAppointmentCommandService _appointmentCommandService;
        private IAppointmentQueryService _appointmentQueryService;
        private IUserAppointmentCommandService _userAppointmentCommandService;
        private IUserAppointmentQueryService _userAppointmentQueryService;

        #region CONSTRUCTORS

        public ViewAdmin(User user)
        {
            _user = user;
            _userCommandService = UserCommandServiceSingleton.Instance;
            _userQueryService = UserQueryServiceSingleton.Instance;
            _appointmentCommandService = AppointmentCommandServiceSingleton.Instance;
            _appointmentQueryService = AppointmentQueryServiceSingleton.Instance;
            _userAppointmentCommandService = UserAppointmentCommandServiceSingleton.Instance;
            _userAppointmentQueryService = UserAppointmentQueryServiceSingleton.Instance;
        }

        #endregion

        #region PUBLIC_METHODS

        public void RunMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Your options :\n");
                DisplayMenuOptions();
                Console.WriteLine("Enter what you want to do :");

                string input = Console.ReadLine()!;
                LineBreak();
                switch (input)
                {
                    case "1":
                        UserMenu();
                        break;
                    case "2":
                        AppointmentMenu();
                        break;
                    case "3":
                        PersonalMenu();
                        break;
                    default:
                        running = false;
                        break;
                }
            }
        }

        #endregion

        #region PRIVATE_METHODS

        // Menus

        private void UserMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Your options :\n");
                DisplayUserOptions();
                Console.WriteLine("Enter what you want to do :");

                string input = Console.ReadLine()!;
                LineBreak();
                switch (input)
                {
                    case "1":
                        ViewAllUsers();
                        break;
                    case "2":
                        ViewAllPatients();
                        break;
                    case "3":
                        ViewAllDoctors();
                        break;
                    case "4":
                        ViewAllAdmins();
                        break;
                    case "5":
                        ViewUserDetails();
                        break;
                    case "6":
                        EditUserName();
                        break;
                    case "7":
                        EditUserEmail();
                        break;
                    case "8":
                        EditUserPassword();
                        break;
                    case "9":
                        EditUserPhone();
                        break;
                    case "10":
                        AssignDoctor();
                        break;
                    case "11":
                        MakePatient();
                        break;
                    case "12":
                        AssignAdmin();
                        break;
                    case "13":
                        DeleteUser();
                        break;
                    default:
                        running = false;
                        break;
                }
                if (running)
                {
                    WaitForUser();
                    LineBreak();
                }
            }
        }

        private void AppointmentMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Your options :\n");
                DisplayAppointmentOptions();
                Console.WriteLine("Enter what you want to do :");

                string input = Console.ReadLine()!;
                LineBreak();
                switch (input)
                {
                    case "1":
                        ViewAllAppointments();
                        break;
                    case "2":
                        ChangeAppointmentPatient();
                        break;
                    case "3":
                        ChangeAppointmentDoctor();
                        break;
                    case "4":
                        ChangeAppointmentDates();
                        break;
                    case "5":
                        CancelAppointment();
                        break;
                    default:
                        running = false;
                        break;
                }
                if (running)
                {
                    WaitForUser();
                    LineBreak();
                }
            }
        }

        private void PersonalMenu()
        {
            bool running = true;
            while (running)
            {
                Console.WriteLine("Your options :\n");
                DisplayPersonalOptions();
                Console.WriteLine("Enter what you want to do :");

                string input = Console.ReadLine()!;
                LineBreak();
                switch (input)
                {
                    case "1":
                        ViewPersonalDetails();
                        break;
                    case "2":
                        UpdateName();
                        break;
                    case "3":
                        UpdateEmail();
                        break;
                    case "4":
                        UpdatePassword();
                        break;
                    case "5":
                        UpdatePhone();
                        break;
                    default:
                        running = false;
                        break;
                }
                if (running)
                {
                    WaitForUser();
                    LineBreak();
                }
            }
        }

        // Functionalities

        private void ViewAllUsers()
        {
            List<User> users;
            try
            {
                users = _userQueryService.GetList();
            }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("There are no users.");
                return;
            }

            foreach(User user in users)
            {
                Console.WriteLine(user.ToStringAdmin());
            }
        }

        private void ViewAllPatients()
        {
            List<User> patients;
            try
            {
                patients = _userQueryService.GetPatientList();
            }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("There are no patients.");
                return;
            }

            foreach (User patient in patients)
            {
                Console.WriteLine(patient.ToStringAdminNoRole());
            }
        }

        private void ViewAllDoctors()
        {
            List<User> doctors;
            try
            {
                doctors = _userQueryService.GetDoctorList();
            }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("There are no doctors.");
                return;
            }

            foreach (User doctor in doctors)
            {
                Console.WriteLine(doctor.ToStringAdminNoRole());
            }
        }

        private void ViewAllAdmins()
        {
            List<User> admins;
            try
            {
                admins = _userQueryService.GetAdminList();
            }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("There are no admins.");
                return;
            }

            foreach (User admin in admins)
            {
                Console.WriteLine(admin.ToStringAdminNoRole());
            }
        }

        private void ViewUserDetails()
        {
            Console.WriteLine("Enter the ID or email of the user you want to see the details of.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    return;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        return;
                    }
                }
            }

            Console.WriteLine("\n" + user);
        }

        private void EditUserName()
        {
            Console.WriteLine("Enter the ID or email of the user you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    return;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        return;
                    }
                }
            }

            Console.WriteLine("\nEnter the user's new name :");
            string name = Console.ReadLine()!;
            while (!IsValidName(name))
            {
                Console.WriteLine("\nPlease enter a valid name (only letters) :");
                name = Console.ReadLine()!;
            }

            user.SetName(name);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully edited user!");
        }

        private void EditUserEmail()
        {
            Console.WriteLine("Enter the ID or email of the user you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    return;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        return;
                    }
                }
            }

            Console.WriteLine("\nEnter the user's new email :");
            string email = Console.ReadLine()!;
            bool unique = false;
            while (!unique)
            {
                if (!IsValidEmailAddress(email))
                {
                    Console.WriteLine("\nInvalid email address.");
                    Console.WriteLine("Please try again :");
                    email = Console.ReadLine()!;
                }
                else if (email == user.GetEmail())
                {
                    Console.WriteLine("\nYou can't change the email to the same one as before!");
                    Console.WriteLine("Please try again :");
                    email = Console.ReadLine()!;
                }
                else
                {
                    try
                    {
                        _userQueryService.FindByEmail(email);
                        unique = false;
                        Console.WriteLine("\nThis email is already used.");
                        Console.WriteLine("Please try again :");
                        email = Console.ReadLine()!;
                    }
                    catch (ItemDoesNotExist)
                    {
                        unique = true;
                    }
                }
            }

            user.SetEmail(email);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully edited user!");
        }

        private void EditUserPassword()
        {
            Console.WriteLine("Enter the ID or email of the user you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    return;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        return;
                    }
                }
            }

            Console.WriteLine("\nEnter the user's new password :");
            string password = Console.ReadLine()!;
            while (!IsValidPassword(password))
            {
                Console.WriteLine("\nPlease enter a valid password (must be at least 4 characters) :");
                password = Console.ReadLine()!;
            }

            user.SetPassword(password);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully edited user!");
        }

        private void EditUserPhone()
        {
            Console.WriteLine("Enter the ID or email of the user you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    return;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        return;
                    }
                }
            }

            Console.WriteLine("\nEnter the user's new phone :");
            string phone = Console.ReadLine()!;
            bool unique = false;
            while (!unique)
            {
                if (!IsValidPhoneNumber(phone))
                {
                    Console.WriteLine("\nInvalid phone number.");
                    Console.WriteLine("Please try again :");
                    phone = Console.ReadLine()!;
                }
                else if (phone == _user.GetPhone())
                {
                    Console.WriteLine("\nYou can't change the phone number to the same one as before!");
                    Console.WriteLine("Please try again :");
                    phone = Console.ReadLine()!;
                }
                else
                {
                    try
                    {
                        _userQueryService.FindByPhone(phone);
                        unique = false;
                        Console.WriteLine("\nThis phone number is already used.");
                        Console.WriteLine("Please try again :");
                        phone = Console.ReadLine()!;
                    }
                    catch (ItemDoesNotExist)
                    {
                        unique = true;
                    }
                }
            }

            user.SetPhone(phone);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully edited user!");
        }

        private void AssignDoctor()
        {
            Console.WriteLine("Enter the ID or email of the user you want to make a doctor.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    Console.WriteLine("Please try again :");
                    identifier = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        Console.WriteLine("Please try again :");
                        identifier = Console.ReadLine()!;
                    }
                }
            }

            if (user.GetType() == UserType.DOCTOR)
            {
                Console.WriteLine("\nThis user is already a doctor!");
                return;
            }

            user.SetType(UserType.DOCTOR);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully assigned doctor role to user!");
        }

        private void MakePatient()
        {
            Console.WriteLine("Enter the ID or email of the user you want to make a patient.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    Console.WriteLine("Please try again :");
                    identifier = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        Console.WriteLine("Please try again :");
                        identifier = Console.ReadLine()!;
                    }
                }
            }

            if (user.GetType() == UserType.PATIENT)
            {
                Console.WriteLine("\nThis user is already a patient!");
                return;
            }

            user.SetType(UserType.PATIENT);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully made user a patient!");
        }

        private void AssignAdmin()
        {
            Console.WriteLine("Enter the ID or email of the user you want to make an admin.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    Console.WriteLine("Please try again :");
                    identifier = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        Console.WriteLine("Please try again :");
                        identifier = Console.ReadLine()!;
                    }
                }
            }

            if (user.GetType() == UserType.ADMIN)
            {
                Console.WriteLine("\nThis user is already an admin!");
                return;
            }

            user.SetType(UserType.ADMIN);
            _userCommandService.Update(user);
            Console.WriteLine("\nSuccessfully assigned admin role to user!");
        }

        private void DeleteUser()
        {
            Console.WriteLine("Enter the ID or email of the user you want to delete.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User user = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    user = _userQueryService.FindById(Int32.Parse(identifier));
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo user has that id.");
                    Console.WriteLine("Please try again :");
                    identifier = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        user = _userQueryService.FindByEmail(identifier);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo user has that email.");
                        Console.WriteLine("Please try again :");
                        identifier = Console.ReadLine()!;
                    }
                }
            }

            if(user.GetType() == UserType.ADMIN)
            {
                Console.WriteLine("\nCan't delete an admin!");
                return;
            }

            Console.WriteLine("\nThis will delete all the user's appointments too!");
            Console.WriteLine("Do you want to continue? Enter \"YES\" to continue.");
            string choice = Console.ReadLine()!;
            if (choice.ToLower().Equals("yes"))
            {
                List<UserAppointment> userAppointments = new List<UserAppointment>();

                if(user.GetType() == UserType.PATIENT)
                {
                    try
                    {
                        userAppointments = _userAppointmentQueryService.FindByPatientId(user.GetId());
                    }
                    catch (ItemsDoNotExist) { }
                }
                else
                {
                    try
                    {
                        userAppointments = _userAppointmentQueryService.FindByDoctorId(user.GetId());
                    }
                    catch (ItemsDoNotExist) { }
                }

                while(userAppointments.Count > 0)
                {
                    Appointment appointment = _userAppointmentQueryService.FindAppointmentByUserAppointmentId(userAppointments[0].GetId());
                    _userAppointmentCommandService.Delete(userAppointments[0]);
                    _appointmentCommandService.Delete(appointment);
                    userAppointments.RemoveAt(0);
                }
                _userCommandService.Delete(user);
                Console.WriteLine("\nUser was successfully deleted!");
                return;
            }

            Console.WriteLine("\nNo users were deleted.");
        }

        private void ViewAllAppointments()
        {
            List<AdminViewAllAppointmentsResponse> responses = null!;
            try { responses = _userAppointmentQueryService.ObtainAllAppointmentDetails(); }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("There are no appointments.\n");
                return;
            }

            Console.WriteLine("Here is the appointments list :\n");
            for(int i = 0; i < responses.Count; i++)
            {
                AdminViewAllAppointmentsResponse response = responses[i];
                string message = $"{i + 1}. ";

                if (response.Appointment.GetStartDate().DayOfYear == response.Appointment.GetEndDate().DayOfYear)
                    message += response.Appointment.GetStartDate().ToString(Constants.STANDARD_DATE_FORMAT) + " - " + response.Appointment.GetEndDate().ToString(Constants.STANDARD_DATE_DAYTIME_ONLY);
                else message += response.Appointment.GetStartDate().ToString(Constants.STANDARD_DATE_FORMAT) + " - " + response.Appointment.GetEndDate().ToString(Constants.STANDARD_DATE_FORMAT);
                message += $"\nPatient {response.Patient.GetName()}\n";
                message += $"Patient email : {response.Patient.GetEmail()}\n";
                message += $"Patient phone : {response.Patient.GetPhone()}\n";
                message += $"With dr. {response.Doctor.GetName()}\n";
                message += $"Doctor email : {response.Doctor.GetEmail()}\n";
                message += $"Doctor phone : {response.Doctor.GetPhone()}\n";

                Console.WriteLine(message);
            }
        }

        private void ChangeAppointmentPatient()
        {
            Console.WriteLine("Enter the ID or email of the patient who's appointment you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier1 = Console.ReadLine()!;
            User patient = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    patient = _userQueryService.FindById(Int32.Parse(identifier1));
                    if (patient.GetType() != UserType.PATIENT)
                    {
                        throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                    }
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo patient has that id.");
                    Console.WriteLine("Please try again :");
                    identifier1 = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        patient = _userQueryService.FindByEmail(identifier1);
                        if (patient.GetType() != UserType.PATIENT)
                        {
                            throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                        }
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo patient has that email.");
                        Console.WriteLine("Please try again :");
                        identifier1 = Console.ReadLine()!;
                    }
                }
            }

            Console.WriteLine("\nEnter the date of the appointment you want to cancel (Example : 21.03.2022)");
            Console.WriteLine("Please enter a date starting from today :");
            String dateString = Console.ReadLine()!;
            DateTime date = DateTime.MinValue;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    date = DateTime.ParseExact(dateString, Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture);
                    if (date < DateTime.Now) throw new FormatException();
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect date. Use this as an example : 21.03.2022");
                    Console.WriteLine("Reminder, you must enter a date starting from the current one onward.");
                    Console.WriteLine("Please try again :");
                    dateString = Console.ReadLine()!;
                }
            }

            Console.WriteLine("\nEnter the hour and minute of the appointment you want to cancel (Example : 08:00)");
            String daytimeString = Console.ReadLine()!;
            parsed = false;
            DateTime daytime = DateTime.MinValue;
            while (!parsed)
            {
                try
                {
                    daytime = DateTime.ParseExact(daytimeString, Constants.STANDARD_DATE_DAYTIME_ONLY, CultureInfo.InvariantCulture);
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect time. Use this as an example : 13:00");
                    Console.WriteLine("Please try again :");
                    daytimeString = Console.ReadLine()!;
                }
            }

            parsed = false;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            Console.WriteLine("\nAppointment length must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
            Console.WriteLine("Enter how long the appointment is in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
            String minutesString = Console.ReadLine()!;
            int minutes = 0;
            while (!Int32.TryParse(minutesString, out minutes) || minutes < 30 || minutes > 120 || minutes % 5 != 0)
            {
                Console.WriteLine("\nYou have entered an incorrect.");
                Console.WriteLine("Reminder, your number needs to be between 30 and 120 minutes and in multiples of 5!");
                Console.WriteLine("Examples : 30, 35, 55, etc.");
                Console.WriteLine("Please try again :");
                minutesString = Console.ReadLine()!;
            }
            duration = new TimeSpan(0, minutes, 0);

            DateTime start = date + new TimeSpan(daytime.Hour, daytime.Minute, 0);

            UserAppointment update = null!;
            try
            {
                update = _userAppointmentQueryService.FindByPatientIdAndDates(patient.GetId(), start, start + duration);
            }
            catch (ItemDoesNotExist)
            {
                Console.WriteLine("\nThat patient has no appointments scheduled at that date and time.");
                return;
            }

            Console.WriteLine("\nEnter the ID or email of the new patient.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier2 = Console.ReadLine()!;
            User change = null!;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    change = _userQueryService.FindById(Int32.Parse(identifier2));
                    if (patient.GetType() != UserType.PATIENT)
                    {
                        throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                    }
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo patient has that id.");
                    Console.WriteLine("Please try again :");
                    identifier2 = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        change = _userQueryService.FindByEmail(identifier2);
                        if (patient.GetType() != UserType.PATIENT)
                        {
                            throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                        }
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo patient has that email.");
                        Console.WriteLine("Please try again :");
                        identifier2 = Console.ReadLine()!;
                    }
                }
            }

            update.SetPatientId(change.GetId());
            try
            {
                update = _userAppointmentQueryService.FindByPatientIdAndDates(change.GetId(), start, start + duration);
                Console.WriteLine("\nThat patient already has an appointment scheduled at that date and time.");
                return;
            }
            catch (ItemDoesNotExist) { }

            _userAppointmentCommandService.Update(update);
            Console.WriteLine("\nSuccessfully modified your appointment!");
        }

        private void ChangeAppointmentDoctor()
        {
            Console.WriteLine("Enter the ID or email of the doctor who's appointment you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier1 = Console.ReadLine()!;
            User doctor = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    doctor = _userQueryService.FindById(Int32.Parse(identifier1));
                    if (doctor.GetType() != UserType.DOCTOR)
                    {
                        throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                    }
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo doctor has that id.");
                    Console.WriteLine("Please try again :");
                    identifier1 = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        doctor = _userQueryService.FindByEmail(identifier1);
                        if (doctor.GetType() != UserType.DOCTOR)
                        {
                            throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                        }
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo doctor has that email.");
                        Console.WriteLine("Please try again :");
                        identifier1 = Console.ReadLine()!;
                    }
                }
            }

            Console.WriteLine("\nEnter the date of the appointment you want to cancel (Example : 21.03.2022)");
            Console.WriteLine("Please enter a date starting from today :");
            String dateString = Console.ReadLine()!;
            DateTime date = DateTime.MinValue;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    date = DateTime.ParseExact(dateString, Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture);
                    if (date < DateTime.Now) throw new FormatException();
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect date. Use this as an example : 21.03.2022");
                    Console.WriteLine("Reminder, you must enter a date starting from the current one onward.");
                    Console.WriteLine("Please try again :");
                    dateString = Console.ReadLine()!;
                }
            }

            Console.WriteLine("\nEnter the hour and minute of the appointment you want to cancel (Example : 08:00)");
            String daytimeString = Console.ReadLine()!;
            parsed = false;
            DateTime daytime = DateTime.MinValue;
            while (!parsed)
            {
                try
                {
                    daytime = DateTime.ParseExact(daytimeString, Constants.STANDARD_DATE_DAYTIME_ONLY, CultureInfo.InvariantCulture);
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect time. Use this as an example : 13:00");
                    Console.WriteLine("Please try again :");
                    daytimeString = Console.ReadLine()!;
                }
            }

            parsed = false;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            Console.WriteLine("\nAppointment length must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
            Console.WriteLine("Enter how long the appointment is in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
            String minutesString = Console.ReadLine()!;
            int minutes = 0;
            while (!Int32.TryParse(minutesString, out minutes) || minutes < 30 || minutes > 120 || minutes % 5 != 0)
            {
                Console.WriteLine("\nYou have entered an incorrect.");
                Console.WriteLine("Reminder, your number needs to be between 30 and 120 minutes and in multiples of 5!");
                Console.WriteLine("Examples : 30, 35, 55, etc.");
                Console.WriteLine("Please try again :");
                minutesString = Console.ReadLine()!;
            }
            duration = new TimeSpan(0, minutes, 0);

            DateTime start = date + new TimeSpan(daytime.Hour, daytime.Minute, 0);

            UserAppointment update = null!;
            try
            {
                update = _userAppointmentQueryService.FindByDoctorIdAndDates(doctor.GetId(), start, start + duration);
            }
            catch (ItemDoesNotExist)
            {
                Console.WriteLine("\nThat doctor has no appointments scheduled at that date and time.");
                return;
            }

            Console.WriteLine("\nEnter the ID or email of the new doctor.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier2 = Console.ReadLine()!;
            User change = null!;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    change = _userQueryService.FindById(Int32.Parse(identifier2));
                    if (doctor.GetType() != UserType.DOCTOR)
                    {
                        throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                    }
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo doctor has that id.");
                    Console.WriteLine("Please try again :");
                    identifier2 = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        change = _userQueryService.FindByEmail(identifier2);
                        if (doctor.GetType() != UserType.DOCTOR)
                        {
                            throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                        }
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo doctor has that email.");
                        Console.WriteLine("Please try again :");
                        identifier2 = Console.ReadLine()!;
                    }
                }
            }

            update.SetDoctorId(change.GetId());
            try
            {
                update = _userAppointmentQueryService.FindByDoctorIdAndDates(change.GetId(), start, start + duration);
                Console.WriteLine("\nThat doctor already has an appointment scheduled at that date and time.");
                return;
            }
            catch (ItemDoesNotExist) { }

            _userAppointmentCommandService.Update(update);
            Console.WriteLine("\nSuccessfully modified your appointment!");
        }

        private void ChangeAppointmentDates()
        {
            Console.WriteLine("Enter the ID or email of the patient who's appointment you want to modify.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier1 = Console.ReadLine()!;
            User patient = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    patient = _userQueryService.FindById(Int32.Parse(identifier1));
                    if (patient.GetType() != UserType.PATIENT)
                    {
                        throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                    }
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo patient has that id.");
                    Console.WriteLine("Please try again :");
                    identifier1 = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        patient = _userQueryService.FindByEmail(identifier1);
                        if (patient.GetType() != UserType.PATIENT)
                        {
                            throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                        }
                        parsed = true;
                    }
                    catch (ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo patient has that email.");
                        Console.WriteLine("Please try again :");
                        identifier1 = Console.ReadLine()!;
                    }
                }
            }

            Console.WriteLine("\nEnter the date of the appointment you want to cancel (Example : 21.03.2022)");
            Console.WriteLine("Please enter a date starting from today :");
            String dateString = Console.ReadLine()!;
            DateTime date = DateTime.MinValue;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    date = DateTime.ParseExact(dateString, Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture);
                    if (date < DateTime.Now) throw new FormatException();
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect date. Use this as an example : 21.03.2022");
                    Console.WriteLine("Reminder, you must enter a date starting from the current one onward.");
                    Console.WriteLine("Please try again :");
                    dateString = Console.ReadLine()!;
                }
            }

            Console.WriteLine("\nEnter the hour and minute of the appointment you want to cancel (Example : 08:00)");
            String daytimeString = Console.ReadLine()!;
            parsed = false;
            DateTime daytime = DateTime.MinValue;
            while (!parsed)
            {
                try
                {
                    daytime = DateTime.ParseExact(daytimeString, Constants.STANDARD_DATE_DAYTIME_ONLY, CultureInfo.InvariantCulture);
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect time. Use this as an example : 13:00");
                    Console.WriteLine("Please try again :");
                    daytimeString = Console.ReadLine()!;
                }
            }

            parsed = false;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            Console.WriteLine("\nAppointment length must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
            Console.WriteLine("Enter how long the appointment is in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
            String minutesString = Console.ReadLine()!;
            int minutes = 0;
            while (!Int32.TryParse(minutesString, out minutes) || minutes < 30 || minutes > 120 || minutes % 5 != 0)
            {
                Console.WriteLine("\nYou have entered an incorrect.");
                Console.WriteLine("Reminder, your number needs to be between 30 and 120 minutes and in multiples of 5!");
                Console.WriteLine("Examples : 30, 35, 55, etc.");
                Console.WriteLine("Please try again :");
                minutesString = Console.ReadLine()!;
            }
            duration = new TimeSpan(0, minutes, 0);

            DateTime start = date + new TimeSpan(daytime.Hour, daytime.Minute, 0);

            UserAppointment update = null!;
            try
            {
                update = _userAppointmentQueryService.FindByPatientIdAndDates(patient.GetId(), start, start + duration);
            }
            catch (ItemDoesNotExist)
            {
                Console.WriteLine("\nThat patient has no appointments scheduled at that date and time.");
                return;
            }

            Console.WriteLine("\nEnter the new date of the appointment (Example : 21.03.2022)");
            Console.WriteLine("Please enter a date starting from today :");
            String newDateString = Console.ReadLine()!;
            DateTime newDate = DateTime.MinValue;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    newDate = DateTime.ParseExact(newDateString, Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture);
                    if (newDate < DateTime.Now) throw new FormatException();
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect date. Use this as an example : 21.03.2022");
                    Console.WriteLine("Reminder, you must enter a date starting from the current one onward.");
                    Console.WriteLine("Please try again :");
                    newDateString = Console.ReadLine()!;
                }
            }

            Console.WriteLine("\nEnter the new hour and minute of the appointment (Example : 08:00)");
            String newDaytimeString = Console.ReadLine()!;
            parsed = false;
            DateTime newDaytime = DateTime.MinValue;
            while (!parsed)
            {
                try
                {
                    newDaytime = DateTime.ParseExact(newDaytimeString, Constants.STANDARD_DATE_DAYTIME_ONLY, CultureInfo.InvariantCulture);
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect time. Use this as an example : 13:00");
                    Console.WriteLine("Please try again :");
                    newDaytimeString = Console.ReadLine()!;
                }
            }

            parsed = false;
            TimeSpan newDuration = new TimeSpan(0, 0, 0);
            Console.WriteLine("\nAppointment length must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
            Console.WriteLine("Enter the new appoinment duration in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
            String newMinutesString = Console.ReadLine()!;
            int newMinutes = 0;
            while (!Int32.TryParse(newMinutesString, out newMinutes) || newMinutes < 30 || newMinutes > 120 || newMinutes % 5 != 0)
            {
                Console.WriteLine("\nYou have entered an incorrect.");
                Console.WriteLine("Reminder, your number needs to be between 30 and 120 minutes and in multiples of 5!");
                Console.WriteLine("Examples : 30, 35, 55, etc.");
                Console.WriteLine("Please try again :");
                newMinutesString = Console.ReadLine()!;
            }
            newDuration = new TimeSpan(0, newMinutes, 0);

            DateTime newStart = newDate + new TimeSpan(newDaytime.Hour, newDaytime.Minute, 0);


            try
            {
                update = _userAppointmentQueryService.FindByPatientIdAndDates(patient.GetId(), newStart, newStart + newDuration);
                Console.WriteLine("\nThat patient already has an appointment scheduled at that date and time.");
                return;
            }
            catch (ItemDoesNotExist) { }

            try
            {
                update = _userAppointmentQueryService.FindByDoctorIdAndDates(update.GetDoctorId(), newStart, newStart + newDuration);
                Console.WriteLine("\nThat doctor already has an appointment scheduled at that date and time.");
                return;
            }
            catch (ItemDoesNotExist) { }

            Appointment appointment = _userAppointmentQueryService.FindAppointmentByUserAppointmentId(update.GetId());
            appointment.SetStartDate(newStart);
            appointment.SetEndDate(newStart + newDuration);
            _appointmentCommandService.Update(appointment);
            Console.WriteLine("\nSuccessfully modified your appointment!");
        }

        private void CancelAppointment()
        {
            Console.WriteLine("Enter the ID or email of the patient who's appointment you want to cancel.");
            Console.WriteLine("Please enter a valid number or email address :");
            String identifier = Console.ReadLine()!;
            User patient = null!;
            bool parsed = false;
            while (!parsed)
            {
                try
                {
                    patient = _userQueryService.FindById(Int32.Parse(identifier));
                    if (patient.GetType() != UserType.PATIENT)
                    {
                        throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                    }
                    parsed = true;
                }
                catch (ItemDoesNotExist)
                {
                    Console.WriteLine("\nNo patient has that id.");
                    Console.WriteLine("Please try again :");
                    identifier = Console.ReadLine()!;
                }
                catch (FormatException)
                {
                    try
                    {
                        patient = _userQueryService.FindByEmail(identifier);
                        if (patient.GetType() != UserType.PATIENT)
                        {
                            throw new ItemDoesNotExist(Constants.USER_NOT_PATIENT);
                        }
                        parsed = true;
                    }
                    catch(ItemDoesNotExist)
                    {
                        Console.WriteLine("\nNo patient has that email.");
                        Console.WriteLine("Please try again :");
                        identifier = Console.ReadLine()!;
                    }
                }
            }

            Console.WriteLine("\nEnter the date of the appointment you want to cancel (Example : 21.03.2022)");
            Console.WriteLine("Please enter a date starting from today :");
            String dateString = Console.ReadLine()!;
            DateTime date = DateTime.MinValue;
            parsed = false;
            while (!parsed)
            {
                try
                {
                    date = DateTime.ParseExact(dateString, Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture);
                    if (date < DateTime.Now) throw new FormatException();
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect date. Use this as an example : 21.03.2022");
                    Console.WriteLine("Reminder, you must enter a date starting from the current one onward.");
                    Console.WriteLine("Please try again :");
                    dateString = Console.ReadLine()!;
                }
            }

            Console.WriteLine("\nEnter the hour and minute of the appointment you want to cancel (Example : 08:00)");
            String daytimeString = Console.ReadLine()!;
            parsed = false;
            DateTime daytime = DateTime.MinValue;
            while (!parsed)
            {
                try
                {
                    daytime = DateTime.ParseExact(daytimeString, Constants.STANDARD_DATE_DAYTIME_ONLY, CultureInfo.InvariantCulture);
                    parsed = true;
                }
                catch (FormatException)
                {
                    Console.WriteLine("\nYou have entered an incorrect time. Use this as an example : 13:00");
                    Console.WriteLine("Please try again :");
                    daytimeString = Console.ReadLine()!;
                }
            }

            parsed = false;
            TimeSpan duration = new TimeSpan(0, 0, 0);
            Console.WriteLine("\nAppointment length must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
            Console.WriteLine("Enter how long the appointment is in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
            String minutesString = Console.ReadLine()!;
            int minutes = 0;
            while (!Int32.TryParse(minutesString, out minutes) || minutes < 30 || minutes > 120 || minutes % 5 != 0)
            {
                Console.WriteLine("\nYou have entered an incorrect.");
                Console.WriteLine("Reminder, your number needs to be between 30 and 120 minutes and in multiples of 5!");
                Console.WriteLine("Examples : 30, 35, 55, etc.");
                Console.WriteLine("Please try again :");
                minutesString = Console.ReadLine()!;
            }
            duration = new TimeSpan(0, minutes, 0);

            DateTime start = date + new TimeSpan(daytime.Hour, daytime.Minute, 0);

            UserAppointment delete = null!;
            try
            {
                delete = _userAppointmentQueryService.FindByPatientIdAndDates(patient.GetId(), start, start + duration);
            }
            catch (ItemDoesNotExist)
            {
                Console.WriteLine("\nThat patient has no appointments scheduled at that date and time.");
                return;
            }

            _userAppointmentCommandService.Delete(delete);
            _appointmentCommandService.DeleteById(delete.GetAppointmentId());
            Console.WriteLine("\nSuccessfully canceled your appointment!");
        }

        private void ViewPersonalDetails()
        {
            Console.WriteLine("Here are your personal details :");
            Console.WriteLine(_user);
        }

        private void UpdateName()
        {
            Console.WriteLine("\nEnter your new name :");
            string name = Console.ReadLine()!;
            while (!IsValidName(name))
            {
                Console.WriteLine("\nPlease enter a valid name (only letters) :");
                name = Console.ReadLine()!;
            }

            _user.SetName(name);
            _userCommandService.Update(_user);
            Console.WriteLine("\nYour name was successfully updated!");
        }

        private void UpdateEmail()
        {
            Console.WriteLine("Enter your new email :");
            string email = Console.ReadLine()!;
            bool unique = false;
            while (!unique)
            {
                if (!IsValidEmailAddress(email))
                {
                    Console.WriteLine("\nInvalid email address.");
                    Console.WriteLine("Please try again :");
                    email = Console.ReadLine()!;
                }
                else if (email == _user.GetEmail())
                {
                    Console.WriteLine("\nYou can't change the email to the same one as before!");
                    Console.WriteLine("Please try again :");
                    email = Console.ReadLine()!;
                }
                else
                {
                    try
                    {
                        _userQueryService.FindByEmail(email);
                        unique = false;
                        Console.WriteLine("\nThis email is already used.");
                        Console.WriteLine("Please try again :");
                        email = Console.ReadLine()!;
                    }
                    catch (ItemDoesNotExist)
                    {
                        unique = true;
                    }
                }
            }
            _user.SetEmail(email);
            _userCommandService.Update(_user);
            Console.WriteLine("\nYour email has been successfully updated!");
        }

        private void UpdatePassword()
        {
            Console.WriteLine("\nEnter your new password (must be at least 4 characters) :");
            string password = Console.ReadLine()!;
            while (!IsValidPassword(password))
            {
                Console.WriteLine("\nPlease enter a valid password (must be at least 4 characters) :");
                password = Console.ReadLine()!;
            }

            _user.SetPassword(password);
            _userCommandService.Update(_user);
            Console.WriteLine("\nYour password was successfully updated!");
        }

        private void UpdatePhone()
        {
            Console.WriteLine("Enter your new phone number :");
            string phone = Console.ReadLine()!;
            bool unique = false;
            while (!unique)
            {
                if (!IsValidPhoneNumber(phone))
                {
                    Console.WriteLine("\nInvalid phone number.");
                    Console.WriteLine("Please try again :");
                    phone = Console.ReadLine()!;
                }
                else if (phone == _user.GetPhone())
                {
                    Console.WriteLine("\nYou can't change the phone number to the same one as before!");
                    Console.WriteLine("Please try again :");
                    phone = Console.ReadLine()!;
                }
                else
                {
                    try
                    {
                        _userQueryService.FindByPhone(phone);
                        unique = false;
                        Console.WriteLine("\nThis phone number is already used.");
                        Console.WriteLine("Please try again :");
                        phone = Console.ReadLine()!;
                    }
                    catch (ItemDoesNotExist)
                    {
                        unique = true;
                    }
                }
            }
            _user.SetPhone(phone);
            _userCommandService.Update(_user);
            Console.WriteLine("\nYour phone number has been successfully updated!");
        }


        // Menu Methods

        private void DisplayMenuOptions()
        {
            string options = "";
            options += "1. Open user menu\n";
            options += "2. Open appointments menu\n";
            options += "3. Open personal menu\n";
            options += "Anything else to log out";
            Console.WriteLine(options);
        }

        private void DisplayUserOptions()
        {
            string options = "";
            options += "1. View all users\n";
            options += "2. View all patients\n";
            options += "3. View all doctors\n";
            options += "4. View all admins\n";
            options += "5. View user details\n";
            options += "6. Edit user name\n";
            options += "7. Edit user email\n";
            options += "8. Edit user password\n";
            options += "9. Edit user phone\n";
            options += "10. Assign doctor role to user\n";
            options += "11. Make user a patient\n";
            options += "12. Assign admin role to user\n";
            options += "13. Delete user\n";
            options += "Anything else to exit the meu";
            Console.WriteLine(options);
        }

        private void DisplayAppointmentOptions()
        {
            string options = "";
            options += "1. View all appointments\n";
            options += "2. Change an appointment's patient\n";
            options += "3. Change an appointment's doctor\n";
            options += "4. Change an appointment's dates and duration\n";
            options += "5. Cancel an appointment\n";
            options += "Anything else to exit the menu";
            Console.WriteLine(options);
        }

        private void DisplayPersonalOptions()
        {
            string options = "";
            options += "1. View personal details\n";
            options += "2. Update your name\n";
            options += "3. Update your email\n";
            options += "4. Update your password\n";
            options += "5. Update your phone\n";
            options += "Anything else to exit the menu";
            Console.WriteLine(options);
        }

        private void LineBreak()
        {
            Console.WriteLine("\n=-=-=-=-=-=-=-=-=-=\n");
        }

        private void WaitForUser()
        {
            Console.Write("Enter anything to continue");
            Console.ReadLine();
        }

        // Logistics

        private bool IsValidName(string name)
        {
            if (name.Length == 0) return false;

            bool found = false;
            return name.All(character =>
            {
                if (found && char.IsWhiteSpace(character))
                {
                    found = false;
                    return true;
                }
                if (char.IsLetter(character))
                {
                    found = true;
                    return true;
                }
                return false;
            });
        }

        private bool IsValidEmailAddress(string email)
        {
            string pattern = @"^[a-zA-Z0-9._]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            if (phone.Length < 3) return false;

            return phone.All(character =>
            {
                return char.IsDigit(character) || character == '-' ||
                       (character == '+' && phone.IndexOf(character) == 0);
            });
        }

        private bool IsValidPassword(string password)
        {
            return password.Length > 4;
        }

        #endregion
    }
}
