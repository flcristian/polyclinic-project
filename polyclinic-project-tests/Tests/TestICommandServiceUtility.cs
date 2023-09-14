using polyclinic_project.system.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;

namespace polyclinic_project_tests.Tests
{
    public class TestICommandServiceUtility
    {
        [Fact]
        public void TestAdd_ItemExists_ReturnsFalse()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            repository.GetList().Add(user);

            // Act
            bool add = ICommandServiceUtility<User>.Add(repository, user);

            // Assert
            Assert.False(add);
            Assert.Single(repository.GetList());
        }

        [Fact]
        public void TestAdd_ItemAdded_ReturnsTrue()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            bool add = ICommandServiceUtility<User>.Add(repository, user);

            // Assert
            Assert.True(add);
            Assert.Single(repository.GetList());
        }

        [Fact]
        public void TestRemove_ItemDoesntExist_ReturnsFalse()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            bool remove = ICommandServiceUtility<User>.Remove(repository, user);

            // Assert
            Assert.False(remove);
        }

        [Fact]
        public void TestRemove_RemovesItem_ReturnsTrue()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            repository.GetList().Add(user);

            // Act
            bool remove = ICommandServiceUtility<User>.Remove(repository, user);

            // Assert
            Assert.True(remove);
            Assert.Empty(repository.GetList());
            Assert.DoesNotContain(user, repository.GetList());
        }

        [Fact]
        public void TestRemoveById_ItemDoesntExist_ReturnsFalse()
        {
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            bool remove = ICommandServiceUtility<User>.RemoveById(repository, user.GetId());

            // Assert
            Assert.False(remove);
        }

        [Fact]
        public void TestRemoveById_RemovesItem_ReturnsTrue()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            repository.GetList().Add(user);

            // Act
            bool remove = ICommandServiceUtility<User>.RemoveById(repository, user.GetId());

            // Assert
            Assert.True(remove);
            Assert.Empty(repository.GetList());
            Assert.DoesNotContain(user, repository.GetList());
        }

        [Fact]
        public void TestClearList_ListIsAlreadyClear_ReturnsFalse()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");

            // Act
            bool clear = ICommandServiceUtility<User>.ClearList(repository);

            // Assert
            Assert.False(clear);
            Assert.Empty(repository.GetList());
        }

        [Fact]
        public void TestClearList_ListCleared_ReturnsTrue()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            repository.GetList().Add(user);

            // Act
            bool clear = ICommandServiceUtility<User>.ClearList(repository);

            // Assert
            Assert.True(clear);
            Assert.Empty(repository.GetList());
        }

        [Fact]
        public void TestEditById_ItemDoesntExist_ReturnsNegative1()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            int edit = ICommandServiceUtility<User>.EditById(repository, user.GetId(), user);

            // Assert
            Assert.Equal(-1, edit);
        }

        [Fact]
        public void TestEditById_ItemNotModified_Returns0()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            repository.GetList().Add(user);

            // Act
            int edit = ICommandServiceUtility<User>.EditById(repository, user.GetId(), user);

            // Assert
            Assert.Equal(0, edit);
        }

        [Fact]
        public void TestEditById_ItemModified_Returns1()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            repository.GetList().Add(user);

            // Act
            User edited = IUserBuilder.BuildUser()
                .Id(1)
                .Name("Name")
                .Email("Another email")
                .Phone("A phone number")
                .Type(UserType.NONE);
            int edit = ICommandServiceUtility<User>.EditById(repository, edited.GetId(), edited);

            // Assert
            Assert.Equal(1, edit);
            Assert.Contains(edited, repository.GetList());
        }
    }
}