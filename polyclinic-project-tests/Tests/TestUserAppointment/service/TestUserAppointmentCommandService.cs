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

namespace polyclinic_project_tests.Tests.TestUserAppointment.service;

[Collection("Tests")]
public class TestUserAppointmentCommandService
{
    private static string _connection = TestConnectionString.GetConnection("UserAppointmentCommandService");
    private static IUserRepository _userRepository =
        new UserRepository(_connection);
    private static IAppointmentRepository _appointmentRepository = 
        new AppointmentRepository(_connection);
    private static IUserAppointmentRepository _userAppointmentRepository =
        new UserAppointmentRepository(_connection);
    private IUserAppointmentCommandService _service = new UserAppointmentCommandService(_userRepository, _appointmentRepository, _userAppointmentRepository);

    [Fact]
    public void TestAdd_PatientDoesNotExist_ThrowsItemDoesNotExistException_DoesNotAddUserAppointment()
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
        _appointmentRepository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Add(userAppointment));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestAdd_DoctorDoesNotExist_ThrowsItemDoesNotExistException_DoesNotAddUserAppointment()
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
        _appointmentRepository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Add(userAppointment));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestAdd_AppointmentDoesNotExist_ThrowsItemDoesNotExistException_DoesNotAddUserAppointment()
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
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Add(userAppointment));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestAdd_UserAppointmentAlreadyExists_ThrowsItemAlreadyExistsException_DoesNotAddUserAppointment()
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
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(userAppointment));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
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
        _service.Add(userAppointment);
        
        // Assert
        Assert.Contains(userAppointment, _userAppointmentRepository.GetList());
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestClearList_ClearsList()
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
        _service.ClearList();
        
        // Assert
        Assert.Empty(_userAppointmentRepository.GetList());
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }

    [Fact]
    public void TestDelete_UserAppointmentDoesNotExist_ThrowsItemDoesNotExistException()
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
        Assert.Throws<ItemDoesNotExist>(() => _service.Delete(userAppointment));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestDelete_UserAppointmentExists_DeletesUserAppointment()
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
        _service.Delete(userAppointment);
        
        // Assert
        Assert.DoesNotContain(userAppointment, _userAppointmentRepository.GetList());
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestDeleteById_UserAppointmentDoesNotExist_ThrowsItemDoesNotExistException()
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
        Assert.Throws<ItemDoesNotExist>(() => _service.DeleteById(userAppointment.GetId()));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestDeleteById_UserAppointmentExists_DeletesUserAppointment()
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
        _service.DeleteById(userAppointment.GetId());
        
        // Assert
        Assert.DoesNotContain(userAppointment, _userAppointmentRepository.GetList());
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestUpdate_UserAppointmentDoesNotExist_ThrowsItemDoesNotExistException()
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
        User anotherDoctor = IUserBuilder.BuildUser()
            .Id(3)
            .Name("George")
            .Email("george@email.com")
            .Phone("+2717811109")
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
        UserAppointment update = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Update(update));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestUpdate_UserNotModified_ThrowsItemNotModifiedException()
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
        User anotherDoctor = IUserBuilder.BuildUser()
            .Id(3)
            .Name("George")
            .Email("george@email.com")
            .Phone("+2717811109")
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
        UserAppointment update = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(2)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);
        
        // Assert
        Assert.Throws<ItemNotModified>(() => _service.Update(update));
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
    
    [Fact]
    public void TestUpdate_UpdatesUser()
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
        User anotherDoctor = IUserBuilder.BuildUser()
            .Id(3)
            .Name("George")
            .Email("george@email.com")
            .Phone("+2717811109")
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
        UserAppointment update = IUserAppointmentBuilder.BuildUserAppointment()
            .Id(1)
            .PatientId(1)
            .DoctorId(3)
            .AppointmentId(1);
        _userRepository.Add(patient);
        _userRepository.Add(doctor);
        _userRepository.Add(anotherDoctor);
        _appointmentRepository.Add(appointment);
        _userAppointmentRepository.Add(userAppointment);
        
        // Act
        _service.Update(update);
        
        // Assert
        Assert.Contains(update, _userAppointmentRepository.GetList());
        Assert.Equal(update, _userAppointmentRepository.FindById(userAppointment.GetId())[0]);
        
        // Cleaning up
        _userRepository.Clear();
        _appointmentRepository.Clear();
        _userAppointmentRepository.Clear();
    }
}