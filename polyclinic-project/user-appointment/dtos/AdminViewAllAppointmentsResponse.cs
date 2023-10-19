using polyclinic_project.appointment.model;
using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user_appointment.dtos
{
    public class AdminViewAllAppointmentsResponse
    {
        private User _patient;
        private User _doctor;
        private Appointment _appointment;

        public User Patient
        {
            get { return _patient; }
            set { _patient = value; }
        }

        public User Doctor
        {
            get { return _doctor; }
            set { _doctor = value; }
        }

        public Appointment Appointment
        {
            get { return _appointment; }
            set { _appointment = value; }
        }

        public override bool Equals(object? obj)
        {
            AdminViewAllAppointmentsResponse response = obj as AdminViewAllAppointmentsResponse;
            return response._patient.Equals(_patient) && response._doctor.Equals(_doctor) && response._appointment.Equals(_appointment);
        }
    }
}
