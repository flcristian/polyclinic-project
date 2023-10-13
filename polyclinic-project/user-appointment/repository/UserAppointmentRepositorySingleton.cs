namespace polyclinic_project.user_appointment.repository;

public class UserAppointmentRepositorySingleton
{
    private static readonly Lazy<UserAppointmentRepository> _instance = new Lazy<UserAppointmentRepository>(() => new UserAppointmentRepository());

    public static UserAppointmentRepository Instance => _instance.Value;
    
    private UserAppointmentRepositorySingleton() { }
}