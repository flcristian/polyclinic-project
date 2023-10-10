using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.repository.interfaces;
using polyclinic_project.user_appointment.repository;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.system.constants;
using polyclinic_project.user.model;

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

    #endregion

    #region PUBLIC_METHODS

    public UserAppointment FindByAppointmentId(int appointmentId)
    {
        List<UserAppointment> result = _userAppointmentRepository.FindByAppointmentId(appointmentId);
        if (result.Count == 0)
            throw new ItemDoesNotExist(Constants.APPOINTMENT_DOES_NOT_EXIST);
        return result[0];
    }

    public List<UserAppointment> FindByDoctorId(int doctorId)
    {
        List<UserAppointment> result = _userAppointmentRepository.FindByDoctorId(doctorId);
        if (result.Count == 0)
            throw new ItemsDoNotExist(Constants.DOCTOR_NOT_ASSIGNED);
        return result;
    }

    public UserAppointment FindById(int id)
    {
        List<UserAppointment> result = _userAppointmentRepository.FindById(id);
        if (result.Count == 0)
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        return result[0];
    }

    public List<UserAppointment> FindByPatientId(int patientId)
    {
        List<UserAppointment> result = _userAppointmentRepository.FindByPatientId(patientId);
        if (result.Count == 0)
            throw new ItemsDoNotExist(Constants.DOCTOR_NOT_ASSIGNED);
        return result;
    }

    public int GetCount()
    {
        return _userAppointmentRepository.GetCount();
    }

    #endregion
}