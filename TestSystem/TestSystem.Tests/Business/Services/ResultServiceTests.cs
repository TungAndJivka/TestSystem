using Microsoft.EntityFrameworkCore.Query;
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
    public class ResultServiceTests
    {
        Mock<IEfGenericRepository<UserTest>> resultRepoMock;
        Mock<IMappingProvider> mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        ResultService resultService;

        [TestInitialize]
        public void TestInitialize()
        {
            resultRepoMock = new Mock<IEfGenericRepository<UserTest>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            resultService = new ResultService(resultRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object);
        }


// GetAll() TESTS:

        [TestMethod]
        public void GetAllMethod_Should_Call_ResultRepo_All()
        {
            // Arrange
            resultRepoMock.Setup(x => x.All).Verifiable();

            // Act
            var results = resultService.GetAll();

            // Assert
            resultRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetAllMethod_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            mapperMock.Setup(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Verifiable();

            // Act
            var results = resultService.GetAll();

            // Assert
            mapperMock.Verify(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>()), Times.Once);
        }

        [TestMethod]
        public void GetAllMethod_Should_Return_Correct()
        {
            // Arrange
            var all = new List<UserTest>() { new UserTest(), new UserTest() };
            var allDto = new List<UserTestDto>() { new UserTestDto(), new UserTestDto() };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetAll().ToList();

            // Assert
            Assert.AreEqual(2, results.Count());
        }


// Update() method TESTS:

        [TestMethod]
        public void Update_Should_Throw_ArgumentNullExcepton_When_Dto_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.Update(null));
        }

        [TestMethod]
        public void Update_Should_Call_Repo_Update()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id, Score = 0.0, SubmittedOn = DateTime.Now, AnsweredQuestions = new List<AnsweredQuestionDto>() };
            var all = new List<UserTest>() { entity };
            var aq = new List<AnsweredQuestion>();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<AnsweredQuestion>(It.IsAny<IQueryable<AnsweredQuestionDto>>())).Returns(aq.AsQueryable());

            resultRepoMock.Setup(x => x.Update(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.Update(result);

            // Assert
            resultRepoMock.Verify(x => x.Update(It.IsAny<UserTest>()), Times.Once);
        }

        [TestMethod]
        public void Update_Should_Call_Saver_SaveChanges()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id, Score = 0.0, SubmittedOn = DateTime.Now, AnsweredQuestions = new List<AnsweredQuestionDto>() };
            var all = new List<UserTest>() { entity };
            var aq = new List<AnsweredQuestion>();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<AnsweredQuestion>(It.IsAny<IQueryable<AnsweredQuestionDto>>())).Returns(aq.AsQueryable());

            resultRepoMock.Setup(x => x.Update(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.Update(result);

            // Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }

        [TestMethod]
        public void Update_Should_Set_Score()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id, Score = 50.0, SubmittedOn = DateTime.Now, AnsweredQuestions = new List<AnsweredQuestionDto>() };
            var all = new List<UserTest>() { entity };
            var aq = new List<AnsweredQuestion>();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<AnsweredQuestion>(It.IsAny<IQueryable<AnsweredQuestionDto>>())).Returns(aq.AsQueryable());

            resultRepoMock.Setup(x => x.Update(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.Update(result);

            // Assert
            Assert.AreEqual(50.0, result.Score);
        }

        [TestMethod]
        public void Update_Should_Set_SubmittedOn()
        {
            // Arrange
            var time = DateTime.Now;
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id, Score = 0.0, SubmittedOn = time, AnsweredQuestions = new List<AnsweredQuestionDto>() };
            var all = new List<UserTest>() { entity };
            var aq = new List<AnsweredQuestion>();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<AnsweredQuestion>(It.IsAny<IQueryable<AnsweredQuestionDto>>())).Returns(aq.AsQueryable());

            resultRepoMock.Setup(x => x.Update(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.Update(result);

            // Assert
            Assert.AreEqual(time, result.SubmittedOn);
        }

        [TestMethod]
        public void Update_Should_Set_AnsweredQuestions()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id, Score = 0.0, SubmittedOn = DateTime.Now, AnsweredQuestions = new List<AnsweredQuestionDto>() { new AnsweredQuestionDto() } };
            var all = new List<UserTest>() { entity };
            var aq = new List<AnsweredQuestion>() { new AnsweredQuestion() };
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<AnsweredQuestion>(It.IsAny<IQueryable<AnsweredQuestionDto>>())).Returns(aq.AsQueryable());

            resultRepoMock.Setup(x => x.Update(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.Update(result);

            // Assert
            Assert.AreEqual(1, result.AnsweredQuestions.Count);
        }



// AddResult() method TESTS:

        [TestMethod]
        public void AddResult_Should_Throw_ArgumentNullExcepton_When_Dto_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.AddResult(null));
        }

        [TestMethod]
        public void AddResult_Should_Call_Repo_Add()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id };
            var all = new List<UserTest>() { entity };

            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            resultRepoMock.Setup(x => x.Add(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.AddResult(result);

            // Assert
            resultRepoMock.Verify(x => x.Add(It.IsAny<UserTest>()), Times.Once);
        }

        [TestMethod]
        public void AddResult_Should_Call_Saver_SaveChanges()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new UserTest() { Id = id };
            var result = new UserTestDto() { Id = id };
            var all = new List<UserTest>() { entity };

            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            resultRepoMock.Setup(x => x.Add(It.IsAny<UserTest>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            resultService.AddResult(result);

            // Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }



// GetUserTest() TESTS:

        [TestMethod]
        public void GetUserTest_Should_Throw_ArgumentNullException_When_UserId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetUserTest(null, "testId"));
        }

        [TestMethod]
        public void GetUserTest_Should_Throw_ArgumentNullException_When_UserId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.GetUserTest("", "testId"));
        }

        [TestMethod]
        public void GetUserTest_Should_Throw_ArgumentNullException_When_TestId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetUserTest("userId", null));
        }

        [TestMethod]
        public void GetUserTest_Should_Throw_ArgumentNullException_When_TestId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.GetUserTest("userId", ""));
        }

        [TestMethod]
        public void GetUserTestsMethod_Should_Return_Correct_Tests()
        {
            // Arrange
            var result= new UserTest();
            var test = new Test();
            var category = new Category();
            var id = Guid.NewGuid();
            test.Category = category;
            result.UserId = id.ToString();
            result.Test = test;
            var resultDto = new TestDto();


            var all = new List<UserTest>() { result };
            var results = new List<TestDto>() { resultDto };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(results.AsQueryable());

            // Act
            var actual = resultService.GetUserResults(id.ToString());

            // Assert
            Assert.AreEqual(1, actual.Count());
        }

        [TestMethod]
        public void GetUserTestsMethod_Should_Return_Correct_TestsObject()
        {
            // Arrange
            var result = new UserTest();
            var test = new Test();
            test.TestName = "Test1";
            var category = new Category();
            var id = Guid.NewGuid();
            test.Category = category;
            result.UserId = id.ToString();
            result.Test = test;
            var resultDto = new TestDto();


            var all = new List<UserTest>() { result };
            var results = new List<TestDto>() { resultDto };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(results.AsQueryable());

            // Act
            var actual = resultService.GetUserResults(id.ToString());

            // Assert
            Assert.AreEqual(resultDto, actual.First());
        }

        [TestMethod]
        public void GetUserTestsMethod_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();

            // Act
            var tests = resultService.GetUserResults("some id");

            // Assert
            mapperMock.Verify(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>()), Times.Once);
        }

        [TestMethod]
        public void GetUserTestsMethod_Should_Call_ResultRepo_All()
        {
            // Arrange
            var result = new UserTest();
            var test = new Test();
            var category = new Category();
            var id = Guid.NewGuid();
            test.Category = category;

            result.Id = id;
            result.Test = test;

            var all = new List<UserTest>() { result };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            var tests = resultService.GetUserResults("some id");

            // Assert
            resultRepoMock.Verify(x => x.All, Times.Once);
        }



// CheckForTakenTest() method TESTS:

        [TestMethod]
        public void CheckForTakenTest_Should_Throw_ArgumentNullException_When_UserId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.CheckForTakenTest(null, "categoryName"));
        }

        [TestMethod]
        public void CheckForTakenTest_Throw_ArgumentNullException_When_UserId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.CheckForTakenTest("", "categoryName"));
        }

        [TestMethod]
        public void CheckForTakenTest_Throw_ArgumentNullException_When_CategoryName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.CheckForTakenTest("userId", null));
        }

        [TestMethod]
        public void CheckForTakenTest_Should_Throw_ArgumentNullException_When_CategoryName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.CheckForTakenTest("userId", ""));
        }

        [TestMethod]
        public void CheckForTakenTest_Should_Return_1_When_No_Test_Found()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var all = new List<UserTest>() {  };

            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            var actual = resultService.CheckForTakenTest(userId, "Java");

            // Assert
            Assert.AreEqual(StatusType.TestNotStarted, actual);
        }

        [TestMethod]
        public void CheckForTakenTest_Should_Return_2_When_Test_Found_But_Not_Submitted()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var user = new User() { Id = userId };
            var test = new Test() { Category = new Category() { Name = "Java" } };
            var result = new UserTest() { UserId = userId, Test = test, SubmittedOn = null };
            var all = new List<UserTest>() { result };

            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            var actual = resultService.CheckForTakenTest(userId, "Java");

            // Assert
            Assert.AreEqual(StatusType.TestNotSubmitted, actual);
        }

        [TestMethod]
        public void CheckForTakenTest_Should_Return_3_When_Test_Found_And_Submitted()
        {
            // Arrange
            var userId = Guid.NewGuid().ToString();
            var user = new User() { Id = userId };
            var test = new Test() { Category = new Category() { Name = "Java" } };
            var result = new UserTest() { UserId = userId, Test = test, SubmittedOn = DateTime.Now };
            var all = new List<UserTest>() { result };

            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            var actual = resultService.CheckForTakenTest(userId, "Java");

            // Assert
            Assert.AreEqual(StatusType.TestSubmitted, actual);
        }



// GetTestFromCategory() Tests:

        [TestMethod]
        public void GetTestFromCategory_Should_Throw_ArgumentNullException_When_UserId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetTestFromCategory(null, "categoryName"));
        }

        [TestMethod]
        public void GetTestFromCategory_Throw_ArgumentNullException_When_UserId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.GetTestFromCategory("", "categoryName"));
        }

        [TestMethod]
        public void GetTestFromCategory_Throw_ArgumentNullException_When_CategoryName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetTestFromCategory("userId", null));
        }

        [TestMethod]
        public void GetTestFromCategory_Should_Throw_ArgumentNullException_When_CategoryName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.GetTestFromCategory("userId", ""));
        }

        [TestMethod]
        public void GetTestFromCategory_Should_Call_Repo_All()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var user = new User() { Id = id };
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var userTest = new UserTest() { UserId = user.Id, Test = test };
            var all = new List<UserTest>() { userTest };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto() { TestName = "Test1"};

            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var actual = resultService.GetTestFromCategory(id ,"Java");

            // Assert
            resultRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetTestFromCategory_Should_Call_Mapper_MapTo()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var user = new User() { Id = id };
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var userTest = new UserTest() { UserId = user.Id, Test = test };
            var all = new List<UserTest>() { userTest };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto() { TestName = "Test1" };

            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var actual = resultService.GetTestFromCategory(id, "Java");

            // Assert
            mapperMock.Verify(x => x.MapTo<TestDto>(test), Times.Once);
        }

        [TestMethod]
        public void GetTestFromCategory_Should_Return_Correct()
        {
            // Arrange
            var id = Guid.NewGuid().ToString();
            var user = new User() { Id = id };
            var category = new Category() { Name = "Java" };
            var testId = Guid.NewGuid();
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Category = category, Questions = questions, IsPusblished = true };
            var userTest = new UserTest() { UserId = user.Id, Test = test };
            var all = new List<UserTest>() { userTest };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            TestDto testDto = new TestDto() { TestName = "Test1" };

            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<TestDto>(test)).Returns(testDto);
            randomMock.Setup(x => x.Next(It.IsAny<int>(), It.IsAny<int>())).Verifiable();

            // Act
            var actual = resultService.GetTestFromCategory(id, "Java");

            // Assert
            Assert.AreEqual(testDto, actual);
        }



// GetUserResults TESTS:

        [TestMethod]
        public void GetUserResults_Should_Throw_ArgumentNullException_When_UserId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => resultService.GetUserResults(null));
        }

        [TestMethod]
        public void GetUserResults_Should_Throw_ArgumentNullException_When_UserId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => resultService.GetUserResults(""));
        }

        [TestMethod]
        public void GetUserResults_Should_Return_Correct_Results()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var test1 = new Test();
            var test2 = new Test();
            var all = new List<UserTest>() { new UserTest() { UserId = userId.ToString(), Test = test1 }, new UserTest() { UserId = Guid.NewGuid().ToString(), Test = test2 } };
            var allDto = new List<TestDto>() { new TestDto() { TestName = "Test1"} };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetUserResults(userId.ToString()).ToList();

            // Assert
            Assert.AreEqual("Test1", results[0].TestName);
        }

        [TestMethod]
        public void GetUserResults_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var test1 = new Test();
            var test2 = new Test();
            var all = new List<UserTest>() { new UserTest() { UserId = userId.ToString(), Test = test1 }, new UserTest() { UserId = Guid.NewGuid().ToString(), Test = test2 } };
            var allDto = new List<TestDto>() { new TestDto() { TestName = "Test1" } };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetUserResults(userId.ToString()).ToList();

            // Assert
            mapperMock.Verify(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>()), Times.Once);
        }

        [TestMethod]
        public void GetUserResults_Should_Call_Repo_All()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var test1 = new Test();
            var test2 = new Test();
            var all = new List<UserTest>() { new UserTest() { UserId = userId.ToString(), Test = test1 }, new UserTest() { UserId = Guid.NewGuid().ToString(), Test = test2 } };
            var allDto = new List<TestDto>() { new TestDto() { TestName = "Test1" } };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetUserResults(userId.ToString()).ToList();

            // Assert
            resultRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetUserResults_Should_Return_Correct_ResultsCount()
        {
            // Arrange
            Guid userId = Guid.NewGuid();
            var test1 = new Test();
            var test2 = new Test();
            var all = new List<UserTest>() { new UserTest() { UserId = userId.ToString(), Test = test1 }, new UserTest() { UserId = Guid.NewGuid().ToString(), Test = test2 } };
            var allDto = new List<TestDto>() { new TestDto() { TestName = "Test1" } };
            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestDto>(It.IsAny<IQueryable<Test>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetUserResults(userId.ToString()).ToList();

            // Assert
            Assert.AreEqual(1, results.Count());
        }


// GetTestResultsForDashBoard() TESTS:

        [TestMethod]
        public void GetTestResultsForDashBoard_Should_Call_ResultRepo_All()
        {
            // Arrange
            var user1 = new User();
            var test1 = new Test() { Category = new Category() };

            var user2 = new User();
            var test2 = new Test() { Category = new Category() };

            var all = new List<UserTest>() { new UserTest() { User = user1, Test = test1 }, new UserTest() { User = user2, Test = test2 } };
            var allDto = new List<TestResultDto>() { new TestResultDto(), new TestResultDto() };

            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<UserTestDto>(It.IsAny<IQueryable<UserTest>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestResultDto>(It.IsAny<IQueryable<UserTest>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetTestResultsForDashBoard();

            // Assert
            resultRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetTestResultsForDashBoard_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            var user1 = new User();
            var test1 = new Test() { Category = new Category() };

            var user2 = new User();
            var test2 = new Test() { Category = new Category() };

            var all = new List<UserTest>() { new UserTest() { User = user1, Test = test1 }, new UserTest() { User = user2, Test = test2 } };
            var allDto = new List<TestResultDto>() { new TestResultDto(), new TestResultDto() };

            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<TestResultDto>(It.IsAny<IQueryable<UserTest>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestResultDto>(It.IsAny<IQueryable<UserTest>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetTestResultsForDashBoard();

            // Assert
            mapperMock.Verify(x => x.ProjectTo<TestResultDto>(It.IsAny<IQueryable<UserTest>>()), Times.Once);
        }

        [TestMethod]
        public void GetTestResultsForDashBoard_Should_Return_Correct()
        {
            // Arrange
            var user1 = new User();
            var test1 = new Test() { Category = new Category() };

            var user2 = new User();
            var test2 = new Test() { Category = new Category() };

            var all = new List<UserTest>() { new UserTest() { User = user1, Test = test1 }, new UserTest() { User = user2, Test = test2 } };
            var allDto = new List<TestResultDto>() { new TestResultDto(), new TestResultDto() };

            resultRepoMock.Setup(x => x.All).Verifiable();
            resultRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<UserTestDto>(It.IsAny<IIncludableQueryable<UserTest, Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<TestResultDto>(It.IsAny<IIncludableQueryable<UserTest, Category>>())).Returns(allDto.AsQueryable());

            // Act
            var results = resultService.GetTestResultsForDashBoard().ToList();

            // Assert
            Assert.AreEqual(2, results.Count());
        }



// CONSTRUCTOR VALIDATIONS TESTS:

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Repo_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new ResultService(null, mapperMock.Object, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new ResultService(resultRepoMock.Object, null, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new ResultService(resultRepoMock.Object, mapperMock.Object, null, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new ResultService(resultRepoMock.Object, mapperMock.Object, saverMock.Object, null));
        }
    }
}
