namespace polyclinic_project.user_appointment.model.interfaces;

public interface IUserAppointmentBuilder
{
    UserAppointment Id(int id);
    UserAppointment PacientId(int id);
    UserAppointment DoctorId(int id);
    UserAppointment AppointmentId(int id);
    
    public static UserAppointment BuildUserAppointment() { return new UserAppointment(); }
}