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

namespace polyclinic_project_tests.Tests
{
    public class TestUserQueryService
    {
        [Fact]
        public void TestFindByEmail_NotFound_ReturnsNull()
        {
            // Arrange
            UserQueryService query = new UserQueryService();
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            User found = query.FindByEmail(user.GetEmail());

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void TestFindByEmail_UserFound_ReturnsUser()
        {
            // Arrange
            UserQueryService query = UserQueryServiceSingleton.Instance;
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            IRepository<User> repository = UserRepositorySingleton.Instance;
            repository.GetList().Add(user);

            // Act
            User found = query.FindByEmail(user.GetEmail());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(found, user);
            repository.GetList().Remove(user);
        }

        [Fact]
        public void TestFindByPhone_NotFound_ReturnsNull()
        {
            // Arrange
            UserQueryService query = new UserQueryService();
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);

            // Act
            User found = query.FindByPhone(user.GetPhone());

            // Assert
            Assert.Null(found);
        }

        [Fact]
        public void TestFindByPhone_UserFound_ReturnsUser()
        {
            // Arrange
            UserQueryService query = UserQueryServiceSingleton.Instance;
            User user = IUserBuilder.BuildUser()
                .Id(1)
                .Name("User")
                .Email("Email")
                .Phone("Phone")
                .Type(UserType.NONE);
            IRepository<User> repository = UserRepositorySingleton.Instance;
            repository.GetList().Add(user);

            // Act
            User found = query.FindByPhone(user.GetPhone());

            // Assert
            Assert.NotNull(found);
            Assert.Equal(found, user);
            repository.GetList().Remove(user);
        }
    }
}
