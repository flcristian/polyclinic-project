using Microsoft.Extensions.Configuration;
using polyclinic_project.system.data;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user_appointment.model;
using polyclinic_project.user_appointment.repository.interfaces;

namespace polyclinic_project.user_appointment.repository;

public class UserAppointmentRepository : IUserAppointmentRepository
{
    private string _connectionString;
    private DataAccess _dataAccess;
    
    #region CONSTRUCTORS

    public UserAppointmentRepository()
    {
        _connectionString = GetConnection();
        _dataAccess = new DataAccess();
    }

    public UserAppointmentRepository(string connectionString)
    {
        _connectionString = connectionString;
        _dataAccess = new DataAccess();
    }
    
    #endregion
    
    #region PUBLIC_METHODS
    
    public void Add(UserAppointment userAppointment)
    {
        string sql = "insert into user_appointment values(@id,@pacientId,@doctorId,@appointmentId)";
        
        _dataAccess.SaveData(sql, new { id = userAppointment.GetId(), pacientId = userAppointment.GetPacientId(), doctorId = userAppointment.GetDoctorId(), appointmentId = userAppointment.GetAppointmentId() }, _connectionString);
    }

    public void Delete(int id)
    {
        string sql = "delete from user_appointment where id = @id";

        _dataAccess.SaveData(sql, new { id }, _connectionString);
    }

    public void Update(UserAppointment userAppointment)
    {
        string sql = "update user_appointment set pacientId = @pacientId, doctorId = @doctorId, appointmentId = @appointmentId where id = @id";
        
        _dataAccess.SaveData(sql, new { id = userAppointment.GetId(), pacientId = userAppointment.GetPacientId(), doctorId = userAppointment.GetDoctorId(), appointmentId = userAppointment.GetAppointmentId() }, _connectionString);
    }

    public List<UserAppointment> FindById(int id)
    {
        string sql = "select * from user_appointment where id = @id";

        return _dataAccess.LoadData<UserAppointment, dynamic>(sql, new { id }, _connectionString);
    }

    public List<UserAppointment> FindByPacientId(int pacientId)
    {
        string sql = "select * from user_appointment where pacientId = @pacientId";
        
        return _dataAccess.LoadData<UserAppointment, dynamic>(sql, new { pacientId }, _connectionString);
    }

    public List<UserAppointment> FindByDoctorId(int doctorId)
    {
        string sql = "select * from user_appointment where doctorId = @doctorId";
        
        return _dataAccess.LoadData<UserAppointment, dynamic>(sql, new { doctorId }, _connectionString);
    }

    public List<UserAppointment> FindByAppointmentId(int appointmentId)
    {
        string sql = "select * from user_appointment where appointmentId = @appointmentId";
        
        return _dataAccess.LoadData<UserAppointment, dynamic>(sql, new { appointmentId }, _connectionString);
    }

    public List<UserAppointment> GetList()
    {
        string sql = "select * from user_appointment";

        return _dataAccess.LoadData<UserAppointment, dynamic>(sql, new { }, _connectionString);
    }

    public int GetCount()
    {
        string sql = "select count(id) from user_appointment";

        return _dataAccess.LoadData<int, dynamic>(sql, new { }, _connectionString)[0];
    }

    public void Clear()
    {
        string sql = "delete from user_appointment";

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