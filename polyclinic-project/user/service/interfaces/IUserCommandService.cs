using polyclinic_project.user.model;

namespace polyclinic_project.user.service.interfaces
{
    public interface IUserCommandService
    {
        void Add(User user);

        void Delete(User user);

        void DeleteById(int id);

        void ClearList();

        void Update(User user);
    }
}
