using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.user_appointment.service;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.view.interfaces;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.dtos;

namespace polyclinic_project.view;

public class ViewPatient : IViewPatient
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
                    ViewAvailableDoctors();
                    break;
                case "5":
                    MakeAppointment();
                    break;
                case "6":
                    CancelAppointment();
                    break;
                case "7":
                    UpdateEmail();
                    break;
                case "8":
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
        try { response = _userQueryService.ObtainAllDoctorNames(); }
        catch (ItemsDoNotExist ex)
        {
            Console.WriteLine(ex.Message);
            return;
        }

        Console.WriteLine("Available doctors :");
        string message = "";
        foreach(string doctor in response.Doctors)
        {
            message += doctor + "\n";
        }
        Console.WriteLine(message);
    }

    private void ViewAvailableDoctors()
    {
        throw new NotImplementedException();
    }

    private void MakeAppointment()
    {
        throw new NotImplementedException();
    }

    private void CancelAppointment()
    {
        throw new NotImplementedException();
    }

    private void UpdateEmail()
    {
        throw new NotImplementedException();
    }

    private void UpdatePhone()
    {
        throw new NotImplementedException();
    }

    // Menu Methods

    private void DisplayOptions()
    {
        string options = "";
        options += "1. View personal details\n";
        options += "2. View appointments\n";
        options += "3. View all doctors\n";
        options += "4. View available doctors in a certain day\n";
        options += "5. Make an appointment\n";
        options += "6. Cancel an appointment\n";
        options += "7. Update your email\n";
        options += "8. Updated your phone number\n";
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
    
    #endregion
}