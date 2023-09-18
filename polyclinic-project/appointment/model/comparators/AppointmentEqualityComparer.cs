using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.model.comparators
{
    public class AppointmentEqualityComparer : IEqualityComparer<Appointment>
    {
        public Boolean Equals(Appointment x, Appointment y)
        {
            return x.Equals(y);
        }

        public Int32 GetHashCode(Appointment x)
        {
            return x.GetHashCode();
        }
    }
}
