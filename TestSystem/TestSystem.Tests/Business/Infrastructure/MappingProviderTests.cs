using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;

namespace TestSystem.Tests.Business.Infrastructure
{
    [TestClass]
    public class MappingProviderTests
    {
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new MappingProvider(null));
        }

        [TestMethod]
        public void MapTo_Should_Call_Mapper_Map()
        {
            // Arrange
            var user = new User();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<UserDto>(It.IsAny<User>())).Verifiable();

            var mappingProvider = new MappingProvider(mapperMock.Object);

            // Act 
            var actual = mappingProvider.MapTo<UserDto>(user);

            // Asser
            mapperMock.Verify(x => x.Map<UserDto>(user), Times.Once);
        }
    }
}
