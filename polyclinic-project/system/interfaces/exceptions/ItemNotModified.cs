using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.interfaces.exceptions
{
    public class ItemNotModified : Exception
    {
        public ItemNotModified(string message) : base(message) { }
    }
}
