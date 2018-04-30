using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services;

namespace TestSystem.Tests.Business.Services
{
    [TestClass]
    public class UserServiceTests
    {
        Mock<IEfGenericRepository<User>> userRepoMock;
        Mock<IMappingProvider> mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        UserService userService;

        [TestInitialize]
        public void TestInitialize()
        {
            userRepoMock = new Mock<IEfGenericRepository<User>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            userService = new UserService(userRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object);
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Repo_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new UserService(null, mapperMock.Object, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new UserService(userRepoMock.Object, null, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new UserService(userRepoMock.Object, mapperMock.Object, null, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new UserService(userRepoMock.Object, mapperMock.Object, saverMock.Object, null));
        }
    }
}
