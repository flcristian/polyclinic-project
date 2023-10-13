using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.user.exceptions
{
    public class UserIsNotADoctor : Exception
    {
        public UserIsNotADoctor(string? message) : base(message) { }
    }
}
