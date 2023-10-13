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
        List<User> patient = _userRepository.FindById(userAppointment.GetPatientId());
        List<User> doctor = _userRepository.FindById(userAppointment.GetDoctorId());
        List<Appointment> appointment = _appointmentRepository.FindById(userAppointment.GetAppointmentId());
        if (patient.Count == 0)
            throw new ItemDoesNotExist(Constants.PATIENT_DOES_NOT_EXIST);
        if (doctor.Count == 0)
            throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);
        if (appointment.Count == 0)
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        if (doctor[0].GetType() != UserType.DOCTOR)
            throw new UserIsNotADoctor(Constants.USER_NOT_DOCTOR);

        _userAppointmentRepository.Add(userAppointment);
    }

    public void ClearList()
    {
        _userAppointmentRepository.Clear();
    }

    public void Delete(UserAppointment userAppointment)
    {
        List<UserAppointment> check = _userAppointmentRepository.FindById(userAppointment.GetId());
        if (check.Count == 0)
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        _userAppointmentRepository.Delete(userAppointment.GetId());
    }

    public void DeleteById(int id)
    {
        List<UserAppointment> check = _userAppointmentRepository.FindById(id);
        if (check.Count == 0)
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        _userAppointmentRepository.Delete(id);
    }

    public void Update(UserAppointment userAppointment)
    {
        List<UserAppointment> check = _userAppointmentRepository.FindById(userAppointment.GetId());
        if (check.Count == 0)
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        if (check[0].Equals(userAppointment))
            throw new ItemNotModified(Constants.USER_APPOINTMENT_NOT_MODIFIED);
        _userAppointmentRepository.Update(userAppointment);
    }

    #endregion
}