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
    public class QuestionServiceTests
    {
        Mock<IEfGenericRepository<Question>> questionRepoMock;
        Mock<IMappingProvider> mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        QuestionService questionService;

        [TestInitialize]
        public void TestInitialize()
        {
            questionRepoMock = new Mock<IEfGenericRepository<Question>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            questionService = new QuestionService(questionRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object);
        }

        // GetQuestionByTestId TESTS:
        [TestMethod]
        public void GetQuestionByTestId_Should_Call_Repo_All()
        {
            // Arrage
            var id = Guid.NewGuid();
            var test = new Test() { Id = id };
            var answers1 = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var answers2 = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var question1 = new Question() { TestId = id, Answers = answers1};
            var question2 = new Question() { TestId = id, Answers = answers2 };
            var all = new List<Question>() { question1, question2 };
            var allDto = new List<QuestionDto>() { new QuestionDto(), new QuestionDto() };

            questionRepoMock.Setup(x => x.All).Verifiable();
            questionRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>())).Returns(allDto.AsQueryable());

            //Act
            var result = questionService.GetAllQuestionsByTestId(id.ToString());

            // Assert
            questionRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetQuestionByTestId_Should_Call_Mapper_ProjectTo()
        {
            // Arrage
            var id = Guid.NewGuid();
            var test = new Test() { Id = id };
            var answers1 = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var answers2 = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var question1 = new Question() { TestId = id, Answers = answers1 };
            var question2 = new Question() { TestId = id, Answers = answers2 };
            var all = new List<Question>() { question1, question2 };
            var allDto = new List<QuestionDto>() { new QuestionDto(), new QuestionDto() };

            questionRepoMock.Setup(x => x.All).Verifiable();
            questionRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>())).Returns(allDto.AsQueryable());

            //Act
            var result = questionService.GetAllQuestionsByTestId(id.ToString());

            // Assert
            mapperMock.Verify(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>()), Times.Once);
        }

        [TestMethod]
        public void GetQuestionByTestId_Should_Return_Correct()
        {
            // Arrage
            var id = Guid.NewGuid();
            var test = new Test() { Id = id };
            var answers1 = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var answers2 = new List<Answer>() { new Answer(), new Answer(), new Answer() };
            var question1 = new Question() { TestId = id, Answers = answers1 };
            var question2 = new Question() { TestId = id, Answers = answers2 };
            var all = new List<Question>() { question1, question2 };
            var allDto = new List<QuestionDto>() { new QuestionDto(), new QuestionDto() };

            questionRepoMock.Setup(x => x.All).Verifiable();
            questionRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<QuestionDto>(It.IsAny<IQueryable<Question>>())).Returns(allDto.AsQueryable());

            //Act
            var result = questionService.GetAllQuestionsByTestId(id.ToString());

            // Assert
            Assert.AreEqual(2, result.Count());
        }


        // CONSTRUCTOR VALIDATIONS TESTS:
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Repo_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new QuestionService(null, mapperMock.Object, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new QuestionService(questionRepoMock.Object, null, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new QuestionService(questionRepoMock.Object, mapperMock.Object, null, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new QuestionService(questionRepoMock.Object, mapperMock.Object, saverMock.Object, null));
        }
    }
}
