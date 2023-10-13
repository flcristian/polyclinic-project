 using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.model.interfaces;
using polyclinic_project.user_appointment.repository;
using polyclinic_project.user_appointment.repository.interfaces;
using polyclinic_project.user_appointment.service;
using polyclinic_project.user_appointment.service.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.system.constants;
using System.Globalization;
using polyclinic_project.user.exceptions;
using polyclinic_project.system.models;

namespace polyclinic_project_tests.Tests.TestUserAppointment.service;

[Collection("Tests")]
public class TestUserAppointmentQueryService
{
    private static string _connection = TestConnectionString.GetConnection("UserAppointmentQueryService");
    private static IUserRepository _userRepository =
        new UserRepository(_connection);
    private static IAppointmentRepository _appointmentRepository =
        new AppointmentRepository(_connection);
    private static IUserAppointmentRepository _userAppointmentRepository =
        new UserAppointmentRepository(_connection);
    private IUserAppointmentQueryService _service = new UserAppointmentQueryService(_userRepository, _appointmentRepository, _userAppointmentRepository);

    [Fact]
    public void TestFindById_UserAppointmentDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);

        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindById(userAppointment.GetId()));

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindById_ReturnsUserAppointment()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);

        // Act
        UserAppointment found = _service.FindById(userAppointment.GetId());

        // Assert
        Assert.Equal(userAppointment, found);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindByPatientId_PatientNotScheduledForAnyAppointments_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.FindByPatientId(userAppointment.GetPatientId()));

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindByPatientId_ReturnsUserAppointmentList()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);

        // Act
        List<UserAppointment> found = _service.FindByPatientId(userAppointment.GetPatientId());

        // Assert
        Assert.Equal(new List<UserAppointment> { userAppointment }, found);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindByDoctorId_DoctorNotAssignedToAnyAppointments_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.FindByDoctorId(userAppointment.GetDoctorId()));

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindByDoctorId_ReturnsUserAppointmentList()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);

        // Act
        List<UserAppointment> found = _service.FindByDoctorId(userAppointment.GetDoctorId());

        // Assert
        Assert.Equal(new List<UserAppointment> { userAppointment }, found);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindByAppointmentId_AppointmentDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);

        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindByAppointmentId(userAppointment.GetAppointmentId()));

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestFindByAppointmentId_ReturnsUserAppointment()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);

        // Act
        UserAppointment found = _service.FindByAppointmentId(userAppointment.GetAppointmentId());

        // Assert
        Assert.Equal(userAppointment, found);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestGetCount_ReturnsCount()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);
        
        // Act
        int count = _service.GetCount();
        
        // Assert
        Assert.Equal(1, count);
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestObtainAppointmentDatesAndDoctorNameByPatientId_PatientHasNoAppointments_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.ObtainAppointmentDatesAndDoctorNameByPatientId(patient.GetId()));

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestObtainAppointmentDatesAndDoctorNameByPatientId_PatientHasAppointments_ReturnsCorrectListOfPatientViewAppointmentsResponseDTO()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);

        // Act
        List<PatientViewAppointmentsResponse> check = _service.ObtainAppointmentDatesAndDoctorNameByPatientId(patient.GetId());

        // Assert
        PatientViewAppointmentsResponse dto = new PatientViewAppointmentsResponse();
        dto.StartDate = appointment.GetStartDate();
        dto.EndDate = appointment.GetEndDate();
        dto.DoctorName = doctor.GetName();
        Assert.Equal(new List<PatientViewAppointmentsResponse> { dto }, check);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestGetDoctorFreeTime_DoctorHasNoAppointments_ReturnsEntireDayAsTimeIntervalInDTO()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(doctor);

        // Act
        DateTime start = DateTime.ParseExact("06.10.2023", Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture) + new TimeSpan(8, 0, 0); ;
        PatientGetDoctorFreeTimeResponse response = _service.GetDoctorFreeTime(doctor.GetId(), start, new TimeSpan(0, 30, 0));

        // Assert
        List<TimeInterval> list = new List<TimeInterval>{
            new TimeInterval(start, start + new TimeSpan(8, 0, 0)),
        };
        Assert.Equal(list, response.TimeIntervals);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestGetDoctorFreeTime_ReturnsResponseDTOWithCorrectTimeIntervals()
    {
        // Arrange
        User patient = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User doctor = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+98127633909")
            .Type(UserType.DOCTOR);
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);

        // Act
        DateTime start = DateTime.ParseExact("06.10.2023", Constants.STANDARD_DATE_CALENDAR_DATE_ONLY, CultureInfo.InvariantCulture) + new TimeSpan(8, 0, 0); ;
        PatientGetDoctorFreeTimeResponse response = _service.GetDoctorFreeTime(doctor.GetId(), start, new TimeSpan(0, 30, 0));

        // Assert
        List<TimeInterval> list = new List<TimeInterval>{
            new TimeInterval(start, start + new TimeSpan(4, 0, 0)),
            new TimeInterval(start + new TimeSpan(5, 0, 0), start +  new TimeSpan(8, 0, 0))
        };
        Assert.Equal(list, response.TimeIntervals);

        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
}