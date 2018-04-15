using Bytes2you.Validation;
using TestSystem.Data.Data.Saver;
using TestSystem.Infrastructure.Providers;

namespace TestSystem.Services
{
    public abstract class AbstractService
    {
        private readonly IMappingProvider mapper;
        private readonly ISaver saver;

        public AbstractService(IMappingProvider mapper, ISaver saver)
        {
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            Guard.WhenArgument(saver, "saver").IsNull().Throw();
            this.mapper = mapper;
            this.saver = saver;
        }

        protected IMappingProvider Mapper { get; }
        protected ISaver Saver { get; set; }
    }
}
