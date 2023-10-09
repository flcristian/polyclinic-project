using polyclinic_project_tests.TestConnectionString;
using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;

namespace polyclinic_project_tests.Tests.TestAppointment.service;

[Collection("Tests")]
public class TestAppointmentCommandService
{
    private static IAppointmentRepository _repository = new AppointmentRepository(ITestConnectionString.GetConnection("AppointmentCommandService"));
    private IAppointmentCommandService _service = new AppointmentCommandService(_repository);
    
    [Fact]
    public void TestAdd_IdAlreadyUsed_ThrowsItemAlreadyExistsException_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 13:00")
            .EndDate("07.10.2023 14:00");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestAdd_StartDateCoincidesWithAnotherAppointment_ThrowsItemAlreadyExistsException_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("07.10.2023 12:30")
            .EndDate("07.10.2023 14:00");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }
        
    [Fact]
    public void TestAdd_EndDateCoincidesWithAnotherAppointment_ThrowsItemAlreadyExistsException_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("07.10.2023 11:30")
            .EndDate("07.10.2023 12:30");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestAdd_StartDateSameOrAfterEndDate_ThrowsInvalidAppointmentScheduleException_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment1 = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 14:00")
            .EndDate("07.10.2023 13:00");
        Appointment appointment2 = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 13:00")
            .EndDate("07.10.2023 13:00");
        
        // Assert
        Assert.Throws<InvalidAppointmentSchedule>(() => _service.Add(appointment1));
        Assert.Throws<InvalidAppointmentSchedule>(() => _service.Add(appointment2));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestAdd_AppointmentIsUnique_AddsAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        
        // Act
        _service.Add(appointment);

        // Assert
        Assert.Contains(appointment, _repository.GetList());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestClearList_ClearsList()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        _repository.Add(appointment);
        
        // Act
        _service.ClearList();
        
        // Assert
        Assert.Empty(_repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestUpdate_AppointmentNotFound_ThrowsItemDoesNotExistException_DoesNotUpdateAnyAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Update(appointment));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestUpdate_AppointmentNotModified_ThrowsItemNotModifiedException_DoesNotUpdateAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        Appointment update = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemNotModified>(() => _service.Update(update));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestUpdate_AppointmentModified_UpdatesAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        Appointment update = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 13:00")
            .EndDate("07.10.2023 14:00");
        _repository.Add(appointment);
        
        // Act
        _service.Update(update);
        
        // Assert
        Assert.Contains(update, _repository.GetList());
        Assert.Equal(update, _repository.FindById(appointment.GetId())[0]);
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestDelete_AppointmentNotFound_ThrowsItemDoesNotExistException_DoesNotDeleteAnyAppointments()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Delete(appointment));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestDelete_AppointmentFound_DeletesAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        _repository.Add(appointment);
        
        // Act
        _service.Delete(appointment);
        
        // Assert
        Assert.DoesNotContain(appointment,_repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestDeleteById_AppointmentNotFound_ThrowsItemDoesNotExistException_DoesNotDeleteAnyAppointments()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.DeleteById(appointment.GetId()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestDeleteById_AppointmentFound_DeletesAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 12:00")
            .EndDate("07.10.2023 13:00");
        _repository.Add(appointment);
        
        // Act
        _service.DeleteById(appointment.GetId());
        
        // Assert
        Assert.DoesNotContain(appointment,_repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }
}