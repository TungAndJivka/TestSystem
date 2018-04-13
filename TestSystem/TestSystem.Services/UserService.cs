using Bytes2you.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using TestSystem.Data.Data.Repositories;
using TestSystem.Data.Data.Saver;
using TestSystem.Data.Models;
using TestSystem.DTO;
using TestSystem.Infrastructure.Providers;
using TestSystem.Services.Contracts;

namespace TestSystem.Services
{
    public class UserService : IUserService
    {
        //private readonly IHashingPassword hashing;
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IEfGenericRepository<User> users;

        public UserService(IMappingProvider mapper, ISaver saver, IEfGenericRepository<User> users) // Hashing
        {
            Guard.WhenArgument(mapper, "mapper").IsNull().Throw();
            //Guard.WhenArgument(hashing, "hashing").IsNull().Throw();
            Guard.WhenArgument(saver, "saver").IsNull().Throw();
            Guard.WhenArgument(users, "userRepo").IsNull().Throw();
            //this.hashing = hashing;
            this.saver = saver;
            this.mapper = mapper;
            this.users = users;
        }

    }
}
