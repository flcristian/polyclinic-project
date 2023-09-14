using polyclinic_project.system.interfaces;
using polyclinic_project.user.model.interfaces;
using polyclinic_project.user.model;
using polyclinic_project.user.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace polyclinic_project_tests.Tests
{
    public class TestIQueryServiceUtility
    {
        [Fact]
        public void TestFindById_NotFound_ReturnsNull()
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
            User found = IQueryServiceUtility<User>.FindById(repository, user.GetId());

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void TestFindById_UserFound_ReturnsUser()
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
            User found = IQueryServiceUtility<User>.FindById(repository, user.GetId());

            // Assert
            Assert.Equal(user, found);
        }

        [Fact]
        public void TestGetCount_ReturnsCorrectCount()
        {
            // Arrange
            IRepository<User> repository = new UserRepository("path");
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
            repository.GetList().Add(user1);
            repository.GetList().Add(user2);
            repository.GetList().Add(user3);

            // Act
            int count = IQueryServiceUtility<User>.GetCount(repository);

            // Assert
            Assert.Equal(3, count);
        }
    }
}
