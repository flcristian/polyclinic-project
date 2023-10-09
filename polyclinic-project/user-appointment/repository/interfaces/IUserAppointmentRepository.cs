using polyclinic_project.user_appointment.model;

namespace polyclinic_project.user_appointment.repository.interfaces;

public interface IUserAppointmentRepository
{
    void Add(UserAppointment userAppointment);
    void Delete(int id);
    void Update(UserAppointment userAppointment);
    List<UserAppointment> FindById(int id);
    List<UserAppointment> FindByPacientId(int pacientId);
    List<UserAppointment> FindByDoctorId(int doctorId);
    List<UserAppointment> FindByAppointmentId(int appointmentId);
    List<UserAppointment> GetList();
    int GetCount();
    void Clear();
}