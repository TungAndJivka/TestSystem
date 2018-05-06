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
    public class AnswerServiceTests
    {
        Mock<IEfGenericRepository<Answer>> answerRepoMock;
        Mock<IMappingProvider> mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        AnswerService answerService;

        [TestInitialize]
        public void TestInitialize()
        {
            answerRepoMock = new Mock<IEfGenericRepository<Answer>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            answerService = new AnswerService(answerRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object);
        }

        // GetByID method TESTS:
        [TestMethod]
        public void GetByID_Method_Should_Throw_ArgumentNullException_When_Id_Is_Null()
        {
            Assert.ThrowsException<ArgumentNullException>(() => answerService.GetById(null));
        }

        [TestMethod]
        public void GetByID_Method_Should_Throw_ArgumentException_When_Id_Is_Null()
        {
            Assert.ThrowsException<ArgumentException>(() => answerService.GetById(""));
        }

        [TestMethod]
        public void GetByID_Method_Should_Call_Repo_All()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new Answer();
            var answerDto = new AnswerDto();
            entity.Id = id;

            var all = new List<Answer>() { entity };

            mapperMock.Setup(x => x.MapTo<AnswerDto>(It.IsAny<Answer>())).Returns(answerDto);
            answerRepoMock.Setup(x => x.All).Verifiable();
            answerRepoMock.Setup(x => x.All).Returns(all.AsQueryable());

            // Act
            var answer = answerService.GetById(id.ToString());

            // Assert
            answerRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetByID_Method_Should_Call_Mapper_MapTo()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new Answer();
            var answerDto = new AnswerDto();
            entity.Id = id;

            var all = new List<Answer>() { entity };

            answerRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.MapTo<AnswerDto>(It.IsAny<Answer>())).Verifiable();
            mapperMock.Setup(x => x.MapTo<AnswerDto>(It.IsAny<Answer>())).Returns(answerDto);

            // Act
            var answer = answerService.GetById(id.ToString());

            // Assert
            mapperMock.Verify(x => x.MapTo<AnswerDto>(It.IsAny<Answer>()), Times.Once);
        }


        [TestMethod]
        public void GetByID_Method_Should_Return_Correct()
        {
            // Arrange
            var id = Guid.NewGuid();
            var entity = new Answer();
            var answerDto = new AnswerDto();
            entity.Id = id;

            var all = new List<Answer>() { entity };

            answerRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.MapTo<AnswerDto>(It.IsAny<Answer>())).Returns(answerDto);

            // Act
            var answer = answerService.GetById(id.ToString());

            // Assert
            Assert.AreEqual(answerDto, answer);
        }


        // CONSTRUCTOR VALIDATIONS TESTS:
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Repo_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new AnswerService(null, mapperMock.Object, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new AnswerService(answerRepoMock.Object, null, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new AnswerService(answerRepoMock.Object, mapperMock.Object, null, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new AnswerService(answerRepoMock.Object, mapperMock.Object, saverMock.Object, null));
        }
    }
}
