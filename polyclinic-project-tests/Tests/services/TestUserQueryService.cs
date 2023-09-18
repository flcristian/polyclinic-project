using polyclinic_project.system.interfaces;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using polyclinic_project.user.service;
using polyclinic_project.user.service.interfaces;
using polyclinic_project.user.model.comparators;

namespace polyclinic_project_tests.Tests.service
{
    public class TestUserQueryService
    {
        private IRepository<User> _repository = new UserRepository("path");
        private IUserQueryService _query;

        [Fact]
        public void TestFindByEmail_UserNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            _query = new UserQueryService(_repository);
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _query.FindByEmail(user.GetEmail()));
        }

        [Fact]
        public void TestFindByEmail_UserFound_ReturnsUser()
        {
            // Arrange
            _query = new UserQueryService(_repository);
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Act
            User found = _query.FindByEmail(user.GetEmail());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(user, found, new UserEqualityComparer());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestFindByEmail_MultipleUsers_ReturnsCorrectUser()
        {
            // Arrange
            _query = new UserQueryService(_repository);
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            User another = IUserBuilder.BuildUser()
                .Id(2)
                .Name("Another")
                .Email("An email")
                .Phone("No phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);
            _repository.GetList().Add(another);

            // Act
            User found = _query.FindByEmail(user.GetEmail());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(user, found, new UserEqualityComparer());
            Assert.NotEqual(another, found, new UserEqualityComparer());
            _repository.GetList().Remove(user);
            _repository.GetList().Remove(another);
        }

        [Fact]
        public void TestFindByPhone_UserNotFound_ThrowsInvalidOperationException()
        {
            // Arrange
            _query = new UserQueryService(_repository);
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Assert
            Assert.Throws<InvalidOperationException>(() => _query.FindByPhone(user.GetPhone()));
        }

        [Fact]
        public void TestFindByPhone_UserFound_ReturnsUser()
        {
            // Arrange
            _query = new UserQueryService(_repository);
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);

            // Act
            User found = _query.FindByPhone(user.GetPhone());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(user, found, new UserEqualityComparer());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestFindByPhone_MultipleUsers_ReturnsCorrectUser()
        {
            // Arrange
            _query = new UserQueryService(_repository);
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            User another = IUserBuilder.BuildUser()
                .Id(2)
                .Name("Another")
                .Email("An email")
                .Phone("No phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user);
            _repository.GetList().Add(another);

            // Act
            User found = _query.FindByPhone(user.GetPhone());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(user, found, new UserEqualityComparer());
            Assert.NotEqual(another, found, new UserEqualityComparer());
            _repository.GetList().Remove(user);
            _repository.GetList().Remove(another);
        }
    }
}
