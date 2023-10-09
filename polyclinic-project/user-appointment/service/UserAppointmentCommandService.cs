using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.repository;
using polyclinic_project.user_appointment.repository.interfaces;
using polyclinic_project.user_appointment.service.interfaces;

namespace polyclinic_project.user_appointment.service;

public class UserAppointmentCommandService : IUserAppointmentCommandService
{
    private IUserRepository _userRepository;
    private IAppointmentRepository _appointmentRepository;
    private IUserAppointmentRepository _userAppointmentRepository;

    #region CONSTRUCTORS

    public UserAppointmentCommandService()
    {
        _userRepository = UserRepositorySingleton.Instance;
        _appointmentRepository = AppointmentRepositorySingleton.Instance;
        _userAppointmentRepository = UserAppointmentRepositorySingleton.Instance;
    }

    public UserAppointmentCommandService(IUserRepository userRepository, IAppointmentRepository appointmentRepository, IUserAppointmentRepository userAppointmentRepository)
    {
        _userRepository = userRepository;
        _appointmentRepository = appointmentRepository;
        _userAppointmentRepository = userAppointmentRepository;
    }

    #endregion

    #region PUBLIC_METHODS

    public void Add(UserAppointment userAppointment)
    {
        User pacient = _userRepository.FindById(userAppointment.GetPacientId());
        User Doctor = _userRepository.FindById(userAppointment.GetDoctorId());
        Appointment appointment = _appointmentRepository.FindById(userAppointment.GetAppointmentId());
        if (pacient == null)
            throw new ItemDoesNotExist("This pacient does not exist.");
        if (doctor == null)
            throw new ItemDoesNotExist("This doctor does not exist.");
        if (pacient == null)
            throw new ItemDoesNotExist("This pacient does not exist.");
    }

    public void ClearList()
    {
        throw new NotImplementedException();
    }

    public void Delete(UserAppointment userAppointment)
    {
        throw new NotImplementedException();
    }

    public void DeleteById(int id)
    {
        throw new NotImplementedException();
    }

    public void Update(UserAppointment userAppointment)
    {
        throw new NotImplementedException();
    }

    #endregion
}