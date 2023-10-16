using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user_appointment.dtos
{
    public class DoctorViewAppointmentsResponse
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private String _patientName;
        private String _patientEmail;
        private String _patientPhone;

        public DateTime StartDate
        {
            get => _startDate;
            set => _startDate = value;
        }

        public DateTime EndDate
        {
            get => _endDate;
            set => _endDate = value;
        }

        public string PatientName
        {
            get => _patientName;
            set => _patientName = value;
        }

        public string PatientEmail
        {
            get => _patientEmail;
            set => _patientEmail = value;
        }

        public string PatientPhone
        {
            get => _patientPhone;
            set => _patientPhone = value;
        }
    }
}
