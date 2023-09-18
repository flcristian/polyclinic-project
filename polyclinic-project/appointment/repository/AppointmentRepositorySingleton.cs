using polyclinic_project.system.constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.repository
{
    public class AppointmentRepositorySingleton
    {
        private static readonly Lazy<AppointmentRepository> _instance = new Lazy<AppointmentRepository>(() => new AppointmentRepository(Constants.APPOINTMENT_DATA_PATH));

        public static AppointmentRepository Instance => _instance.Value;

        private AppointmentRepositorySingleton() { }
    }
}
