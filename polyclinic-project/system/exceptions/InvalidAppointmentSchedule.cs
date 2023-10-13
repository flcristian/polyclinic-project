namespace polyclinic_project.system.interfaces.exceptions;

public class InvalidAppointmentSchedule : Exception
{
    public InvalidAppointmentSchedule(string? message) : base(message) { }
}