using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project.system.interfaces
{
    public interface ICommandServiceUtility<T> where T : IHasId
    {
        public static bool Add(IRepository<T> repository, T item)
        {
            if (repository.GetList().Any(i => i.GetId() == item.GetId()))
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

            T? item = repository.GetList().FirstOrDefault(i => i.GetId() == id);

            if (item == null)
            {
                return false;
            }

            repository.GetList().Remove(item);
            return true;
        }

        public static bool ClearList(IRepository<T> repository)
        {
            if (!repository.GetList().Any())
            {
                return false;
            }

            repository.GetList().Clear();
            return true;
        }

        public static int EditById(IRepository<T> repository, int id, T item)
        {
            T? found = repository.GetList().FirstOrDefault(i => i.GetId() == id);

            if (found == null) { return -1; }
            if (item.Equals(found)) { return 0; }

            repository.GetList()[repository.GetList().IndexOf(found)] = item;
            return 1;
        }
    }
}
