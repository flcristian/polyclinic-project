using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.interfaces
{
    public class ICommandService<T> where T : IHasId
    {
        public static bool Add(IRepository<T> repository, T item)
        {
            if (repository.GetList().Any(i => i.Equals(item)))
            {
                return false;
            }

            repository.GetList().Add(item);
            return true;
        }

        public static bool Remove(IRepository<T> repository, T item)
        {
            if (!repository.GetList().Any(i => i.Equals(item)))
            {
                return false;
            }

            repository.GetList().Remove(item);
            return true;
        }

        public static bool RemoveById(IRepository<T> repository, int id)
        {
            //todo:handle with exceptions

            T item = repository.GetList().FirstOrDefault(i => i.GetId() == id);

            if (item == null)
            {
                return false;
            }

            repository.GetList().Remove(item);
            return true;
        }
    }
}
