using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.interfaces
{
    public interface IQueryServiceUtility<T> where T : IHasId
    {
        public static T FindById(IRepository<T> repository, int id)
        {
            return repository.GetList().First(i => i.GetId() == id);
        }

        public static int GetCount(IRepository<T> repository)
        {
            return repository.GetList().Count();
        }
    }
}
