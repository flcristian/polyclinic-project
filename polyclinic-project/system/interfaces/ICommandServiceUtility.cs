using polyclinic_project.system.interfaces.exceptions;
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
        public static void Add(IRepository<T> repository, T item)
        {
            if (repository.GetList().Any(i => i.Equals(item)))
            {
                throw new ItemAlreadyExists($"{item} already exists.");
            }

            repository.GetList().Add(item);
        }

        public static void Remove(IRepository<T> repository, T item)
        {
            if (!repository.GetList().Any(i => i.Equals(item)))
            {
                throw new ItemDoesNotExist($"{item} does not exist or can not be found.");
            }

            repository.GetList().Remove(item);
        }

        public static void RemoveById(IRepository<T> repository, int id)
        {

            T? item = repository.GetList().FirstOrDefault(i => i.GetId() == id);

            if (item == null)
            {
                throw new NoItemWithThatId($"There is no item with Id {id}");
            }

            repository.GetList().Remove(item);
        }

        public static void ClearList(IRepository<T> repository)
        {
            if (!repository.GetList().Any())
            {
                throw new ListAlreadyEmpty("This repository already has an empty list.");
            }

            repository.GetList().Clear();
        }

        public static void EditById(IRepository<T> repository, int id, T item)
        {
            T? found = repository.GetList().FirstOrDefault(i => i.GetId() == id);

            if (found == null) { throw new NoItemWithThatId($"There is no item with Id {id}"); }
            if (item.Equals(found)) { throw new ItemNotModified($"No need to edit item since it was not modified"); }

            repository.GetList()[repository.GetList().IndexOf(found)] = item;
        }
    }
}
