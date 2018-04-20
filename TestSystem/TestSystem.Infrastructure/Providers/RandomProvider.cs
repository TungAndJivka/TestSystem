using System;

namespace TestSystem.Infrastructure.Providers
{
    public class RandomProvider : IRandomProvider
    {
        private readonly Random random;

        public RandomProvider(Random random)
        {
            this.random = random;
        }

        public int Next(int min, int max)
        {
            return this.random.Next(min, max);
        }
    }
}
