using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.exceptions;
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
        List<User> pacient = _userRepository.FindById(userAppointment.GetPacientId());
        List<User> doctor = _userRepository.FindById(userAppointment.GetDoctorId());
        List<Appointment> appointment = _appointmentRepository.FindById(userAppointment.GetAppointmentId());
        if (pacient.Count == 0)
            throw new ItemDoesNotExist(Constants.PACIENT_DOES_NOT_EXIST);
        if (doctor.Count == 0)
            throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
        if (appointment.Count == 0)
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        if (doctor[0].GetType() != UserType.DOCTOR)
            throw new UserIsNotADoctor(Constants.USER_NOT_DOCTOR);

        List<UserAppointment> check = _userAppointmentRepository.FindById(userAppointment.GetId());
        if (check.Count > 0)
            throw new ItemAlreadyExists(Constants.ID_ALREADY_USED);
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