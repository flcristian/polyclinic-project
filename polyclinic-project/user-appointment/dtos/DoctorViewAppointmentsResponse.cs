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

        public override bool Equals(object? obj)
        {
            DoctorViewAppointmentsResponse dto = obj as DoctorViewAppointmentsResponse;
            return dto._startDate.Equals(_startDate) && dto._endDate.Equals(_endDate) && dto._patientEmail.Equals(_patientEmail) && dto._patientName.Equals(_patientName) && dto._patientPhone.Equals(_patientPhone);
        }
    }
}
