using System.Globalization;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project_tests;
using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.system.constants;

namespace polyclinic_project_tests.Tests.TestAppointment.repository;

[Collection("Tests")]
public class TestAppointmentRepository
{
    private IAppointmentRepository _repository = new AppointmentRepository(TestConnectionString.GetConnection("AppointmentRepository")); 
    
    [Fact]
    public void TestAdd_AddsAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        
        // Act
        _repository.Add(appointment);
        
        // Assert
        Assert.Contains(appointment, _repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestDelete_DeletesAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        _repository.Add(appointment);
        
        // Act
        _repository.Delete(appointment.GetId());
        
        // Assert
        Assert.DoesNotContain(appointment, _repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestUpdate_UpdatesAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment update = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 13:10")
            .EndDate("06.10.2023 13:30");
        _repository.Add(appointment);
        
        // Act
        _repository.Update(update);
        
        // Assert
        Assert.Contains(update, _repository.GetList());
        Assert.Equal(update, _repository.FindById(appointment.GetId())[0]);
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestFindById_ReturnsAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        _repository.Add(appointment);
        
        // Act
        Appointment found = _repository.FindById(appointment.GetId())[0];
        
        // Assert
        Assert.NotNull(found);
        Assert.Equal(appointment, found);
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetList_ReturnsList()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment another = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:10")
            .EndDate("06.10.2023 13:30");
        List<Appointment> list = new List<Appointment> { appointment, another };
        _repository.Add(appointment);
        _repository.Add(another);
        
        // Assert
        Assert.Equal(list, _repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetCount_ReturnsCount()
    {
        
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment another = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:10")
            .EndDate("06.10.2023 13:30");
        _repository.Add(appointment);
        _repository.Add(another);
        
        // Assert
        Assert.Equal(2, _repository.GetCount());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestClear_ClearsList()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment another = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:10")
            .EndDate("06.10.2023 13:30");
        _repository.Add(appointment);
        _repository.Add(another);
        
        // Act
        _repository.Clear();
        
        // Assert
        Assert.Equal(0, _repository.GetCount());
        Assert.Empty(_repository.GetList());
    }
}