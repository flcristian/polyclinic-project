using polyclinic_project.system.interfaces;
using polyclinic_project.system.interfaces.exceptions;
using polyclinic_project.user.model;
using polyclinic_project.user.model.comparators;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.repository;

namespace polyclinic_project_tests.Tests.service
{
    public class TestICommandServiceUtility
    {
        private IRepository<User> _repository = new UserRepository("path");

        [Fact]
        public void TestAdd_ItemAlreadyExists_ThrowsItemAlreadyExistsException_DoesNotAddItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Assert
            Assert.Throws<ItemAlreadyExists>(() => ICommandServiceUtility<User>.Add(_repository, user));
            Assert.Single(_repository.GetList());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestAdd_NoIdenticalItemAlreadyExists_AddsItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            ICommandServiceUtility<User>.Add(_repository, user);

            // Assert
            Assert.Contains(user, _repository.GetList(), new UserEqualityComparer());
            Assert.Single(_repository.GetList());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestRemove_ItemDoesNotExist_ThrowsItemDoesNotExistException_DoesNotRemoveItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Assert
            Assert.Throws<ItemDoesNotExist>(() => ICommandServiceUtility<User>.Remove(_repository, user));
        }

        [Fact]
        public void TestRemove_ItemExists_RemovesItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Act
            ICommandServiceUtility<User>.Remove(_repository, user);

            // Assert
            Assert.Empty(_repository.GetList());
            Assert.DoesNotContain(user, _repository.GetList(), new UserEqualityComparer());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestRemoveById_NoItemWithThatId_ReturnsNoItemWithThatIdException_DoesNotRemoveItem()
        {
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Assert
            Assert.Throws<NoItemWithThatId>(() => ICommandServiceUtility<User>.RemoveById(_repository, user.GetId()));
        }

        [Fact]
        public void TestRemoveById_ItemExistsWithThatId_RemovesItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            ICommandServiceUtility<User>.RemoveById(_repository, user.GetId());

            // Assert
            Assert.Empty(_repository.GetList());
            Assert.DoesNotContain(user, _repository.GetList(), new UserEqualityComparer());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestClearList_ListAlreadyEmpty_ThrowsListAlreadyEmptyException_DoesNotClearList()
        {
            Assert.Throws<ListAlreadyEmpty>(() => ICommandServiceUtility<User>.ClearList(_repository));
        }

        [Fact]
        public void TestClearList_ListNotEmpty_ClearsList()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Act
            ICommandServiceUtility<User>.ClearList(_repository);

            // Assert
            Assert.Empty(_repository.GetList());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestEditById_NoItemWithThatId_ThrowsNoItemWithThatIdException_DoesNotEditItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            Assert.Throws<NoItemWithThatId>(() => ICommandServiceUtility<User>.EditById(_repository, user.GetId(), user));
        }

        [Fact]
        public void TestEditById_ItemNotModified_ThrowsItemNotModifiedException_DoesNotEditItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Assert
            Assert.Throws<ItemNotModified>(() => ICommandServiceUtility<User>.EditById(_repository, user.GetId(), user));
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestEditById_ItemModified_EditsItem()
        {
            // Arrange
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Act
            User edited = IUserBuilder.BuildUser()
                .Id(1)
                .Name("Name")
                .Email("Another email")
                .Phone("A phone number")
                .Type(UserType.NONE);
            ICommandServiceUtility<User>.EditById(_repository, edited.GetId(), edited);

            // Assert
            Assert.DoesNotContain(user, _repository.GetList(), new UserEqualityComparer());
            Assert.Contains(edited, _repository.GetList(), new UserEqualityComparer());
            _repository.GetList().Remove(user);
            _repository.GetList().Remove(edited);
        }
    }
}