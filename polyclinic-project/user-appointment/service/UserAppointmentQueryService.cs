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
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.appointment.model;
using polyclinic_project.user.dtos;
using MySqlX.XDevAPI.Common;
using polyclinic_project.user.exceptions;
using polyclinic_project.appointment.model.interfaces;

namespace polyclinic_project.user_appointment.service;

public class UserAppointmentQueryService : IUserAppointmentQueryService
{
    private IUserRepository _userRepository;
    private IAppointmentRepository _appointmentRepository;
    private IUserAppointmentRepository _userAppointmentRepository;

    #region CONSTRUCTORS

    public UserAppointmentQueryService()
    {
        _userRepository = UserRepositorySingleton.Instance;
        _appointmentRepository = AppointmentRepositorySingleton.Instance;
        _userAppointmentRepository = UserAppointmentRepositorySingleton.Instance;
    }

    public UserAppointmentQueryService(IUserRepository userRepository, IAppointmentRepository appointmentRepository, IUserAppointmentRepository userAppointmentRepository)
    {
        _userRepository = userRepository;
        _appointmentRepository = appointmentRepository;
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
            throw new ItemsDoNotExist(Constants.PATIENT_HAS_NO_APPOINTMENTS);
        return result;
    }

    public int GetCount()
    {
        return _userAppointmentRepository.GetCount();
    }

    public bool IsDoctorAvailable(string name, DateTime startDate, DateTime endDate)
    {
        List<User> doctors = _userRepository.FindDoctorByName(name);
        if (doctors.Count == 0)
            throw new ItemsDoNotExist(Constants.NO_DOCTORS_WITH_THAT_NAME);
        if (doctors.Count > 1)
            throw new MultipleDoctorsWithThatName(Constants.MULTIPLE_DOCTORS_WITH_THAT_NAME);

        Appointment check = IAppointmentBuilder.BuildAppointment()
            .Id(-1)
            .StartDate(startDate)
            .EndDate(endDate);
        List<UserAppointment> userAppointments = FindByDoctorId(doctors[0].GetId());
        foreach(UserAppointment userAppointment in userAppointments)
        {
            Appointment appointment = _appointmentRepository.FindById(userAppointment.GetAppointmentId())[0];
            if (appointment.Equals(check))
            {
                return false;
            }
        }
        return true;
    }

    public bool IsDoctorAvailableByEmail(string email, DateTime startDate, DateTime endDate)
    {
        List<User> users = _userRepository.FindByEmail(email);
        if (users.Count == 0)
            throw new ItemDoesNotExist(Constants.DOCTOR_DOES_NOT_EXIST);

        User doctor = users[0];
        if (doctor.GetType() != UserType.DOCTOR)
            throw new UserIsNotADoctor(Constants.USER_NOT_DOCTOR);

        Appointment check = IAppointmentBuilder.BuildAppointment()
            .Id(-1)
            .StartDate(startDate)
            .EndDate(endDate);
        List<UserAppointment> userAppointments = FindByDoctorId(doctor.GetId());
        foreach (UserAppointment userAppointment in userAppointments)
        {
            Appointment appointment = _appointmentRepository.FindById(userAppointment.GetAppointmentId())[0];
            if (appointment.Equals(check))
            {
                return false;
            }
        }
        return true;
    }

    public List<PatientViewAppointmentsResponse> ObtainAppointmentDatesAndDoctorNameByPatientId(int patientId)
    {
        List<UserAppointment> userAppointments;
        try { userAppointments = FindByPatientId(patientId); }
        catch (ItemsDoNotExist ex) { throw; }

        List<User> users = _userRepository.GetList();
        List<Appointment> appointments = _appointmentRepository.GetList();

        IEnumerable<PatientViewAppointmentsResponse> result = from userAppointment in userAppointments
                                                       join appointment in appointments on userAppointment.GetAppointmentId() equals appointment.GetId()
                                                       join user in users on userAppointment.GetDoctorId() equals user.GetId()
                                                       select new PatientViewAppointmentsResponse
                                                       {
                                                           StartDate = appointment.GetStartDate(),
                                                           EndDate = appointment.GetEndDate(),
                                                           DoctorName = user.GetName()
                                                       };
        return result.ToList();
    }

    #endregion
}