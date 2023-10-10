using polyclinic_project_tests;
using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.system.constants;

namespace polyclinic_project_tests.Tests.TestAppointment.service;

[Collection("Tests")]
public class TestAppointmentQueryService
{
    private static IAppointmentRepository _repository = new AppointmentRepository(TestConnectionString.GetConnection("AppointmentQueryService"));
    private IAppointmentQueryService _service = new AppointmentQueryService(_repository);
    
    [Fact]
    public void TestFindById_AppointmentDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindById(appointment.GetId()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindById_AppointmentExists_ReturnsCorrectAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        _repository.Add(appointment);
        
        // Act
        Appointment found = _service.FindById(appointment.GetId());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(appointment, found);

        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByDateUsingDateTime_AppointmentDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindByDate(appointment.GetStartDate()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByDateUsingDateTime_AppointmentExists_ReturnsCorrectAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        _repository.Add(appointment);          
        
        // Act
        Appointment found = _service.FindByDate(appointment.GetStartDate());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(appointment, found);

        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByDateUsingString_AppointmentDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindByDate(appointment.GetStartDate().ToString(Constants.SQL_DATE_FORMAT)));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByDateUsingString_AppointmentExists_ReturnsCorrectAppointment()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        _repository.Add(appointment);          
        
        // Act
        Appointment found = _service.FindByDate(appointment.GetStartDate().ToString(Constants.SQL_DATE_FORMAT));
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(appointment, found);

        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestGetCount_ReturnsCorrectCount()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment another = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 13:00")
            .EndDate("06.10.2023 14:00");
        _repository.Add(appointment);
        _repository.Add(another);
        
        // Act
        int count = _service.GetCount();

        // Assert
        Assert.Equal(_repository.GetList().Count, count);
        
        // Cleaning up
        _repository.Clear();
    }
}