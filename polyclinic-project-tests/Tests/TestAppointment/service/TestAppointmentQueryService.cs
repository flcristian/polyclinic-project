using polyclinic_project_tests;
using polyclinic_project.appointment.model;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.appointment.repository;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.appointment.service;
using polyclinic_project.appointment.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.system.constants;
using polyclinic_project.appointment.model.comparators;

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
        appointment = _repository.GetList()[0];

        // Act
        Appointment found = _service.FindById(appointment.GetId());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(appointment, found, new AppointmentEqualityComparer());

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
        appointment = _repository.GetList()[0];
        another = _repository.GetList()[1];

        // Act
        int count = _service.GetCount();

        // Assert
        Assert.Equal(_repository.GetList().Count, count);
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestCanAddAppointment_AnotherAppointmentScheduledInThatDate_ReturnsFalse()
    {
        // Arrange
        Appointment appointment = IAppointmentBuilder.BuildAppointment()
            .Id(1)
            .StartDate("06.10.2023 12:00")
            .EndDate("06.10.2023 13:00");
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 12:30")
            .EndDate("06.10.2023 14:00");
        _repository.Add(appointment);
        appointment = _repository.GetList()[0];

        // Assert
        Assert.False(_service.CanAddAppointment(add));

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestCanAddAppointment_NoOtherAppointmentsScheduledInThatDate_ReturnsTrue()
    {
        // Arrange
        Appointment add = IAppointmentBuilder.BuildAppointment()
            .Id(2)
            .StartDate("06.10.2023 12:30")
            .EndDate("06.10.2023 14:00");

        // Assert
        Assert.True(_service.CanAddAppointment(add));

        // Cleaning up
        _repository.Clear();
    }
}