using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.model;

namespace polyclinic_project.user_appointment.service.interfaces;

public interface IUserAppointmentQueryService
{
    UserAppointment FindById(int id);

    UserAppointment FindByAppointmentId(int appointmentId);

    List<UserAppointment> FindByPatientId(int patientId);

    List<UserAppointment> FindByDoctorId(int doctorId);
    
    int GetCount();

    List<PatientViewAppointmentsResponse> ObtainAppointmentDatesAndDoctorNameByPatientId(int patientId);

    Boolean IsDoctorAvailable(String name, DateTime startDate, DateTime endDate);

    Boolean IsDoctorAvailableByEmail(String email, DateTime startDate, DateTime endDate);
}