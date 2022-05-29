using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SimpleTrader.EntityFramework.Serivces.Common
{
    public class NonQueryDataService<T> where T : DomainObject
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;

        public NonQueryDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }
        public async Task<T> Create(T entity)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            EntityEntry<T>? createdResult = await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();

            return createdResult.Entity;
        }

        public async Task<bool> Delete(int id)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            T? entity = await context.Set<T>().FirstOrDefaultAsync(e => e.Id == id);
            if (entity is not null)
            {
                context.Set<T>().Remove(entity);
            }
            await context.SaveChangesAsync();

            return true;
        }



        public async Task<T> Update(int id, T entity)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            entity.Id = id;

            context.Set<T>().Update(entity);
            await context.SaveChangesAsync();

            return entity;
        }
    }
}
