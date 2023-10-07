using polyclinic_project.system.interfaces;
using polyclinic_project.user_appointment.model.interfaces;

namespace polyclinic_project.user_appointment.model;

public class UserAppointment : IUserAppointmentBuilder, IPrototype<UserAppointment>
{
    private int id;
    private int pacientId;
    private int doctorId;
    private int appointmentId;
    
    #region CONSTRUCTORS

    public UserAppointment()
    {
        this.id = -1;
        this.pacientId = -1;
        this.doctorId = -1;
        this.appointmentId = -1;
    }

    public UserAppointment(int id, int pacientId, int doctorId, int appointmentId)
    {
        this.id = id;
        this.pacientId = pacientId;
        this.doctorId = doctorId;
        this.appointmentId = appointmentId;
    }

    public UserAppointment(UserAppointment userAppointment)
    {
        this.id = userAppointment.id;
        this.pacientId = userAppointment.pacientId;
        this.doctorId = userAppointment.doctorId;
        this.appointmentId = userAppointment.appointmentId;
    }
    
    #endregion
    
    #region ACCESSORS
        public int GetId() { return this.id; }
        public int GetPacientId() { return this.pacientId; }
        public int GetDoctorId() { return this.doctorId; }
        public int GetAppointmentId() { return this.appointmentId; }
        public void SetId(int id) { this.id = id; }
        public void SetPacientId(int pacientId) { this.pacientId = pacientId; }
        public void SetDoctorId(int doctorId) { this.doctorId = doctorId; }
        public void SetAppointmentId(int appointmentId) { this.appointmentId = appointmentId; }
    #endregion
    
    #region BUILDER

    public UserAppointment Id(int id)
    {
        this.id = id;
        return this;
    }

    public UserAppointment PacientId(int pacientId)
    {
        this.pacientId = pacientId;
        return this;
    }

    public UserAppointment DoctorId(int doctorId)
    {
        this.doctorId = doctorId;
        return this;
    }

    public UserAppointment AppointmentId(int appointmentId)
    {
        this.appointmentId = appointmentId;
        return this;
    }
    
    #endregion
    
    #region PUBLIC_METHODS

    public override Boolean Equals(object? obj)
    {
        UserAppointment? userAppointment = obj as UserAppointment;
        return userAppointment.id == this.id && userAppointment.pacientId == this.pacientId && userAppointment.doctorId == this.id && userAppointment.appointmentId == this.appointmentId;
    }

    public override int GetHashCode()
    {
        return this.id * this.appointmentId + (int)Math.Pow(this.pacientId, this.doctorId);
    }

    public override String ToString()
    {
        String desc = "";
        desc += $"Id : {this.id}\n";
        desc += $"Pacient Id : {this.pacientId}\n";
        desc += $"Doctor Id : {this.doctorId}\n";
        desc += $"Appointment Id : {this.appointmentId}\n";
        return desc;
    }

    public UserAppointment Clone()
    {
        return new UserAppointment(this);
    }
    
    #endregion
}