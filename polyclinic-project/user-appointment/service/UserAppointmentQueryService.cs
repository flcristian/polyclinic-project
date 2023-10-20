using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.system.models;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.repository;
using polyclinic_project.user_appointment.repository.interfaces;
using polyclinic_project.user_appointment.service.interfaces;

namespace polyclinic_project.user_appointment.service;

public class UserAppointmentQueryService : IUserAppointmentQueryService
{
    private IUserRepository _userRepository;
    private IAppointmentRepository _appointmentRepository;
    private IUserAppointmentRepository _userAppointmentRepository;
    private IAppointmentQueryService _appointmentQueryService;

    #region CONSTRUCTORS

    public UserAppointmentQueryService()
    {
        _userRepository = UserRepositorySingleton.Instance;
        _appointmentRepository = AppointmentRepositorySingleton.Instance;
        _userAppointmentRepository = UserAppointmentRepositorySingleton.Instance;
        _appointmentQueryService = AppointmentQueryServiceSingleton.Instance;
    }

    public UserAppointmentQueryService(IUserRepository userRepository, IAppointmentRepository appointmentRepository, IUserAppointmentRepository userAppointmentRepository)
    {
        _userRepository = userRepository;
        _appointmentRepository = appointmentRepository;
        _userAppointmentRepository = userAppointmentRepository;
        _appointmentQueryService = new AppointmentQueryService(appointmentRepository);
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

    public UserAppointment FindByDoctorIdAndAppointment(int doctorId, Appointment appointment)
    {
        List<UserAppointment> userAppointments = null!;
        try
        {
            userAppointments = FindByDoctorId(doctorId);
        }
        catch (ItemsDoNotExist)
        {
            throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
        }

        List<Appointment> appointments = new List<Appointment>();
        userAppointments.ForEach(userAppointment =>
        {
            appointments.Add(_appointmentQueryService.FindById(userAppointment.GetAppointmentId()));
        });
        foreach(Appointment current in appointments)
        {
            if (current.Equals(appointment))
            {
                return userAppointments[appointments.IndexOf(appointment)];
            }
        }
        throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
    }

    public int GetCount()
    {
        return _userAppointmentRepository.GetCount();
    }

    public PatientGetDoctorFreeTimeResponse GetDoctorFreeTime(int doctorId, DateTime date, TimeSpan duration)
    {
        PatientGetDoctorFreeTimeResponse response = new PatientGetDoctorFreeTimeResponse();
        List<UserAppointment> userAppointments = null!;
        try
        {
             userAppointments = FindByDoctorId(doctorId);
        }
        catch (ItemsDoNotExist)
        {
            response.TimeIntervals.Add(new TimeInterval(date, date + new TimeSpan(8, 0, 0)));
            return response;
        }

        List<Appointment> appointments = new List<Appointment>();
        foreach (UserAppointment userAppointment in userAppointments)
        {
            appointments.Add(_appointmentQueryService.FindById(userAppointment.GetAppointmentId()));
        }
        appointments.RemoveAll((appointment) =>
        {
            if (appointment.GetStartDate().Year != date.Year || appointment.GetStartDate().DayOfYear != date.DayOfYear)
                return true;
            return false;
        });
        appointments.Sort((a, b) =>
        {
            if (a.GetStartDate() > b.GetStartDate())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        });

        DateTime prev = date;
        DateTime next = date;
        foreach (Appointment appointment in appointments)
        {
            next = appointment.GetStartDate();
            TimeSpan check = next - prev;
            if (check >= duration)
            {
                response.TimeIntervals.Add(new TimeInterval(prev, next));
            }
            prev = appointment.GetEndDate();
        }
        if (next < date + new TimeSpan(8, 0, 0))
        {
            response.TimeIntervals.Add(new TimeInterval(prev, date + new TimeSpan(8, 0, 0)));
        }

        for(int i = 0; i < response.TimeIntervals.Count; i++)
        {
            TimeInterval interval = response.TimeIntervals[i];

            if (interval.StartTime == interval.EndTime)
            {
                response.TimeIntervals.Remove(interval);
                i--;
            }
        }

        return response;
    }

    public List<PatientViewAppointmentsResponse> ObtainAppointmentDatesAndDoctorNameByPatientId(int patientId)
    {
        List<UserAppointment> userAppointments = FindByPatientId(patientId);

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
        List<PatientViewAppointmentsResponse> resultList = result.ToList();
        resultList.Sort((a, b) =>
        {
            if(a.StartDate > b.StartDate)
            {
                return 1;
            }

            return 0;
        });
        return resultList;
    }

    public List<DoctorViewAppointmentsResponse> ObtainAppointmentDetailsByDoctorId(int doctorId)
    {
        List<UserAppointment> userAppointments = FindByDoctorId(doctorId);
        List<User> users = _userRepository.GetList();
        List<Appointment> appointments = _appointmentRepository.GetList();

        IEnumerable<DoctorViewAppointmentsResponse> result = from userAppointment in userAppointments
            join appointment in appointments on userAppointment.GetAppointmentId() equals appointment.GetId()
            join user in users on userAppointment.GetPatientId() equals user.GetId()
            select new DoctorViewAppointmentsResponse
            {
                StartDate = appointment.GetStartDate(),
                EndDate = appointment.GetEndDate(),
                PatientName = user.GetName(),
                PatientEmail = user.GetEmail(),
                PatientPhone = user.GetPhone()
            };
        List<DoctorViewAppointmentsResponse> resultList = result.ToList();
        resultList.Sort((a, b) =>
        {
            if(a.StartDate > b.StartDate)
            {
                return 1;
            }

            return 0;
        });
        return resultList;
    }

    public UserAppointment FindByPatientIdAndDates(int patientId, DateTime startDate, DateTime endDate)
    {
        List<UserAppointment> userAppointments = null!;
        try { userAppointments = FindByPatientId(patientId); }
        catch (ItemsDoNotExist) { throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST); }

        List<Appointment> appointments = new List<Appointment>();
        foreach(UserAppointment userAppointment in userAppointments)
        {
            appointments.Add(_appointmentQueryService.FindById(userAppointment.GetAppointmentId()));
        }

        Appointment check = IAppointmentBuilder.BuildAppointment()
            .Id(-1)
            .StartDate(startDate)
            .EndDate(endDate);

        foreach(Appointment appointment in appointments)
        {
            if (check.Equals(appointment))
            {
                return userAppointments[appointments.IndexOf(appointment)];
            }
        }

        throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
    }

    public UserAppointment FindByDoctorIdAndDates(int doctorId, DateTime startDate, DateTime endDate)
    {
        List<UserAppointment> userAppointments = null!;
        try { userAppointments = FindByDoctorId(doctorId); }
        catch (ItemsDoNotExist) { throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST); }

        List<Appointment> appointments = new List<Appointment>();
        foreach (UserAppointment userAppointment in userAppointments)
        {
            appointments.Add(_appointmentQueryService.FindById(userAppointment.GetAppointmentId()));
        }

        Appointment check = IAppointmentBuilder.BuildAppointment()
            .Id(-1)
            .StartDate(startDate)
            .EndDate(endDate);

        foreach (Appointment appointment in appointments)
        {
            if (check.Equals(appointment))
            {
                return userAppointments[appointments.IndexOf(appointment)];
            }
        }

        throw new ItemDoesNotExist(Constants.USER_APPOINTMENT_DOES_NOT_EXIST);
    }

    public List<AdminViewAllAppointmentsResponse> ObtainAllAppointmentDetails()
    {
        List<UserAppointment> userAppointments = _userAppointmentRepository.GetList();
        if(userAppointments.Count == 0)
        {
            throw new ItemsDoNotExist(Constants.USER_APPOINTMENTS_DO_NOT_EXIST);
        }
        List<User> users = _userRepository.GetList();
        List<Appointment> appointments = _appointmentRepository.GetList();

        IEnumerable<AdminViewAllAppointmentsResponse> result = from userAppointment in userAppointments
                                                                 join patient in users on userAppointment.GetPatientId() equals patient.GetId()
                                                                 join doctor in users on userAppointment.GetDoctorId() equals doctor.GetId()
                                                                 join appointment in appointments on userAppointment.GetAppointmentId() equals appointment.GetId()
                                                                 select new AdminViewAllAppointmentsResponse
                                                                 {
                                                                     Patient = patient,
                                                                     Doctor = doctor,
                                                                     Appointment = appointment
                                                                 };
        List<AdminViewAllAppointmentsResponse> response = result.ToList();
        response.Sort((a, b) =>
        {
            if (a.Appointment.GetStartDate() > b.Appointment.GetEndDate())
            {
                return 1;
            }

            return 0;
        });
        return response;
    }

    public Appointment FindAppointmentByUserAppointmentId(int id)
    {
        UserAppointment userAppointment = FindById(id);
        Appointment result = _appointmentQueryService.FindById(userAppointment.GetAppointmentId());
        return result;
    }
                    
    #endregion
}