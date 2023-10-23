using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
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
        IView view = null!;
        try
        {
            user = query.FindById(10);

            switch (user.GetType())
            {
                case UserType.PATIENT:
                    view = new ViewPatient(user);
                    break;
                case UserType.DOCTOR:
                    view = new ViewDoctor(user);
                    break;
                case UserType.ADMIN:
                    view = new ViewAdmin(user);
                    break;
                default:
                    break;
            }
        }
        catch (ItemDoesNotExist)
        {
            view = new ViewLogin();
        }
        view.RunMenu();
    }
}