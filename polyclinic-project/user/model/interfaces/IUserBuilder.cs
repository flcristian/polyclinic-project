using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.model.interfaces
{
    public interface IUserBuilder
    {
        User Id(int id);

        User Name(string name);

        User Email(string email);

        User Phone(string phone);

        User Type(UserType type);

        public static User BuildUser()
        {
            return new User();
        }
    }
}
