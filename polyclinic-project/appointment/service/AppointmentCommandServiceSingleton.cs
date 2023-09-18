using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.service
{
    public class AppointmentCommandServiceSingleton
    {
        private static readonly Lazy<AppointmentCommandService> _instance = new Lazy<AppointmentCommandService>(() => new AppointmentCommandService());

        public static AppointmentCommandService Instance => _instance.Value;

        private AppointmentCommandServiceSingleton() { }
    }
}
