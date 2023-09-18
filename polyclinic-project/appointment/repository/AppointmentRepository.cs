using polyclinic_project.appointment.model;
using polyclinic_project.system.interfaces;
using polyclinic_project.user.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.appointment.repository
{
    public class AppointmentRepository : IRepository<Appointment>
    {
        private List<Appointment> _list;
        private String _path;

        // Constants

        public AppointmentRepository(String path)
        {
            _list = new List<Appointment>();
            _path = path;
        }

        public AppointmentRepository(List<Appointment> list, String path)
        {
            _list = list;
            _path = path;
        }

        // Accessors

        public void LoadData()
        {
            _list = IDataRepository<Appointment>.LoadData(_path, new AppointmentFactory());
        }

        public void SaveData()
        {
            IDataRepository<Appointment>.SaveData(_path, _list);
        }

        public List<Appointment> GetList() { return _list; }
    }
}
