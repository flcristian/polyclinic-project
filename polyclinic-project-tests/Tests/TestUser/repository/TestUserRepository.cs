using polyclinic_project.user.dtos;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;
using polyclinic_project.user.repository.interfaces;

namespace polyclinic_project_tests.Tests.TestUser.repository;

[Collection("Tests")]
public class TestUserRepository
{
    private IUserRepository _repository = new UserRepository(TestConnectionString.GetConnection("UserRepository"));

    [Fact]
    public void TestAdd_AddsUser()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);

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
            .Type(UserType.PATIENT);
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
            .Type(UserType.PATIENT);
        User update = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+12174633909")
            .Type(UserType.PATIENT);
        _repository.Add(user);

        // Act
        _repository.Update(update);

        // Assert
        Assert.Contains(update, _repository.GetList());
        Assert.Equal(update, _repository.FindById(user.GetId())[0]);

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
            .Type(UserType.PATIENT);
        _repository.Add(user);

        // Act
        User found = _repository.FindById(user.GetId())[0];

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
            .Type(UserType.PATIENT);
        _repository.Add(user);

        // Act
        User found = _repository.FindByEmail(user.GetEmail())[0];

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
            .Type(UserType.PATIENT);
        _repository.Add(user);

        // Act
        User found = _repository.FindByPhone(user.GetPhone())[0];

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
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.PATIENT);
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
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.PATIENT);
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
            .Type(UserType.PATIENT);
        User another = IUserBuilder.BuildUser()
            .Id(2)
            .Name("Marian")
            .Email("marian@email.com")
            .Phone("+15399738970")
            .Type(UserType.PATIENT);
        _repository.Add(user);
        _repository.Add(another);

        // Act
        _repository.Clear();

        // Assert
        Assert.Equal(0, _repository.GetCount());
        Assert.Empty(_repository.GetList());
    }

    [Fact]
    public void TestObtainAllDoctorDetails_ReturnsStringListOfDoctors()
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
        PatientViewAllDoctorsResponse response = _repository.ObtainAllDoctorDetails();

        // Assert
        Assert.Equal(new List<User> { user, another }, response.Doctors);

        // Cleaning up
        _repository.Clear();
    }

    [Fact]
    public void TestFindDoctorsByName_ReturnsDoctor()
    {
        // Arrange
        User user = IUserBuilder.BuildUser()
            .Id(1)
            .Name("Andrei")
            .Email("andrei@email.com")
            .Phone("+12174633909")
            .Type(UserType.DOCTOR);
        _repository.Add(user);

        // Act
        User found = _repository.FindDoctorsByName(user.GetName())[0];

        // Assert
        Assert.NotNull(found);
        Assert.Equal(user, found);

        // Cleaning up
        _repository.Clear();
    }
}