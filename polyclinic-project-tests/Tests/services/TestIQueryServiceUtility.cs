using polyclinic_project.system.interfaces;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using polyclinic_project.user.model.comparators;
using polyclinic_project.user.service;

namespace polyclinic_project_tests.Tests.service
{
    public class TestIQueryServiceUtility
    {
        private IRepository<User> _repository = new UserRepository("path");

        [Fact]
        public void TestFindById_NotFound_ThrowsInvalidOperationException()
        {
            // Assert
            Assert.Throws<InvalidOperationException>(() => IQueryServiceUtility<User>.FindById(_repository, 1));
        }

        [Fact]
        public void TestFindById_ItemFound_ReturnsItem()
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
            User found = IQueryServiceUtility<User>.FindById(_repository, user.GetId());

            // Assert
            Assert.Equal(user, found, new UserEqualityComparer());
            _repository.GetList().Remove(user);
        }

        [Fact]
        public void TestFindById_MultipleUsers_ReturnsCorrectUser()
        {
            // Arrange
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
            User found = IQueryServiceUtility<User>.FindById(_repository, user.GetId());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(user, found, new UserEqualityComparer());
            Assert.NotEqual(another, found, new UserEqualityComparer());
            _repository.GetList().Remove(user);
            _repository.GetList().Remove(another);
        }

        [Fact]
        public void TestGetCount_ReturnsCorrectCount()
        {
            // Arrange
            User user1 = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            User user2 = IUserBuilder.BuildUser()
                .Id(2)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            User user3 = IUserBuilder.BuildUser()
                .Id(3)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            _repository.GetList().Add(user1);
            _repository.GetList().Add(user2);
            _repository.GetList().Add(user3);

            // Act
            int count = IQueryServiceUtility<User>.GetCount(_repository);

            // Assert
            Assert.Equal(3, count);
            _repository.GetList().Remove(user1);
            _repository.GetList().Remove(user2);
            _repository.GetList().Remove(user3);
        }
    }
}
