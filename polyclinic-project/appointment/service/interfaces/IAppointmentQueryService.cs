using polyclinic_project.appointment.model;

namespace polyclinic_project.appointment.service.interfaces
{
    public interface IAppointmentQueryService
    {
        Appointment FindById(int id);

        Appointment FindByDate(DateTime date);

        Appointment FindByDate(String date);

        bool CanAddAppointment(Appointment appointment);

        int GetCount();
    }
}
