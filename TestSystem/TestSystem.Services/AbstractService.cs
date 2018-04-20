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
        private readonly IRandomProvider random;

        public AbstractService(IMappingProvider mapper, ISaver saver, IRandomProvider random)
        {
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(saver, "saver").IsNull().Throw();
            this.mapper = mapper;
            this.saver = saver;
            this.random = random;
        }

        protected IMappingProvider Mapper { get => this.mapper; }
        protected ISaver Saver { get => this.saver; }
        protected IRandomProvider Random { get => this.random; }

    }
}
