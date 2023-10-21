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
        user = query.FindById(3);

        IView view = null!;
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
        view.RunMenu();
    }
}