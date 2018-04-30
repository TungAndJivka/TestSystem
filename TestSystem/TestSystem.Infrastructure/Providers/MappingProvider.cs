using AutoMapper;
using AutoMapper.QueryableExtensions;
using Bytes2you.Validation;
using System.Collections.Generic;
using System.Linq;

namespace TestSystem.Infrastructure.Providers
{
    public class MappingProvider : IMappingProvider
    {
        private IMapper mapper;

        public MappingProvider(IMapper mapper)
        {
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            this.mapper = mapper;
        }

        public TDestination MapTo<TDestination>(object source)
        {
            return this.mapper.Map<TDestination>(source);
        }

        public IQueryable<TDestination> ProjectTo<TDestination>(IQueryable<object> source)
        {
            return source.ProjectTo<TDestination>();
        }

        public IEnumerable<TDestination> EnumerableProjectTo<TSource, TDestination>(IEnumerable<TSource> source)
        {
            return source.AsQueryable().ProjectTo<TDestination>();
        }
    }
}
