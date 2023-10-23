using polyclinic_project.appointment.model;

namespace polyclinic_project.appointment.service.interfaces
{
    public interface IAppointmentQueryService
    {
        Appointment FindById(int id);

        bool CanAddAppointment(Appointment appointment);

        int GetCount();

        int GetLastId();
    }
}
