﻿using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.system.models;
using polyclinic_project.user.dtos;
using polyclinic_project.user.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.model.interfaces;
using polyclinic_project.user_appointment.service;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.view.interfaces;
using System.Globalization;
using System.Text.RegularExpressions;

namespace polyclinic_project.view;

public class ViewPatient : IView
{
    private User _user;
    private IUserCommandService _userCommandService;
    private IUserQueryService _userQueryService;
    private IAppointmentCommandService _appointmentCommandService;
    private IAppointmentQueryService _appointmentQueryService;
    private IUserAppointmentCommandService _userAppointmentCommandService;
    private IUserAppointmentQueryService _userAppointmentQueryService;

    #region CONSTRUCTORS

    public ViewPatient(User user)
    {
        _user = user;
        _userCommandService = UserCommandServiceSingleton.Instance;
        _userQueryService = UserQueryServiceSingleton.Instance;
        _userAppointmentCommandService = UserAppointmentCommandServiceSingleton.Instance;
        _userAppointmentQueryService = UserAppointmentQueryServiceSingleton.Instance;
        _appointmentCommandService = AppointmentCommandServiceSingleton.Instance;
        _appointmentQueryService = AppointmentQueryServiceSingleton.Instance;
    }

    #endregion

    #region PUBLIC_METHODS

    public void RunMenu()
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Your options :\n");
            DisplayOptions();
            Console.WriteLine("Enter what you want to do :");

            string input = Console.ReadLine();
            LineBreak();
            switch (input)
            {
                case "1":
                    ViewPersonalDetails();
                    break;
                case "2":
                    ViewAppointments();
                    break;
                case "3":
                    ViewDoctors();
                    break;
                case "4":
                    CheckDoctorFreeTime();
                    break;
                case "5":
                    MakeAppointment();
                    break;
                case "6":
                    CancelAppointment();
                    break;
                case "7":
                    UpdateName();
                    break;
                case "8":
                    UpdateEmail();
                    break;
                case "9":
                    UpdatePassword();
                    break;
                case "10":
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

    #endregion

    #region PRIVATE_METHODS

    private void ViewPersonalDetails()
    {
        Console.WriteLine("Here are your personal details :");
        Console.WriteLine(_user);
    }

    private void ViewAppointments()
    {
        List<PatientViewAppointmentsResponse> responses = null!;
        try { responses = _userAppointmentQueryService.ObtainAppointmentDatesAndDoctorNameByPatientId(_user.GetId()); }
        catch (ItemsDoNotExist)
        {
            Console.WriteLine("You have no appointments!\n");
            return;
        }

        Console.WriteLine("Your appointments are :\n");
        for (int i = 0; i < responses.Count; i++)
        {
            PatientViewAppointmentsResponse response = responses[i];
            string message = $"{i + 1}. ";

            if (response.StartDate.DayOfYear == response.EndDate.DayOfYear)
                message += response.StartDate.ToString(Constants.STANDARD_DATE_FORMAT) + " - " + response.EndDate.ToString(Constants.STANDARD_DATE_DAYTIME_ONLY);
            else message += response.StartDate.ToString(Constants.STANDARD_DATE_FORMAT) + " - " + response.EndDate.ToString(Constants.STANDARD_DATE_FORMAT);
            message += "\nWith dr. " + response.DoctorName + "\n";

            Console.WriteLine(message);
        }
    }

    private void ViewDoctors()
    {
        PatientViewAllDoctorsResponse response = null!;
        try { response = _userQueryService.ObtainAllDoctorDetails(); }
        catch (ItemsDoNotExist ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.WriteLine("Available doctors :");
        string message = "";
        foreach (User doctor in response.Doctors)
        {
            message += doctor.GetName() + "\n";
            message += doctor.GetEmail() + "\n";
            message += doctor.GetPhone() + "\n";
            message += "\n";
        }
        Console.WriteLine(message);
    }

    private void CheckDoctorFreeTime()
    {
        Console.WriteLine("Enter the doctor's name :");
        String name = Console.ReadLine()!;
        User doctor = null!;
        bool parsed = false;
        while (!parsed)
        {
            try
            {
                doctor = _userQueryService.FindDoctorByName(name);
                parsed = true;
            }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("\nNo doctors with that name exist!\nPlease try again :");
                name = Console.ReadLine()!;
            }
            catch (MultipleDoctorsWithThatName)
            {
                Console.WriteLine("\nThere are multiple doctors with that name!");
                Console.WriteLine("Please enter the doctor's email to be more specific :");
                String email = Console.ReadLine()!;
                while (!parsed)
                {
                    try
                    {
                        doctor = _userQueryService.FindByEmail(email);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist ex)
                    {

                        Console.WriteLine("\n" + ex.Message);
                    }

                    if (doctor != null && doctor.GetType() != UserType.DOCTOR)
                    {
                        Console.WriteLine("\n" + Constants.USER_NOT_DOCTOR);
                    }

                    if (!parsed)
                    {
                        Console.WriteLine("Please try again :");
                        email = Console.ReadLine()!;
                    }
                }
            }
        }

        Console.WriteLine("\nChoose the day you want to check the doctor's availability (Example : 21.03.2022)");
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
        date += new TimeSpan(8, 0, 0);

        parsed = false;
        TimeSpan duration = new TimeSpan(0, 0, 0);
        Console.WriteLine("\nAppointment must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
        Console.WriteLine("Enter how long you want the appointment to be in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
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

        PatientGetDoctorFreeTimeResponse response = _userAppointmentQueryService.GetDoctorFreeTime(doctor.GetId(), date, duration);
        if (response.TimeIntervals.Count == 0)
        {
            Console.WriteLine("\nThis doctor has a full schedule in this day.");
            return;
        }
        Console.WriteLine("\nThis doctor is free in these time intervals :");
        foreach (TimeInterval interval in response.TimeIntervals)
        {
            Console.Write(interval.StartTime.ToString(Constants.STANDARD_DATE_DAYTIME_ONLY) + " - " + interval.EndTime.ToString(Constants.STANDARD_DATE_DAYTIME_ONLY) + "\n");
        }
    }

    private void MakeAppointment()
    {
        Console.WriteLine("Enter the doctor's name :");
        String name = Console.ReadLine()!;
        User doctor = null!;
        bool parsed = false;
        while (!parsed)
        {
            try
            {
                doctor = _userQueryService.FindDoctorByName(name);
                parsed = true;
            }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("\nNo doctors with that name exist!\nPlease try again :");
                name = Console.ReadLine()!;
            }
            catch (MultipleDoctorsWithThatName)
            {
                Console.WriteLine("\nThere are multiple doctors with that name!");
                Console.WriteLine("Please enter the doctor's email to be more specific :");
                String email = Console.ReadLine()!;
                while (!parsed)
                {
                    try
                    {
                        doctor = _userQueryService.FindByEmail(email);
                        parsed = true;
                    }
                    catch (ItemDoesNotExist ex)
                    {
                        Console.WriteLine("\n" + ex.Message);
                    }

                    if (doctor != null && doctor.GetType() != UserType.DOCTOR)
                    {
                        Console.WriteLine("\n" + Constants.USER_NOT_DOCTOR);
                    }

                    if (!parsed)
                    {
                        Console.WriteLine("Please try again :");
                        email = Console.ReadLine()!;
                    }
                }
            }
        }

        Console.WriteLine("\nChoose the day for the appointment (Example : 21.03.2022)");
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
        DateTime maxDate = date + new TimeSpan(16, 0, 0);

        Console.WriteLine("\nEnter the hour and minute of the appointment (Example : 12:30) : ");
        Console.WriteLine("Must be between 08:00 - 16:00!");
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
        date += new TimeSpan(daytime.Hour, daytime.Minute, 0);

        parsed = false;
        TimeSpan duration = new TimeSpan(0, 0, 0);
        Console.WriteLine("\nAppointment must be minimum 30 minutes and maximum 120 minutes! (2 hours)");
        Console.WriteLine("Enter how long you want the appointment to be in minutes and in multiples of 5.\nExample: 60 => 1 hour, 90 => 1 hour and 30 minutes");
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
        if(date + duration > maxDate)
        {
            Console.WriteLine("Appointment would exceed the doctor's working time!");
            return;
        }

        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(0)
            .StartDate(date)
            .EndDate(date + duration);

        try
        {
            UserAppointment check = _userAppointmentQueryService.FindByDoctorIdAndAppointment(doctor.GetId(), appointment);
            Console.WriteLine("\nDoctor is occupied in that date.");
            return;
        }
        catch (ItemDoesNotExist) { }

        UserAppointment patientAppointment = null!;
        try
        {
            patientAppointment = _userAppointmentQueryService.FindByPatientIdAndDates(_user.GetId(), appointment.GetStartDate(), appointment.GetEndDate());
            Console.WriteLine("You already have an appointment in that date!");
            return;
        }
        catch (ItemDoesNotExist) { }
        try
        {
            patientAppointment = _userAppointmentQueryService.FindByDoctorIdAndDates(doctor.GetId(), appointment.GetStartDate(), appointment.GetEndDate());
            Console.WriteLine("\nThat doctor already has an appointment scheduled at that date and time.");
            return;
        }
        catch (ItemDoesNotExist) { }

        _appointmentCommandService.Add(appointment);
        int appointmentId = _appointmentQueryService.GetLastId();
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(0)
            .PatientId(_user.GetId())
            .DoctorId(doctor.GetId())
            .AppointmentId(appointmentId);
        _userAppointmentCommandService.Add(userAppointment);
        Console.WriteLine("\nSuccessfully scheduled the appointment!\nDoctor will be notified.");
    }

    private void CancelAppointment()
    {
        Console.WriteLine("Enter the date of the appointment you want to cancel (Example : 21.03.2022)");
        Console.WriteLine("Please enter a date starting from today :");
        String dateString = Console.ReadLine()!;
        DateTime date = DateTime.MinValue;
        bool parsed = false;
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

        UserAppointment patientAppointment = null!;
        try
        {
            patientAppointment = _userAppointmentQueryService.FindByPatientIdAndDates(_user.GetId(), start, start + duration);
        }
        catch (ItemDoesNotExist)
        {
            Console.WriteLine("\nYou have no appointments scheduled at that date and time.");
            return;
        }

        _userAppointmentCommandService.Delete(patientAppointment);
        _appointmentCommandService.DeleteById(patientAppointment.GetAppointmentId());
        Console.WriteLine("Successfully canceled your appointment!");
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

    private void DisplayOptions()
    {
        string options = "";
        options += "1. View personal details\n";
        options += "2. View appointments\n";
        options += "3. View all doctors\n";
        options += "4. Check a doctor's availability in a certain day\n";
        options += "5. Make an appointment\n";
        options += "6. Cancel an appointment\n";
        options += "7. Update your name\n";
        options += "8. Update your email\n";
        options += "9. Update your password\n";
        options += "10. Updated your phone number\n";
        options += "Anything else to log out";
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

        return name.All(character =>
        {
            bool found = false;
            if (!char.IsLetter(character) || (character == ' ' && !found))
            {
                return false;
            }
            if (char.IsLetter(character))
            {
                found = true;
            }
            if (character == ' ')
            {
                found = false;
            }
            return true;
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