using polyclinic_project.appointment.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.service.interfaces
{
    public interface IAppointmentQueryService
    {
        Appointment FindById(int id);

        Appointment FindByDate(DateTime date);

        Appointment FindByDate(String date);

        int GetCount();
    }
}
