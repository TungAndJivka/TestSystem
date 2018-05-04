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
    public class CategoryServiceTests
    {
        Mock<IEfGenericRepository<Category>> categoryRepoMock;
        Mock<IMappingProvider> mapperMock;
        Mock<ISaver> saverMock;
        Mock<IRandomProvider> randomMock;
        CategoryService categoryService;

        [TestInitialize]
        public void TestInitialize()
        {
            categoryRepoMock = new Mock<IEfGenericRepository<Category>>();
            mapperMock = new Mock<IMappingProvider>();
            saverMock = new Mock<ISaver>();
            randomMock = new Mock<IRandomProvider>();

            categoryService = new CategoryService(categoryRepoMock.Object, mapperMock.Object, saverMock.Object, randomMock.Object);
        }

// GetAll() TESTS:

        [TestMethod]
        public void GetAllMethod_Should_Call_TestRepo_All()
        {
            // Arrange
            var all = new List<Category>() { new Category() { Name = "Java" }, new Category() { Name = "JavaScript" }, };
            var allDto = new List<CategoryDto>() { new CategoryDto(), new CategoryDto() };
            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var tests = categoryService.GetAll();

            // Assert
            categoryRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetAllMethod_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            var all = new List<Category>() { new Category() { Name = "Java"}, new Category() { Name = "JavaScript" }, };
            var allDto = new List<CategoryDto>() { new CategoryDto(), new CategoryDto() };
            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var tests = categoryService.GetAll();

            // Assert
            mapperMock.Verify(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>()), Times.Once);
        }

        [TestMethod]
        public void GetAllMethod_Should_Return_Correct()
        {
            // Arrange
            var all = new List<Category>() { new Category() { Name = "Java" }, new Category() { Name = "JavaScript" }, };
            var allDto = new List<CategoryDto>() { new CategoryDto(), new CategoryDto() };
            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var tests = categoryService.GetAll();

            // Assert
            Assert.AreEqual(2, tests.Count());
        }


// GetAllWithPublishedTests() TESTS:

        [TestMethod]
        public void GetAllWithPublsihedTests_Should_Call_TestRepo_All()
        {
            // Arrange
            var category1 = new Category();
            var category2 = new Category();
            var test1 = new Test() { IsPusblished = true,  Category = category1 };
            var test2 = new Test() { IsPusblished = true,  Category = category1 };
            var test3 = new Test() { IsPusblished = false,  Category = category2 };
            var all = new List<Category>() { category1, category2 };

            var catDto1 = new CategoryDto();
            var testDto1 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var testDto2 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var allDto = new List<CategoryDto>() { catDto1 };

            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var cats = categoryService.GetAllWithPublsihedTests();

            // Assert
            categoryRepoMock.Verify(x => x.All, Times.Once);
        }

        [TestMethod]
        public void GetAllWithPublsihedTests_Should_Call_Mapper_ProjectTo()
        {
            // Arrange
            var category1 = new Category();
            var category2 = new Category();
            var test1 = new Test() { IsPusblished = true, Category = category1 };
            var test2 = new Test() { IsPusblished = true, Category = category1 };
            var test3 = new Test() { IsPusblished = false, Category = category2 };
            var all = new List<Category>() { category1, category2 };

            var catDto1 = new CategoryDto();
            var testDto1 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var testDto2 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var allDto = new List<CategoryDto>() { catDto1 };

            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var cats = categoryService.GetAllWithPublsihedTests();

            // Assert
            mapperMock.Verify(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>()), Times.Once);
        }

        [TestMethod]
        public void GetAllWithPublsihedTests_Should_Return_Correct()
        {
            // Arrange
            var category1 = new Category();
            var category2 = new Category();
            var test1 = new Test() { IsPusblished = true, Category = category1 };
            var test2 = new Test() { IsPusblished = true, Category = category1 };
            var test3 = new Test() { IsPusblished = false, Category = category2 };
            var all = new List<Category>() { category1, category2 };

            var catDto1 = new CategoryDto();
            var testDto1 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var testDto2 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var allDto = new List<CategoryDto>() { catDto1 };

            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var cats = categoryService.GetAllWithPublsihedTests();

            // Assert
            Assert.AreEqual(1, cats.Count());
        }

        [TestMethod]
        public void GetAllWithPublsihedTests_Should_Filter_By_IsPublished()
        {
            // Arrange
            var category1 = new Category();
            var category2 = new Category();
            var test1 = new Test() { IsPusblished = true, Category = category1 };
            var test2 = new Test() { IsPusblished = true, Category = category1 };
            var test3 = new Test() { IsPusblished = false, Category = category2 };
            var all = new List<Category>() { category1, category2 };

            var catDto1 = new CategoryDto();
            var testDto1 = new TestDto() { IsPusblished = true, Category = catDto1 };
            var testDto2 = new TestDto() { IsPusblished = true, Category = catDto1 };
            catDto1.Tests = new List<TestDto>() { testDto1, testDto2 };
            var allDto = new List<CategoryDto>() { catDto1 };

            categoryRepoMock.Setup(x => x.All).Verifiable();
            categoryRepoMock.Setup(x => x.All).Returns(all.AsQueryable());
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Verifiable();
            mapperMock.Setup(x => x.ProjectTo<CategoryDto>(It.IsAny<IQueryable<Category>>())).Returns(allDto.AsQueryable());

            // Act
            var cats = categoryService.GetAllWithPublsihedTests();

            // Assert
            Assert.AreEqual(2, cats.First().Tests.Count());
        }



// CONSTRUCTOR VALIDATIONS TESTS:

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Repo_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new CategoryService(null, mapperMock.Object, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Mapper_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new CategoryService(categoryRepoMock.Object, null, saverMock.Object, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Saver_Is_Null()
        {

            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new CategoryService(categoryRepoMock.Object, mapperMock.Object, null, randomMock.Object));
        }

        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new CategoryService(categoryRepoMock.Object, mapperMock.Object, saverMock.Object, null));
        }
    }
}
