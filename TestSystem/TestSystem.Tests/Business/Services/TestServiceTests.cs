using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Linq;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services;

namespace TestSystem.Tests.Business.Services
{
    [TestClass]
    public class TestServiceTests
    {
        Mock<IEfGenericRepository<Test>> testRepoMock;
        Mock<IEfGenericRepository<User>> userRepoMock;
        Mock<IEfGenericRepository<UserTest>> resultRepoMock;
        Mock<IMappingProvider>  mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        TestService testService;

        [TestInitialize]
        public void TestInitialize()
        {
            testRepoMock = new Mock<IEfGenericRepository<Test>>();
            userRepoMock = new Mock<IEfGenericRepository<User>>();
            resultRepoMock = new Mock<IEfGenericRepository<UserTest>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            testService = new TestService(testRepoMock.Object, userRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object, resultRepoMock.Object);
        }


        // GetAll() TESTS:
        [TestMethod]
        public void GetAllMethod_Should_Call_TestRepo_All()
        {
            // Arrange
            testRepoMock.Setup(x => x.All).Verifiable();           
          
            // Act
            var tests = testService.GetAll();

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetAllMethod_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();

            // Act
            var tests = testService.GetAll();

            // Assert
            mapperMock.Verify(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>()), Times.Once);
        }

        // CONSTRUCTOR VALIDATIONS TESTS:
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_TestRepo_Is_Null()
        {
            // Arrange
            var userRepoMock = new Mock<IEfGenericRepository<User>>();
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var randomMock = new Mock<IRandomProvider>();
            var resultRepoMock = new Mock<IEfGenericRepository<UserTest>>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestService(null, userRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object, resultRepoMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_UserRepo_Is_Null()
        {
            // Arrange
            var testRepoMock = new Mock<IEfGenericRepository<Test>>();
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();
            var randomMock = new Mock<IRandomProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestService(testRepoMock.Object, null, mapperMock.Object, saverMock.Object, randomMock.Object, resultRepoMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            // Arrange
            var testRepoMock = new Mock<IEfGenericRepository<Test>>();
            var userRepoMock = new Mock<IEfGenericRepository<User>>();
            var saverMock = new Mock<ISaver>();
            var randomMock = new Mock<IRandomProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestService(testRepoMock.Object, userRepoMock.Object, null, saverMock.Object, randomMock.Object, resultRepoMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {
            // Arrange
            var testRepoMock = new Mock<IEfGenericRepository<Test>>();
            var userRepoMock = new Mock<IEfGenericRepository<User>>();
            var mapperMock = new Mock<IMappingProvider>();
            var randomMock = new Mock<IRandomProvider>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestService(testRepoMock.Object, userRepoMock.Object, mapperMock.Object, null, randomMock.Object, resultRepoMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            // Arrange
            var testRepoMock = new Mock<IEfGenericRepository<Test>>();
            var userRepoMock = new Mock<IEfGenericRepository<User>>();
            var mapperMock = new Mock<IMappingProvider>();
            var saverMock = new Mock<ISaver>();

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => new TestService(testRepoMock.Object, userRepoMock.Object, mapperMock.Object, saverMock.Object, null, resultRepoMock.Object));
        }


    }
}
