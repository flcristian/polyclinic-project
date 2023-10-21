using polyclinic_project.appointment.service;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.service;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.view.interfaces;
using System.Text.RegularExpressions;

namespace polyclinic_project.view
{
    public class ViewDoctor : IView
    {
        private User _user;
        private IUserCommandService _userCommandService;
        private IUserQueryService _userQueryService;
        private IUserAppointmentQueryService _userAppointmentQueryService;

        #region CONSTRUCTORS

        public ViewDoctor(User user)
        {
            _user = user;
            _userCommandService = UserCommandServiceSingleton.Instance;
            _userQueryService = UserQueryServiceSingleton.Instance;
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
                DisplayOptions();
                Console.WriteLine("Enter what you want to do :");

                string input = Console.ReadLine()!;
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
                        UpdateName();
                        break;
                    case "4":
                        UpdateEmail();
                        break;
                    case "5":
                        UpdatePassword();
                        break;
                    case "6":
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
            options += "3. Update your name\n";
            options += "4. Update your email\n";
            options += "5. Update your password\n";
            options += "6. Updated your phone number\n";
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