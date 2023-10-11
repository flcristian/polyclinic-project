using polyclinic_project.user.dtos;
using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.service.interfaces
{
    public interface IUserQueryService
    {
        User FindById(int id);

        User FindByEmail(String email);

        User FindByPhone(String phone);

        int GetCount();

        PatientViewAllDoctorsResponse ObtainAllDoctorNames();
    }
}
