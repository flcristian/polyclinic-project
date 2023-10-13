using polyclinic_project.system.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user_appointment.dtos
{
    public class PatientGetDoctorFreeTimeResponse
    {
        private List<TimeInterval> _timeIntervals;

        public PatientGetDoctorFreeTimeResponse()
        {
            _timeIntervals = new List<TimeInterval>();
        }

        public List<TimeInterval> TimeIntervals
        {
            get { return _timeIntervals; }
            set { _timeIntervals = value; }
        }
    }
}
