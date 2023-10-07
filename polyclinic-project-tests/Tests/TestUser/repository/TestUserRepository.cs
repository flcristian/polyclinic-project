using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;
using polyclinic_project_tests.TestConnectionString;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;

namespace polyclinic_project_tests.Tests.TestUser.repository;

[Collection("Tests")]
public class TestUserRepository
{
    private IUserRepository _repository = new UserRepository(ITestConnectionString.GetConnection("UserRepository")); 
    
    [Fact]
    public void TestAdd_AddsUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        
        // Act
        _repository.Add(user);
        
        // Assert
        Assert.Contains(user, _repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestDelete_DeletesUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        
        // Act
        _repository.Delete(user.GetId());
        
        // Assert
        Assert.DoesNotContain(user, _repository.GetList());
        
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
            .Type(UserType.PACIENT);
        User update = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        
        // Act
        _repository.Update(update);
        
        // Assert
        Assert.Contains(update, _repository.GetList());
        Assert.Equal(update, _repository.FindById(user.GetId()));
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestFindById_ReturnsUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        
        // Act
        User found = _repository.FindById(user.GetId());
        
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found);
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByEmail_ReturnsUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        
        // Act
        User found = _repository.FindByEmail(user.GetEmail());
        
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found);
        
        // Cleaning up
        _repository.Clear();
    }
    
    [Fact]
    public void TestFindByPhone_ReturnsUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        
        // Act
        User found = _repository.FindByPhone(user.GetPhone());
        
        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found);
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetList_ReturnsList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.PACIENT);
        List<User> list = new List<User> { user, another };
        _repository.Add(user);
        _repository.Add(another);
        
        // Assert
        Assert.Equal(list, _repository.GetList());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestGetCount_ReturnsCount()
    {
        
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        _repository.Add(another);
        
        // Assert
        Assert.Equal(2, _repository.GetCount());
        
        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestClear_ClearsList()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PACIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.PACIENT);
        _repository.Add(user);
        _repository.Add(another);
        
        // Act
        _repository.Clear();
        
        // Assert
        Assert.Equal(0, _repository.GetCount());
        Assert.Empty(_repository.GetList());
    }
}