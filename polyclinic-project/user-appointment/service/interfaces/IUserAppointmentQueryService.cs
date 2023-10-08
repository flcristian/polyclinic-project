using polyclinic_project.user_appointment.model;

namespace polyclinic_project.user_appointment.service.interfaces;

public interface IUserAppointmentQueryService
{
    UserAppointment FindById(int id);

    UserAppointment FindByEmail(String email);

    UserAppointment FindByPhone(String phone);

    int GetCount();
}