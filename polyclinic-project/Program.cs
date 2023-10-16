using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.view;
using polyclinic_project.view.interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        IUserQueryService query = new UserQueryService();
        User user = null!;
        try
        {
            user = query.FindById(3);
        }
        catch (ItemDoesNotExist)
        {
            user = IUserBuilder.BuildUser()
                .Id(3)
                .Name("Marius")
                .Email("andrei@email.com")
                .Phone("+9127431")
                .Type(UserType.PATIENT);
        }

        IView view = new ViewDoctor(user);
        view.RunMenu();
    }
}