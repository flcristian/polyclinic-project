using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.user_appointment.service;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.view.interfaces;
using System.Text.RegularExpressions;

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
                        ViewUserDetails();
                        break;
                    case "5":
                        EditUserName();
                        break;
                    case "6":
                        EditUserEmail();
                        break;
                    case "7":
                        EditUserPhone();
                        break;
                    case "8":
                        AssignDoctor();
                        break;
                    case "9":
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
                        UpdateEmail();
                        break;
                    case "3":
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

        }

        private void ViewAllPatients()
        {

        }

        private void ViewAllDoctors()
        {

        }

        private void ViewUserDetails()
        {

        }

        private void EditUserName()
        {

        }

        private void EditUserEmail()
        {

        }

        private void EditUserPhone()
        {

        }

        private void AssignDoctor()
        {

        }

        private void DeleteUser()
        {

        }

        private void ViewAllAppointments()
        {

        }

        private void ChangeAppointmentPatient()
        {

        }

        private void ChangeAppointmentDoctor()
        {

        }

        private void ChangeAppointmentDates()
        {

        }

        private void CancelAppointment()
        {

        }

        private void ViewPersonalDetails()
        {
            Console.WriteLine("Here are your personal details :");
            Console.WriteLine(_user);
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
            options += "4. View user details\n";
            options += "5. Edit user name\n";
            options += "6. Edit user email\n";
            options += "7. Edit user phone\n";
            options += "8. Assign doctor type to user\n";
            options += "9. Delete user\n";
            options += "Anything else to exit the meu";
            Console.WriteLine(options);
        }

        private void DisplayAppointmentOptions()
        {
            string options = "";
            options += "1. View all appointments\n";
            options += "2. Change an appointment's patient\n";
            options += "3. Change an appointment's doctor\n";
            options += "4. Change an appointment's dates\n";
            options += "5. Cancel an appointment\n";
            options += "Anything else to exit the menu";
            Console.WriteLine(options);
        }

        private void DisplayPersonalOptions()
        {
            string options = "";
            options += "1. View personal details\n";
            options += "2. Update your email\n";
            options += "3. Update your phone\n";
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

        private bool IsValidEmailAddress(string email)
        {
            string pattern = @"^[a-zA-Z0-9._]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";

            return Regex.IsMatch(email, pattern);
        }

        private bool IsValidPhoneNumber(string phone)
        {
            return phone.All(character =>
            {
                return char.IsDigit(character) || character == '-' ||
                       (character == '+' && phone.IndexOf(character) == 0);
            });
        }


        #endregion
    }
}
