namespace polyclinic_project.user_appointment.service;

public class UserAppointmentCommandServiceSingleton
{
    private static readonly Lazy<UserAppointmentCommandService> _instance = new Lazy<UserAppointmentCommandService>(() => new UserAppointmentCommandService());

    public static UserAppointmentCommandService Instance => _instance.Value;
    
    private UserAppointmentCommandServiceSingleton() { }
}