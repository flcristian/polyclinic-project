using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.model.comparators
{
    public class UserEqualityComparer : IEqualityComparer<User>
    {
        public bool Equals(User? user1, User? user2)
        {
            return user1.Equals(user2);
        }

        public int GetHashCode(User user)
        {
            return user.GetHashCode();
        }
    }
}
