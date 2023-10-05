using polyclinic_project.appointment.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.service.interfaces
{
    public interface IAppointmentCommandService
    {
        void Add(Appointment appointment);

        void Delete(Appointment appointment);

        void DeleteById(int id);

        void ClearList();

        void Update(Appointment appointment);
    }
}
