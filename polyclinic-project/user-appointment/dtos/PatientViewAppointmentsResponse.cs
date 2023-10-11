using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user_appointment.dtos
{
    public class PatientViewAppointmentsResponse
    {
        private DateTime _startDate;
        private DateTime _endDate;
        private string _doctorName;

        #region ACCESSORS

        public DateTime StartDate 
        { 
            get { return _startDate; } 
            set { _startDate = value; }
        }
        public DateTime EndDate 
        { 
            get { return _endDate; }
            set { _endDate = value; }
        }
        public string DoctorName 
        {  
            get { return _doctorName; }
            set { _doctorName = value; }
        }

        #endregion

        // Methods

        public override bool Equals(object? obj)
        {
            PatientViewAppointmentsResponse check = obj as PatientViewAppointmentsResponse;
            return check._startDate.Equals(_startDate) && check._endDate.Equals(_endDate) && check._doctorName.Equals(_doctorName);
        }
    }
}
