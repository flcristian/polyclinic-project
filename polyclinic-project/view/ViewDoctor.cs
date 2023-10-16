using polyclinic_project.appointment.model;
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

namespace polyclinic_project.view
{
    public class ViewDoctor : IView
    {
        private User _user;
        private IUserCommandService _userCommandService;
        private IUserQueryService _userQueryService;
        private IAppointmentCommandService _appointmentCommandService;
        private IUserAppointmentCommandService _userAppointmentCommandService;
        private IUserAppointmentQueryService _userAppointmentQueryService;

        #region CONSTRUCTORS

        public ViewDoctor(User user)
        {
            _user = user;
            _userCommandService = UserCommandServiceSingleton.Instance;
            _userQueryService = UserQueryServiceSingleton.Instance;
            _userAppointmentCommandService = UserAppointmentCommandServiceSingleton.Instance;
            _userAppointmentQueryService = UserAppointmentQueryServiceSingleton.Instance;
            _appointmentCommandService = AppointmentCommandServiceSingleton.Instance;
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
                        CompleteAppointment();
                        break;
                    case "4":
                        UpdateEmail();
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

        #endregion

        #region PRIVATE_METHODS

        public void ViewPersonalDetails()
        {
            Console.WriteLine("Here are your personal details :");
            Console.WriteLine(_user);
        }
        
        public void ViewAppointments()
        {
            List<DoctorViewAppointmentsResponse> responses = null!;
            try { responses = _userAppointmentQueryService.ObtainAppointmentDetailsByDoctorId(_user.GetId()); }
            catch (ItemsDoNotExist)
            {
                Console.WriteLine("You have no appointments!\n");
                return;
            }

            Console.WriteLine("Your appointments are :\n");
            for (int i = 0; i < responses.Count; i++)
            {
                DoctorViewAppointmentsResponse response = responses[i];
                string message = $"{i + 1}. ";

                if (response.StartDate.DayOfYear == response.EndDate.DayOfYear)
                    message += response.StartDate.ToString(Constants.STANDARD_DATE_FORMAT) + " - " + response.EndDate.ToString(Constants.STANDARD_DATE_DAYTIME_ONLY);
                else message += response.StartDate.ToString(Constants.STANDARD_DATE_FORMAT) + " - " + response.EndDate.ToString(Constants.STANDARD_DATE_FORMAT);
                message += $"\nPatient {response.PatientName}\n";
                message += $"Patient email : {response.PatientEmail}\n";
                message += $"Patient phone number : {response.PatientPhone}\n";

                Console.WriteLine(message);
            }
        }

        public void CompleteAppointment()
        {
        }

        public void UpdateEmail()
        {
        }

        public void UpdatePhone()
        {
        }
        
        // Menu Methods

        private void DisplayOptions()
        {
            string options = "";
            options += "1. View personal details\n";
            options += "2. View appointments\n";
            options += "3. Mark an appointment as complete\n";
            options += "4. Update your email\n";
            options += "5. Updated your phone number\n";
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
}