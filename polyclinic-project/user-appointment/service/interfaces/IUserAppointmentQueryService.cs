using polyclinic_project.user_appointment.model;

namespace polyclinic_project.user_appointment.service.interfaces;

public interface IUserAppointmentQueryService
{
    UserAppointment FindById(int id);

    UserAppointment FindByAppointmentId(int appointmentId);

    List<UserAppointment> FindByPatientId(int patientId);

    List<UserAppointment> FindByDoctorId(int doctorId);

    int GetCount();
}