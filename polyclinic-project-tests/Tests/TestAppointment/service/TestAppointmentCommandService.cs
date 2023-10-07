using polyclinic_project_tests.TestConnectionString;
using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;

namespace polyclinic_project_tests.Tests.TestAppointment.service;

public class TestAppointmentCommandService
{
    private static IAppointmentRepository _repository = new AppointmentRepository(ITestConnectionString.GetConnection());
    private IAppointmentCommandService _service = new AppointmentCommandService(_repository);
    
    [Fact]
    public void TestAdd_IdAlreadyUsed_ThrowsItemAlreadyExists_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("07.10.2023 13:00")
            .StartDate("07.10.2023 14:00");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }
    
    /*[Fact]
    public void TestAdd_StartDateCoincidesWithAnotherAppointment_ThrowsItemAlreadyExists_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("07.10.2023 12:30")
            .StartDate("07.10.2023 14:00");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }*/
        
    [Fact]
    public void TestAdd_EndDateCoincidesWithAnotherAppointment_ThrowsItemAlreadyExists_DoesNotAddAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("07.10.2023 11:30")
            .StartDate("07.10.2023 12:30");
        _repository.Add(appointment);
        
        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }
}