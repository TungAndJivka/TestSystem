using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using TestSystem.Infrastructure.Providers;

namespace TestSystem.Tests.Business.Infrastructure
{
    [TestClass]
    public class RandomProviderTests
    {
        [TestMethod]
        public void Constructor_Should_Throw_ArgumentNullException_When_Random_Is_Null()
        {
            //Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(()
                => new RandomProvider(null));
        }

        [TestMethod]
        public void Next_Should_Return_Exclusive_Max()
        {
            // Arrange
            var randomProvider = new RandomProvider(new Random());
            int min = 1;
            int max = 2;

            // Act 
            int num = randomProvider.Next(min, max);

            // Asser
            Assert.IsTrue(num < max);
        }

        [TestMethod]
        public void Next_Should_Return_Inclusive_Min()
        {
            // Arrange
            var randomProvider = new RandomProvider(new Random());
            int min = 1;
            int max = 2;

            // Act 
            int num = randomProvider.Next(min, max);

            // Asser
            Assert.IsTrue(num >= min);
        }
    }
}
