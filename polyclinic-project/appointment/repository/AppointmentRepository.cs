using polyclinic_project.appointment.model;
using polyclinic_project.appointment.repository.interfaces;
using polyclinic_project.system.data;
using Microsoft.Extensions.Configuration;
using polyclinic_project.system.constants;
using polyclinic_project.system.interfaces.exceptions;

namespace polyclinic_project.appointment.repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private string _connectionString;
        private DataAccess _dataAccess;

        #region CONSTRUCTORS
        
        public AppointmentRepository()
        {
            _dataAccess = new DataAccess();
            _connectionString = GetConnection();
        }

        public AppointmentRepository(String connectionString)
        {
            _dataAccess = new DataAccess();
            _connectionString = connectionString;
        }
        
        #endregion

        #region PUBLIC_METHODS

        public void Add(Appointment appointment)
        {
            string sql = "insert into appointment(id, startDate, endDate) values(@id, @startDate, @endDate)";

            _dataAccess.SaveData(sql, new { id = appointment.GetId(), startDate = appointment.GetStartDate(),  endDate = appointment.GetEndDate() }, _connectionString);
        }

        public void Delete(int id)
        {
            string sql = "delete from appointment where id = @id";

            _dataAccess.SaveData(sql, new { id }, _connectionString);
        }

        public void Update(Appointment appointment)
        {
            string sql = "update appointment set startDate = @startDate, endDate = @endDate where id = @id";

            _dataAccess.SaveData(sql, new { id = appointment.GetId(), startDate = appointment.GetStartDate(), endDate = appointment.GetEndDate() }, _connectionString);
        }

        public List<Appointment> FindById(int id)
        {
            string sql = "select * from appointment where id = @id";

            return _dataAccess.LoadData<Appointment, dynamic>(sql, new { id }, _connectionString).ToList();
        }
        
        public List<Appointment> GetList()
        {
            string sql = "select * from appointment";

            return _dataAccess.LoadData<Appointment, dynamic>(sql, new { }, _connectionString).ToList();
        }

        public int GetCount()
        {
            string sql = "select count(id) from appointment";

            return _dataAccess.LoadData<int, dynamic>(sql, new { }, _connectionString)[0];
        }

        public void Clear()
        {
            string sql = "delete from appointment";

            _dataAccess.SaveData(sql, new { }, _connectionString);
        }

        #endregion

        #region PRIVATE_METHODS

        private string GetConnection()
        {
            string c = Directory.GetCurrentDirectory();
            IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(c).AddJsonFile("appsettings.json").Build();
            string connectionString = configuration.GetConnectionString("Default")!;
            return connectionString;
        }
        
        #endregion
    }
}
