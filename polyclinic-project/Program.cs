﻿using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.view;
using polyclinic_project.view.interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+9127431")
            .Type(UserType.PATIENT);

        IViewPatient view = new ViewPatient(user);
        view.RunMenu();
    }
}