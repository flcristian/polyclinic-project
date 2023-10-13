using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.dtos
{
    public class PatientViewAllDoctorsResponse
    {
        private List<User> _doctors;

        public PatientViewAllDoctorsResponse()
        {
            _doctors = new List<User>();
        }

        public List<User> Doctors
        {
            get { return _doctors; }
            set { _doctors = value; }
        }
    }
}
