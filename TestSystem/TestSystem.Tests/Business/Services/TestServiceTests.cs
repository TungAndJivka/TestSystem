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
using TestSystem.Services.Contracts;

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
        Mock<IQuestionService> questionServiceMock;
        TestService testService;

        [TestInitialize]
        public void TestInitialize()
        {
            testRepoMock = new Mock<IEfGenericRepository<Test>>();
            categoryRepoMock = new Mock<IEfGenericRepository<Category>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();
            questionServiceMock = new Mock<IQuestionService>();

            testService = new TestService(testRepoMock.Object, categoryRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object, questionServiceMock.Object);

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
        public void GetRandomTest_Should_Throw_ArgumentNullException_When_Category_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.GetRandomTestByCategory(null));
        }

        [TestMethod]
        public void GetRandomTest_Should_Throw_ArgumentNullException_When_Category_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.GetRandomTestByCategory(""));
        }

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
        public void GetFullTestInfo_Should_Throw_ArgumentNullException_When_TestId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.GetFullTestInfo(null));
        }

        [TestMethod]
        public void GetFullTestInfo_Should_Throw_ArgumentNullException_When_TestId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.GetFullTestInfo(""));
        }

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
        public void GetQuestionsCount_Should_Throw_ArgumentNullException_When_TestId_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.GetQuestionsCount(null));
        }

        [TestMethod]
        public void GetQuestionsCount_Should_Throw_ArgumentNullException_When_TestId_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.GetQuestionsCount(""));
        }

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



// AllTestsForDashboard() TESTS:

        [TestMethod]
        public void AllTestsForDashboard_Should_Call_TestRepo_All()
        {
            // Arrange
            testRepoMock.Setup(x => x.All).Verifiable();

            // Act
            var tests = testService.AllTestsForDashBoard();

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void AllTestsForDashboard_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            mapperMock.Setup(x => x.EnumerableProjectTo<Test, ExistingTestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();

            // Act
            var tests = testService.AllTestsForDashBoard();

            // Assert
            mapperMock.Verify(x => x.EnumerableProjectTo<Test, ExistingTestDto>(It.IsAny<IQueryable<Test>>()), Times.Once);
        }

        [TestMethod]
        public void AllTestsForDashboard_Should_Return_Correct()
        {
            // Arrange
            var all = new List<Test>() { new Test(), new Test() };
            var allDto = new List<ExistingTestDto>() { new ExistingTestDto(), new ExistingTestDto() };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.EnumerableProjectTo<Test, ExistingTestDto>(It.IsAny<IQueryable<Test>>())).Verifiable();
            mapperMock.Setup(x => x.EnumerableProjectTo<Test, ExistingTestDto>(It.IsAny<IQueryable<Test>>())).Returns(allDto);

            // Act
            var tests = testService.AllTestsForDashBoard().ToList();

            // Assert
            Assert.AreEqual(2, tests.Count());
        }


// CreateTest() TESTS:

        [TestMethod]
        public void CreateTest_Should_Throw_ArgumentNullException_When_Dto_Is_Null()
        {
            // Arrange, Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() => testService.CreateTest(null));
        }

        [TestMethod]
        public void CreateTest_Should_Call_TestRepo_Add()
        {
            // Arrange
            var dbQuestions = new List<Question>() { new Question(), new Question(), new Question()};
            var questions = new List<AdministerQuestionDto>() { new AdministerQuestionDto(), new AdministerQuestionDto(), new AdministerQuestionDto() };

            var testDto = new AdministerTestDto()
            {
                Category = "Java",
                TestName = "Some Test",
                Duration = 20,
                IsPusblished = false,
                Questions = questions
            };

            var allCategories = new List<Category>() { new Category { Id = Guid.NewGuid(), Name = "Java"} };

            categoryRepoMock.Setup(x => x.All).Returns(allCategories.AsQueryable());
            testRepoMock.Setup(x => x.Add(It.IsAny<Test>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            mapperMock
                .Setup(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()))
                .Verifiable();

            mapperMock
                .Setup(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()))
                .Returns(dbQuestions);

            // Act 
            testService.CreateTest(testDto);

            // Assert
            testRepoMock.Verify(x => x.Add(It.IsAny<Test>()), Times.Once());
        }

        [TestMethod]
        public void CreateTest_Should_Call_Mapper_Enumerable_ProjectTo_ManyTimes()
        {
            // Arrange
            var dbQuestions = new List<Question>() { new Question(), new Question(), new Question() };
            var questions = new List<AdministerQuestionDto>() { new AdministerQuestionDto(), new AdministerQuestionDto(), new AdministerQuestionDto() };

            var testDto = new AdministerTestDto()
            {
                Category = "Java",
                TestName = "Some Test",
                Duration = 20,
                IsPusblished = false,
                Questions = questions
            };

            var allCategories = new List<Category>() { new Category { Id = Guid.NewGuid(), Name = "Java" } };

            categoryRepoMock.Setup(x => x.All).Returns(allCategories.AsQueryable());
            testRepoMock.Setup(x => x.Add(It.IsAny<Test>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            mapperMock
                .Setup(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()))
                .Verifiable();

            mapperMock
                .Setup(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()))
                .Returns(dbQuestions);

            // Act 
            testService.CreateTest(testDto);

            // Assert
            mapperMock.Verify(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()), Times.AtLeastOnce());
        }

        [TestMethod]
        public void CreateTest_Should_Call_Saver_SaveChanges()
        {
            // Arrange
            var dbQuestions = new List<Question>() { new Question(), new Question(), new Question() };
            var questions = new List<AdministerQuestionDto>() { new AdministerQuestionDto(), new AdministerQuestionDto(), new AdministerQuestionDto() };

            var testDto = new AdministerTestDto()
            {
                Category = "Java",
                TestName = "Some Test",
                Duration = 20,
                IsPusblished = false,
                Questions = questions
            };

            var allCategories = new List<Category>() { new Category { Id = Guid.NewGuid(), Name = "Java" } };

            categoryRepoMock.Setup(x => x.All).Returns(allCategories.AsQueryable());
            testRepoMock.Setup(x => x.Add(It.IsAny<Test>())).Verifiable();
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            mapperMock
                .Setup(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()))
                .Verifiable();

            mapperMock
                .Setup(x => x.EnumerableProjectTo<AdministerQuestionDto, Question>(It.IsAny<ICollection<AdministerQuestionDto>>()))
                .Returns(dbQuestions);

            // Act 
            testService.CreateTest(testDto);

            // Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once());
        }



// GetTest() TESTS:

        [TestMethod]
        public void GetTest_Should_Throw_ArgumentNullException_When_TestName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.GetTest(null, "Java"));
        }

        [TestMethod]
        public void GetTest_Should_Throw_ArgumentNullException_When_TestName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.GetTest("", "Java"));
        }

        [TestMethod]
        public void GetTest_Should_Throw_ArgumentNullException_When_CategoryName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.GetTest("Test1", null));
        }

        [TestMethod]
        public void GetTest_Should_Throw_ArgumentNullException_When_CategoryName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.GetTest("Test1", ""));
        }

        [TestMethod]
        public void GetTest_Should_Return_Correct()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java"};
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1"};
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            AdministerTestDto testDto = new AdministerTestDto();
            mapperMock.Setup(x => x.MapTo<AdministerTestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<AdministerTestDto>(test)).Returns(testDto);

            // Act
            var result = testService.GetTest("Test1", "Java");

            // Assert
            Assert.AreEqual(testDto, result);
        }

        [TestMethod]
        public void GetTest_Should_Call_Repo()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            AdministerTestDto testDto = new AdministerTestDto();
            mapperMock.Setup(x => x.MapTo<AdministerTestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<AdministerTestDto>(test)).Returns(testDto);

            // Act
            var result = testService.GetTest("Test1", "Java");

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetTest_Should_Call_Mapper()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            AdministerTestDto testDto = new AdministerTestDto();
            mapperMock.Setup(x => x.MapTo<AdministerTestDto>(test)).Verifiable();
            mapperMock.Setup(x => x.MapTo<AdministerTestDto>(test)).Returns(testDto);

            // Act
            var result = testService.GetTest("Test1", "Java");

            // Assert
            mapperMock.Verify(x => x.MapTo<AdministerTestDto>(test), Times.Once);
        }



// PublishTest() TESTS:

        [TestMethod]
        public void PublishTest_Should_Throw_ArgumentNullException_When_TestName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.PublishTest(null));
        }

        [TestMethod]
        public void PublishTest_Should_Throw_ArgumentNullException_When_TestName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.PublishTest(""));
        }

        [TestMethod]
        public void PublishTest_Should_Call_Repo_All()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            testService.PublishTest(testId.ToString());

            // Assert
            testRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void PublishTest_Should_Return_False_When_Questions_Count_Is_0()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var questions = new List<Question>();
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            bool result = testService.PublishTest(testId.ToString());

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void PublishTest_Should_Return_True_When_Questions_Count_Is_More_Than_0()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            bool result = testService.PublishTest(testId.ToString());

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PublishTest_Should_Call_Repo_Update()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            bool result = testService.PublishTest(testId.ToString());

            // Assert
            testRepoMock.Verify(x => x.Update(It.IsAny<Test>()), Times.Once);
        }

        [TestMethod]
        public void PublishTest_Should_Set_IsPublished_True()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            bool result = testService.PublishTest(testId.ToString());

            // Assert
            Assert.IsTrue(test.IsPusblished == true);
        }

        [TestMethod]
        public void PublishTest_Should_Call_Saver_SaveChanges()
        {
            // Arrange
            var testId = Guid.NewGuid();
            var category = new Category() { Name = "Java" };
            var answers = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var questions = new List<Question>() { new Question() { Answers = answers } };
            var test = new Test() { Id = testId, Questions = questions, Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };
            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            bool result = testService.PublishTest(testId.ToString());

            // Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }



// DeleteTest() TESTS:

        [TestMethod]
        public void DeleteTest_Should_Throw_ArgumentNullException_When_TestName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.DeleteTest(null, "Java"));
        }

        [TestMethod]
        public void DeleteTest_Should_Throw_ArgumentNullException_When_TestName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.DeleteTest("", "Java"));
        }

        [TestMethod]
        public void DeleteTest_Should_Throw_ArgumentNullException_When_CategoryName_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => testService.DeleteTest("Test1", null));
        }

        [TestMethod]
        public void DeleteTest_Should_Throw_ArgumentNullException_When_CategoryName_Is_Empty()
        {
            Assert.ThrowsException<ArgumentException>(() => testService.DeleteTest("Test1", ""));
        }

        [TestMethod]
        public void DeleteTest_Should_Call_Repo_All()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var test = new Test() { Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };

            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            testService.DeleteTest("Test1", "Java");

            // Assert
        }

        [TestMethod]
        public void DeleteTest_Should_Set_IsDeleted_True()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var test = new Test() { Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };

            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            testService.DeleteTest("Test1", "Java");

            // Assert
            Assert.IsTrue(test.IsDeleted == true);
        }

        [TestMethod]
        public void DeleteTest_Should_Call_Repo_Update()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var test = new Test() { Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };

            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            testService.DeleteTest("Test1", "Java");

            // Assert
            testRepoMock.Verify(x => x.Update(It.IsAny<Test>()), Times.Once);
        }

        [TestMethod]
        public void DeleteTest_Should_Call_Saver_SaveChanges()
        {
            // Arrange
            var category = new Category() { Name = "Java" };
            var test = new Test() { Category = category, TestName = "Test1" };
            var all = new List<Test>() { test };

            testRepoMock.Setup(x => x.All).Verifiable();
            testRepoMock.Setup(x => x.Update(It.IsAny<Test>())).Verifiable();
            testRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            saverMock.Setup(x => x.SaveChanges()).Verifiable();

            // Act
            testService.DeleteTest("Test1", "Java");

            // Assert
            saverMock.Verify(x => x.SaveChanges(), Times.Once);
        }



// CONSTRUCTOR VALIDATIONS TESTS:

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_TestRepo_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(null, categoryRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object, questionServiceMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(testRepoMock.Object, categoryRepoMock.Object, null, saverMock.Object, randomMock.Object, questionServiceMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(testRepoMock.Object, categoryRepoMock.Object, mapperMock.Object, null, randomMock.Object, questionServiceMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() 
                => new TestService(testRepoMock.Object, categoryRepoMock.Object, mapperMock.Object, saverMock.Object, null, questionServiceMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_CategoryRepo_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new TestService(testRepoMock.Object, null, mapperMock.Object, saverMock.Object, randomMock.Object, questionServiceMock.Object));
        }

    }
}
