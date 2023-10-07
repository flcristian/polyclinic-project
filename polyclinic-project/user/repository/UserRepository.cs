using Microsoft.Extensions.Configuration;
using polyclinic_project.system.data;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.repository.interfaces;
namespace polyclinic_project.user.repository
{
    public class UserRepository : IUserRepository
    {
        private string _connectionString;
        private DataAccess _dataAccess;

        #region CONSTRUCTORS

        public UserRepository()
        {
            _dataAccess = new DataAccess();
            _connectionString = GetConnection();
        }

        public UserRepository(string connectionString)
        {
            _dataAccess = new DataAccess();
            _connectionString = connectionString;
        }
        
        #endregion

        #region PUBLIC_METHODS

        public void Add(User user)
        {
            string sql = "insert into user(id,name,email,phone,type) values(@id,@name,@email,@phone,@type)";

            _dataAccess.SaveData(sql, new { id =  user.GetId(), name = user.GetName(), email = user.GetEmail(), phone = user.GetPhone(), type = user.GetType() }, _connectionString);
        }

        public void Delete(int id)
        {
            string sql = "delete from user where id = @id";

            _dataAccess.SaveData(sql, new { id }, _connectionString);
        }

        public void Update(User user)
        {
            string sql = "update user set name = @name,email = @email,phone = @phone,type = @type where id = @id";

            _dataAccess.SaveData(sql, new { id = user.GetId(), name = user.GetName(), email = user.GetEmail(), phone = user.GetPhone(), type = user.GetType() }, _connectionString);
        }

        public User FindById(int id)
        {
            string sql = "select * from user where id=@id";

            List<User> result = _dataAccess.LoadData<User, dynamic>(sql, new { id }, _connectionString);
            if (result.Count == 0) 
                throw new ItemDoesNotExist("User does not exist"); // TO ADD EXCEPTIONS
            return result[0];
        }

        public User FindByEmail(string email)
        {
            string sql = "select * from user where email=@email";

            List<User> result = _dataAccess.LoadData<User, dynamic>(sql, new { email }, _connectionString);
            if (result.Count == 0) 
                throw new ItemDoesNotExist("User does not exist"); // TO ADD EXCEPTIONS
            return result[0];
        }

        public User FindByPhone(string phone)
        {
            string sql = "select * from user where phone=@phone";

            List<User> result = _dataAccess.LoadData<User, dynamic>(sql, new { phone }, _connectionString);
            if (result.Count == 0) 
                throw new ItemDoesNotExist("User does not exist"); // TO ADD EXCEPTIONS
            return result[0];
        }

        public List<User> GetList()
        {
            string sql = "select * from user";

            return _dataAccess.LoadData<User, dynamic>(sql, new { }, _connectionString);
        }

        public int GetCount()
        {
            string sql = "select count(id) from user";

            return _dataAccess.LoadData<int, dynamic>(sql, new { }, _connectionString)[0];
        }

        public void Clear()
        {
            string sql = "delete from user";

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
