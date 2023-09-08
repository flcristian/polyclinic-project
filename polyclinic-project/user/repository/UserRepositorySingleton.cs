using polyclinic_project.system.constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.repository
{
    public class UserRepositorySingleton
    {
        private static readonly Lazy<UserRepository> _instance = new Lazy<UserRepository>(() => new UserRepository(Constants.USER_DATA_PATH));

        public static UserRepository Instance => _instance.Value;

        private UserRepositorySingleton() { }
    }
}
