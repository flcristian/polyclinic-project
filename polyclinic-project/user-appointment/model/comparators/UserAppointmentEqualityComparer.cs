namespace polyclinic_project.user_appointment.model.comparators;

public class UserAppointmentEqualityComparer : IEqualityComparer<UserAppointment>
{
    public bool Equals(UserAppointment? x, UserAppointment? y)
    {
        return x.Equals(y);
    }

    public int GetHashCode(UserAppointment obj)
    {
        return obj.GetHashCode();
    }
}