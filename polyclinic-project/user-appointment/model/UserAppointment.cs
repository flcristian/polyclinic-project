using polyclinic_project.system.interfaces;
using polyclinic_project.user_appointment.model.interfaces;

namespace polyclinic_project.user_appointment.model;

public class UserAppointment : IUserAppointmentBuilder, IPrototype<UserAppointment>
{
    private int id;
    private int patientId;
    private int doctorId;
    private int appointmentId;
    
    #region CONSTRUCTORS

    public UserAppointment()
    {
        this.id = -1;
        this.patientId = -1;
        this.doctorId = -1;
        this.appointmentId = -1;
    }

    public UserAppointment(int id, int patientId, int doctorId, int appointmentId)
    {
        this.id = id;
        this.patientId = patientId;
        this.doctorId = doctorId;
        this.appointmentId = appointmentId;
    }

    public UserAppointment(UserAppointment userAppointment)
    {
        this.id = userAppointment.id;
        this.patientId = userAppointment.patientId;
        this.doctorId = userAppointment.doctorId;
        this.appointmentId = userAppointment.appointmentId;
    }
    
    #endregion
    
    #region ACCESSORS
        public int GetId() { return this.id; }
        public int GetPatientId() { return this.patientId; }
        public int GetDoctorId() { return this.doctorId; }
        public int GetAppointmentId() { return this.appointmentId; }
        public void SetId(int id) { this.id = id; }
        public void SetPatientId(int patientId) { this.patientId = patientId; }
        public void SetDoctorId(int doctorId) { this.doctorId = doctorId; }
        public void SetAppointmentId(int appointmentId) { this.appointmentId = appointmentId; }
    #endregion
    
    #region BUILDER

    public UserAppointment Id(int id)
    {
        this.id = id;
        return this;
    }

    public UserAppointment PatientId(int patientId)
    {
        this.patientId = patientId;
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
        UserAppointment userAppointment = obj as UserAppointment;
        return userAppointment.patientId == this.patientId && userAppointment.doctorId == this.doctorId && userAppointment.appointmentId == this.appointmentId;
    }

    public override int GetHashCode()
    {
        return this.id * this.appointmentId + (int)Math.Pow(this.patientId, this.doctorId);
    }

    public override String ToString()
    {
        String desc = "";
        desc += $"Id : {this.id}\n";
        desc += $"Patient Id : {this.patientId}\n";
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