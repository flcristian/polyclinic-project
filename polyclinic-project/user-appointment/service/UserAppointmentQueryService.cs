using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.repository.interfaces;
using polyclinic_project.user_appointment.repository;
using polyclinic_project.user_appointment.service.interfaces;

namespace polyclinic_project.user_appointment.service;

public class UserAppointmentQueryService : IUserAppointmentQueryService
{
    private IUserAppointmentRepository _userAppointmentRepository;

    #region CONSTRUCTORS

    public UserAppointmentQueryService()
    {
        _userAppointmentRepository = UserAppointmentRepositorySingleton.Instance;
    }

    public UserAppointmentQueryService(IUserAppointmentRepository userAppointmentRepository)
    {
        _userAppointmentRepository = userAppointmentRepository;
    }

    public UserAppointment FindByEmail(string email)
    {
        throw new NotImplementedException();
    }

    public UserAppointment FindById(int id)
    {
        throw new NotImplementedException();
    }

    public UserAppointment FindByPhone(string phone)
    {
        throw new NotImplementedException();
    }

    public int GetCount()
    {
        throw new NotImplementedException();
    }
}