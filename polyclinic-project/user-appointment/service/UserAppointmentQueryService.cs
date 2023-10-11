﻿using polyclinic_project.appointment.repository.interfaces;
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