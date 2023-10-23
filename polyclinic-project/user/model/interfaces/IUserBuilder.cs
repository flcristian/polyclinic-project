namespace polyclinic_project.user.model.interfaces
{
    public interface IUserBuilder
    {
        User Id(int id);

        User Name(string name);

        User Email(string email);

        User Password(string password);

        User Phone(string phone);

        User Type(UserType type);

        public static User BuildUser()
        {
            return new User();
        }
    }
}
