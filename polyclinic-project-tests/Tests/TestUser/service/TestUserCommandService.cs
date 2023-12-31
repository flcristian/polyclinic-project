﻿using polyclinic_project_tests;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model.comparators;
using Microsoft.VisualBasic;

namespace polyclinic_project_tests.Tests.TestUser.service;

[Collection("Tests")]
public class TestUserCommandService
{
    private static IUserRepository _repository = new UserRepository(TestConnectionString.GetConnection("UserCommandService"));
    private IUserCommandService _service = new UserCommandService(_repository);
    
    [Fact]
    public void TestAdd_EmailAlreadyUsed_ThrowsItemAlreadyExistsException_DoesNotAddUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User add = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+71623163111")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        user = _repository.FindByEmail(user.GetEmail())[0];

        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestAdd_PhoneAlreadyUsed_ThrowsItemAlreadyExistsException_DoesNotAddUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User add = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        user = _repository.FindByEmail(user.GetEmail())[0];

        // Assert
        Assert.Throws<ItemAlreadyExists>(() => _service.Add(add));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestAdd_UserIsUnique_AddsUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);

        // Act
        _service.Add(user);

        // Assert
        Assert.Contains(user, _repository.GetList(), new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestClearList_ClearsList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        
        // Act
        _service.ClearList();
        
        // Assert
        Assert.Empty(_repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestUpdate_UserNotFound_ThrowsItemDoesNotExistException_DoesNotUpdateAnyUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);

        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Update(user));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestUpdate_UpdatesUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User update = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        user = _repository.FindByEmail(user.GetEmail())[0];
        update.SetId(user.GetId());

        // Act
        _service.Update(update);
        
        // Assert
        Assert.Contains(update, _repository.GetList(), new UserEqualityComparer());
        Assert.Equal(update, _repository.FindById(user.GetId())[0], new UserEqualityComparer());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestDelete_UserNotFound_ThrowsItemDoesNotExistException_DoesNotDeleteAnyUsers()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);

        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.Delete(user));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestDelete_UserFound_DeletesUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        user = _repository.FindByEmail(user.GetEmail())[0];

        // Act
        _service.Delete(user);
        
        // Assert
        Assert.DoesNotContain(user,_repository.GetList(), new UserEqualityComparer());
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestDeleteById_UserNotFound_ThrowsItemDoesNotExistException_DoesNotDeleteAnyUsers()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.DeleteById(user.GetId()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestDeleteById_UserFound_DeletesUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        user = _repository.FindByEmail(user.GetEmail())[0];

        // Act
        _service.DeleteById(user.GetId());
        
        // Assert
        Assert.DoesNotContain(user,_repository.GetList(), new UserEqualityComparer());
        
        // Cleaning up
        _repository.Clear();
    }
}