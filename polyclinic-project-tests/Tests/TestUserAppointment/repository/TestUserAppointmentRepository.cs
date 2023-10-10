using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.model.interfaces;
using polyclinic_project.user_appointment.repository;
using polyclinic_project.user_appointment.repository.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;

namespace polyclinic_project_tests.Tests.TestUserAppointment.repository;

[Collection("Tests")]
public class TestUserAppointmentRepository
{
    private IUserAppointmentRepository _userAppointmentRepository = new UserAppointmentRepository(TestConnectionString.GetConnection("UserAppointmentRepository"));
    private IAppointmentRepository _appointmentRepository = new AppointmentRepository(TestConnectionString.GetConnection("AppointmentRepository"));
    private IUserRepository _userRepository = new UserRepository(TestConnectionString.GetConnection("UserRepository"));
    
    [Fact]
    public void TestAdd_AddsUserAppointment()
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
        
        // Act
        _userAppointmentRepository.Add(userAppointment);
        
        // Assert
        Assert.Contains(userAppointment, _userAppointmentRepository.GetList());
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
    }
    
    [Fact]
    public void TestDelete_DeletesUserAppointment()
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
        _userAppointmentRepository.Delete(userAppointment.GetId());
        
        // Assert
        Assert.DoesNotContain(userAppointment, _userAppointmentRepository.GetList());
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
    }
    
    [Fact]
    public void TestUpdate_UpdatesUserAppointment()
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
        Appointment another = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        UserAppointment update = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(2);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _appointmentRepository.Add(another);
        _userAppointmentRepository.Add(userAppointment);
        
        // Act
        _userAppointmentRepository.Update(update);
        
        // Assert
        Assert.Contains(update, _userAppointmentRepository.GetList());
        Assert.Equal(update, _userAppointmentRepository.FindById(userAppointment.GetId())[0]);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
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
        UserAppointment found = _userAppointmentRepository.FindById(userAppointment.GetId())[0];
        
        // Assert
        Assert.Equal(userAppointment, found);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
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
        Appointment anotherAppointment = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        UserAppointment anotherUserAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(2)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(2);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _appointmentRepository.Add(anotherAppointment);
        _userAppointmentRepository.Add(userAppointment);
        _userAppointmentRepository.Add(anotherUserAppointment);
        List<UserAppointment> list = new List<UserAppointment> { userAppointment, anotherUserAppointment };
        
        // Act
        List<UserAppointment> check = _userAppointmentRepository.FindByPatientId(1);
        
        // Assert
        Assert.Equal(list, check);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
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
        Appointment anotherAppointment = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        UserAppointment anotherUserAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(2)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(2);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _appointmentRepository.Add(anotherAppointment);
        _userAppointmentRepository.Add(userAppointment);
        _userAppointmentRepository.Add(anotherUserAppointment);
        List<UserAppointment> list = new List<UserAppointment> { userAppointment, anotherUserAppointment };
        
        // Act
        List<UserAppointment> check = _userAppointmentRepository.FindByDoctorId(2);
        
        // Assert
        Assert.Equal(list, check);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
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
        UserAppointment found = _userAppointmentRepository.FindByAppointmentId(userAppointment.GetAppointmentId())[0];
        
        // Assert
        Assert.Equal(userAppointment, found);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
    }
    
    [Fact]
    public void TestGetList_ReturnsUserAppointmentList()
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
        Appointment anotherAppointment = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        UserAppointment anotherUserAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(2)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(2);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _appointmentRepository.Add(anotherAppointment);
        _userAppointmentRepository.Add(userAppointment);
        _userAppointmentRepository.Add(anotherUserAppointment);
        List<UserAppointment> list = new List<UserAppointment> { userAppointment, anotherUserAppointment };
        
        // Act
        List<UserAppointment> check = _userAppointmentRepository.GetList();
        
        // Assert
        Assert.Equal(list, check);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
    }
    
    [Fact]
    public void TestGetCount_ReturnsUserAppointmentCount()
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
        Appointment anotherAppointment = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        UserAppointment anotherUserAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(2)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(2);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _appointmentRepository.Add(anotherAppointment);
        _userAppointmentRepository.Add(userAppointment);
        _userAppointmentRepository.Add(anotherUserAppointment);
        List<UserAppointment> list = new List<UserAppointment> { userAppointment, anotherUserAppointment };
        
        // Act
        int count = _userAppointmentRepository.GetCount();
        
        // Assert
        Assert.Equal(list.Count, count);
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
    }
    
    [Fact]
    public void TestClear_ClearsList()
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
        Appointment anotherAppointment = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        UserAppointment userAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        UserAppointment anotherUserAppointment = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(2)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(2);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _appointmentRepository.Add(anotherAppointment);
        _userAppointmentRepository.Add(userAppointment);
        _userAppointmentRepository.Add(anotherUserAppointment);
        
        // Act
        _userAppointmentRepository.Clear();
        
        // Assert
        Assert.Empty(_userAppointmentRepository.GetList());
        
        // Cleaning up
        _userAppointmentRepository.Clear();
        _userRepository.Clear();
        _appointmentRepository.Clear();
    }
}