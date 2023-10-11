using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.dtos
{
    public class PatientViewAllDoctorsResponse
    {
        private List<String> _doctors;

        public List<String> Doctors
        {
            get { return _doctors; }
            set { _doctors = value; }
        }
    }
}
