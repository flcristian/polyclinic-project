﻿using polyclinic_project_tests;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.dtos;
using polyclinic_project.user.exceptions;
using polyclinic_project.user.model.comparators;

namespace polyclinic_project_tests.Tests.TestUser.service;

[Collection("Tests")]
public class TestUserQueryService
{
    private static IUserRepository _repository = new UserRepository(TestConnectionString.GetConnection("UserQueryService"));
    private IUserQueryService _service = new UserQueryService(_repository);
    
    [Fact]
    public void TestFindById_UserDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindById(user.GetId()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindById_UserExists_ReturnsCorrectUser()
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
        User found = _service.FindById(user.GetId());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByEmail_UserDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindByEmail(user.GetEmail()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByEmail_UserExists_ReturnsCorrectUser()
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
        User found = _service.FindByEmail(user.GetEmail());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByPhone_UserDoesNotExist_ThrowsItemDoesNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        
        // Assert
        Assert.Throws<ItemDoesNotExist>(() => _service.FindByPhone(user.GetPhone()));
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByPhone_UserExists_ReturnsCorrectUser()
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
        User found = _service.FindByPhone(user.GetPhone());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestGetCount_ReturnsCorrectCount()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        _repository.Add(another);
        
        // Act
        int count = _service.GetCount();

        // Assert
        Assert.Equal(_repository.GetList().Count, count);
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestObtainAllDoctorDetails_ReturnsStringListOfDoctorNames()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.DOCTOR);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.DOCTOR);
        _repository.Add(user);
        _repository.Add(another);

        // Act
        PatientViewAllDoctorsResponse response = _service.ObtainAllDoctorDetails();

        // Assert
        Assert.Equal(new List<User> { user, another }, response.Doctors, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestObtainAllDoctorNames_NoDoctorsAvailable_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.DOCTOR);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.DOCTOR);

        // Act
        Assert.Throws<ItemsDoNotExist>(() => _service.ObtainAllDoctorDetails());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestFindDoctorByName_NoDoctorsWithThatName_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User doctor1 = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Marian")
            .Email("marian1@email.com")
            .Phone("+12174633909")
            .Type(UserType.DOCTOR);
        User doctor2 = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian2@email.com")
            .Phone("+15399738970")
            .Type(UserType.DOCTOR);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.FindDoctorByName(doctor1.GetName()));

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestFindDoctorByName_MultipleDoctorsWithThatName_ThrowsMultipleDoctorsWithThatNameException()
    {
        // Arrange
        User doctor1 = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Marian")
            .Email("marian1@email.com")
            .Phone("+12174633909")
            .Type(UserType.DOCTOR);
        User doctor2 = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian2@email.com")
            .Phone("+15399738970")
            .Type(UserType.DOCTOR);
        _repository.Add(doctor1);
        _repository.Add(doctor2);

        // Assert
        Assert.Throws<MultipleDoctorsWithThatName>(() => _service.FindDoctorByName(doctor1.GetName()));

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestFindDoctorByName_DoctorExists_ReturnsDoctor()
    {
        // Arrange
        User doctor = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+12174633909")
            .Type(UserType.DOCTOR);
        _repository.Add(doctor);

        // Act
        User found = _service.FindDoctorByName(doctor.GetName());

        // Assert
        Assert.Equal(doctor, found, new UserEqualityComparer());
        Assert.Equal(UserType.DOCTOR, found.GetType());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetList_ReturnsCorrectList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        _repository.Add(another);

        // Act
        List<User> list = _service.GetList();

        // Assert
        Assert.Equal(new List<User> { user, another }, list, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetList_NoUsersExist_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.PATIENT);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.GetList());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetPatientList_ReturnsCorrectList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.DOCTOR);
        _repository.Add(user);
        _repository.Add(another);

        // Act
        List<User> list = _service.GetPatientList();

        // Assert
        Assert.Equal(new List<User> { user }, list, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetPatientList_NoPatientsExist_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.PATIENT);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.GetPatientList());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetDoctorList_ReturnsCorrectList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.DOCTOR);
        _repository.Add(user);
        _repository.Add(another);

        // Act
        List<User> list = _service.GetDoctorList();

        // Assert
        Assert.Equal(new List<User> { another }, list, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetDoctorList_NoDoctorsExist_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.PATIENT);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.GetDoctorList());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetAdminList_ReturnsCorrectList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.ADMIN);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.DOCTOR);
        _repository.Add(user);
        _repository.Add(another);

        // Act
        List<User> list = _service.GetAdminList();

        // Assert
        Assert.Equal(new List<User> { user }, list, new UserEqualityComparer());

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetAdminList_NoAdminsExist_ThrowsItemsDoNotExistException()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+989133909")
            .Type(UserType.PATIENT);

        // Assert
        Assert.Throws<ItemsDoNotExist>(() => _service.GetAdminList());

        // Cleaning up
        _repository.Clear();
    }
}