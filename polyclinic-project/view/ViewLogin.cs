using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.user.service;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.user_appointment.service;
using polyclinic_project.view.interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using polyclinic_project.user.model.interfaces;

namespace polyclinic_project.view
{
    public class ViewLogin : IView
    {
        private IUserCommandService _userCommandService;
        private IUserQueryService _userQueryService;

        #region CONSTRUCTORS

        public ViewLogin()
        {
            _userCommandService = UserCommandServiceSingleton.Instance;
            _userQueryService = UserQueryServiceSingleton.Instance;
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
                        Register();
                        break;
                    case "2":
                        Login();
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

        private void Register()
        {
            Console.Write("Enter your name : ");
            string name = Console.ReadLine()!;
            while (!IsValidName(name))
            {
                Console.WriteLine("\nPlease enter a valid name (only letters) :");
                name = Console.ReadLine()!;
            }

            Console.Write("\nEnter your email address : ");
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
                else
                {
                    try
                    {
                        _userQueryService.FindByEmail(email);
                        unique = false;
                        Console.WriteLine("\nThis email is already used.");
                        return;
                    }
                    catch (ItemDoesNotExist)
                    {
                        unique = true;
                    }
                }
            }

            Console.Write("\nEnter your password : ");
            string password = Console.ReadLine()!;
            while (!IsValidPassword(password))
            {
                Console.WriteLine("\nPlease enter a valid password (must be at least 4 characters) :");
                password = Console.ReadLine()!;
            }

            Console.Write("\nEnter your phone number : ");
            string phone = Console.ReadLine()!;
            unique = false;
            while (!unique)
            {
                if (!IsValidPhoneNumber(phone))
                {
                    Console.WriteLine("\nInvalid phone number.");
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

            User create = IUserBuilder.BuildUser()
                .Name(name)
                .Email(email)
                .Password(password)
                .Phone(phone)
                .Type(UserType.PATIENT);

            try
            {
                _userCommandService.Add(create);
                Console.WriteLine("Your account has been created successfully!");
                Console.WriteLine("You can now log in.");
            }
            catch (ItemAlreadyExists ex)
            {
                Console.WriteLine("Something went wrong, please contact an administrator.");
                Console.WriteLine(ex.Message);
            }
        }

        private void Login()
        {
            Console.Write("Enter your email address : ");
            string email = Console.ReadLine()!;
            while (!IsValidEmailAddress(email))
            {
                Console.WriteLine("\nPlease enter a valid email :");
                email = Console.ReadLine()!;
            }

            User user = null!;
            try
            {
                user = _userQueryService.FindByEmail(email);
            }
            catch (ItemDoesNotExist)
            {
                Console.WriteLine("\nNo user with that email address!");
                return;
            }

            Console.Write("\nEnter your password : ");
            string password = Console.ReadLine()!;
            if(!password.Equals(user.GetPassword()))
            {
                Console.WriteLine("\nWrong password!");
                return;
            }

            ChooseMenu(user);
        }

        private void ChooseMenu(User user)
        {
            IView view = null!;
            switch (user.GetType())
            {
                case UserType.PATIENT:
                    view = new ViewPatient(user);
                    break;
                case UserType.DOCTOR:
                    view = new ViewDoctor(user);
                    break;
                case UserType.ADMIN:
                    view = new ViewAdmin(user);
                    break;
                default:
                    break;
            }
            view.RunMenu();
        }

        // Menu Methods

        private void DisplayOptions()
        {
            string options = "";
            options += "1. Register a new account\n";
            options += "2. Log into your account\n";
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
