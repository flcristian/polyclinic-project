using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using polyclinic_project.appointment.model.interfaces;
using polyclinic_project.system.constants;
using System.Globalization;
using polyclinic_project.system.interfaces;

namespace polyclinic_project.appointment.model
{
    public class AppointmentFactory : IFactory<Appointment>
    {
        public Appointment CreateObject(String text)
        {
            String[] data = text.Split('/');

            return IAppointmentBuilder.BuildAppointment()
                .Id(Int32.Parse(data[0]))
                .StartDate(DateTime.ParseExact(data[1], Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture))
                .EndDate(DateTime.ParseExact(data[2], Constants.STANDARD_DATE_FORMAT, CultureInfo.InvariantCulture));
        }
    }
}
