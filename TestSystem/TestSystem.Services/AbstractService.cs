using Bytes2you.Validation;
using System;
using TestSystem.Data.Data.Saver;
using TestSystem.Infrastructure.Providers;

namespace TestSystem.Services
{
    public abstract class AbstractService
    {
        private readonly IMappingProvider mapper;
        private readonly ISaver saver;
        private readonly Random random;

        public AbstractService(IMappingProvider mapper, ISaver saver)
        {
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(saver, "saver").IsNull().Throw();
            this.mapper = mapper;
            this.saver = saver;
            this.random = new Random(); // TODO wrap the random
        }

        protected IMappingProvider Mapper { get; }
        protected ISaver Saver { get; }
        protected Random Random { get; }

    }
}
