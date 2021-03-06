﻿using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestSystem.Data.Models.Abstractions;
using TestSystem.Web.Data;

namespace TestSystem.Data.Data.Repositories
{
    public class EfGenericRepository<T> : IEfGenericRepository<T>
    where T : class, IDeletable
    {
        private readonly ApplicationDbContext context;

        public EfGenericRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        public IQueryable<T> All
        {
            get
            {
                return this.context.Set<T>().Where(x => !x.IsDeleted);
            }
        }

        public void Add(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);

            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.context.Set<T>().Add(entity);
            }
        }

        public void Delete(T entity)
        {
            entity.IsDeleted = true;
            entity.DeletedOn = DateTime.Now;

            var entry = this.context.Entry(entity);
            entry.State = EntityState.Modified;
        }

        public void RealDelete(T entity)
        {
            var entry = this.context.Entry(entity);
            entry.State = EntityState.Deleted;
        }

        public void Update(T entity)
        {
            EntityEntry entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.context.Set<T>().Attach(entity);
            }

            entry.State = EntityState.Modified;
        }
    }
}
