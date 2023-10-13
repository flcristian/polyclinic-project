using polyclinic_project.user_appointment.model;

namespace polyclinic_project.user_appointment.service.interfaces;

public interface IUserAppointmentCommandService
{
    void Add(UserAppointment userAppointment);

    void Delete(UserAppointment userAppointment);

    void DeleteById(int id);

    void ClearList();

    void Update(UserAppointment userAppointment);
}