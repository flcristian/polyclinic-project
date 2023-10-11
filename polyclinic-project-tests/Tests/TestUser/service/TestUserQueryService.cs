using polyclinic_project_tests;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.dtos;

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
        
        // Act
        User found = _service.FindById(user.GetId());
                    
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found);

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
        Assert.Equal(user, found);

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
        Assert.Equal(user, found);

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
    public void TestObtainAllDoctorNames_ReturnsStringListOfDoctorNames()
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
        PatientViewAllDoctorsResponse response = _service.ObtainAllDoctorNames();

        // Assert
        Assert.Equal(new List<String> { user.GetName(), another.GetName() }, response.Doctors);

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestObtainAllDoctorNames_NoDoctorsExist_ThrowsItemsDoNotExistException()
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
        Assert.Throws<ItemsDoNotExist>(() => _service.ObtainAllDoctorNames());

        // Cleaning up
        _repository.Clear();
    }
}