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
    public class ViewDoctor : IViewDoctor
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
        }

        #endregion

        #region PRIVATE_METHODS

        public void ViewPersonalDetails()
        {
            
        }
        
        public void ViewAppointments()
        {
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

        #endregion
    }
}