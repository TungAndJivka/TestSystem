using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
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
        Mock<IEfGenericRepository<Category>> categoryRepoMock;
        Mock<IMappingProvider>  mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        TestService testService;

        [TestInitialize]
        public void TestInitialize()
        {
            testRepoMock = new Mock<IEfGenericRepository<Test>>();
            categoryRepoMock = new Mock<IEfGenericRepository<Category>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            testService = new TestService(testRepoMock.Object, categoryRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object);

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

        [TestMethod]
        public void GetAllMethod_Should_Return_Correct()
        {
            // Arrange
            var all = new List<Test>() { new Test(), new Test()};
            var allDto = new List<TestDto>() { new TestDto(), new TestDto() };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(allDto.AsQueryable());

            // Act
            var tests = testService.GetAll().ToList();

            // Assert
            Assert.AreEqual(2, tests.Count());
        }

        // GetRandomTest() TESTS:
        [TestMethod]
        public void GetRandomTest_Should_Call_Repo_All()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var result = testService.GetRandomTestByCategory("Java");

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetRandomTest_Should_Call_Random_Next()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var result = testService.GetRandomTestByCategory("Java");

            // Assert
            randomMock.Verify(x => x.Next(0, 1), Times.Once);
        }

        [TestMethod]
        public void GetRandomTest_Should_Call_Mapper_MapTo()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var result = testService.GetRandomTestByCategory("Java");

            // Assert
            mapperMock.Verify(x => x.MapTo<TestDto>(test), Times.Once);
        }

        [TestMethod]
        public void GetRandomTest_Should_Return_Correct()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var result = testService.GetRandomTestByCategory("Java");

            // Assert
            Assert.AreEqual(testDto, result);
        }

        // GetFullTestInfo() TESTS:
        [TestMethod]
        public void GetFullTestInfo_Should_Return_Correct()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);

            // Act
            var result = testService.GetFullTestInfo(testId.ToString());

            // Assert
            Assert.AreEqual(testDto, result);
        }

        [TestMethod]
        public void GetFullTestInfo_Should_Call_Repo()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);

            // Act
            var result = testService.GetFullTestInfo(testId.ToString());

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetFullTestInfo_Should_Call_Mapper()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);

            // Act
            var result = testService.GetFullTestInfo(testId.ToString());

            // Assert
            mapperMock.Verify(x => x.MapTo<TestDto>(test), Times.Once);
        }

        // GetQuestionsCount() TESTS:
        [TestMethod]
        public void GetQuestionsCount_Should_Return_Correct()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var questions = new List<Question>() { new Question() };
            var test = new Test() { Id = testId , Questions = questions };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            int count = testService.GetQuestionsCount(testId.ToString());

            // Assert
            Assert.AreEqual(1, count);
        }

        [TestMethod]
        public void GetQuestionsCount_Should_Return_Zero_When_Test_Not_Found()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var all = new List<Test>();
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            int count = testService.GetQuestionsCount(testId.ToString());

            // Assert
            Assert.AreEqual(0, count);
        }

        [TestMethod]
        public void GetQuestionsCount_Should_Call_Repo_All()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var questions = new List<Question>() { new Question() };
            var test = new Test() { Id = testId, Questions = questions };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            int count = testService.GetQuestionsCount(testId.ToString());

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        // CONSTRUCTOR VALIDATIONS TESTS:
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_TestRepo_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(null, categoryRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(testRepoMock.Object, categoryRepoMock.Object, null, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(testRepoMock.Object, categoryRepoMock.Object, mapperMock.Object, null, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(testRepoMock.Object, categoryRepoMock.Object, mapperMock.Object, saverMock.Object, null));
        }


    }
}
