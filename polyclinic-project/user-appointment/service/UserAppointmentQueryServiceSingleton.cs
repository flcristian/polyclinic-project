namespace polyclinic_project.user_appointment.service;

public class UserAppointmentQueryServiceSingleton
{
    private static readonly Lazy<UserAppointmentQueryService> _instance = new Lazy<UserAppointmentQueryService>(() => new UserAppointmentQueryService());

    public static UserAppointmentQueryService Instance => _instance.Value;
    
    private UserAppointmentQueryServiceSingleton() { }
}