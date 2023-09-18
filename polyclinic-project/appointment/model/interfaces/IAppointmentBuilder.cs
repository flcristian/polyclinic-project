using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.model.interfaces
{
    public interface IAppointmentBuilder
    {
        Appointment Id(int id);

        Appointment StartDate(DateTime startDate);

        Appointment StartDate(String startDate);

        Appointment EndDate(DateTime endDate);

        Appointment EndDate(String endDate);

        public static Appointment BuildAppointment()
        {
            return new Appointment();
        }
    }
}
