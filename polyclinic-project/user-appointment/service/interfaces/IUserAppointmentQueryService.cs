using polyclinic_project.appointment.model;
using polyclinic_project.user.model;
using polyclinic_project.user_appointment.dtos;
using polyclinic_project.user_appointment.model;

namespace polyclinic_project.user_appointment.service.interfaces;

public interface IUserAppointmentQueryService
{
    UserAppointment FindById(int id);

    UserAppointment FindByAppointmentId(int appointmentId);

    List<UserAppointment> FindByPatientId(int patientId);

    List<UserAppointment> FindByDoctorId(int doctorId);

    UserAppointment FindByDoctorIdAndAppointment(int doctorId, Appointment appointment);

    int GetCount();

    List<PatientViewAppointmentsResponse> ObtainAppointmentDatesAndDoctorNameByPatientId(int patientId);

    PatientGetDoctorFreeTimeResponse GetDoctorFreeTime(int doctorId, DateTime date, TimeSpan duration);

    UserAppointment FindByPatientIdAndDates(int patientId, DateTime startDate, DateTime endDate);
}