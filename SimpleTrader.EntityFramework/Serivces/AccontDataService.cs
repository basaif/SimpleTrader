using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore;
using SimpleTrader.Domain.Models;
using SimpleTrader.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SimpleTrader.EntityFramework.Serivces.Common;

namespace SimpleTrader.EntityFramework.Serivces
{
    public class AccontDataService : IDataService<Account>
    {
        private readonly SimpleTraderDbContextFactory _contextFactory;
        private readonly NonQueryDataService<Account> _nonQueryDataService;

        public AccontDataService(SimpleTraderDbContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new(_contextFactory);
        }
        public async Task<Account> Create(Account entity)
        {
            return await _nonQueryDataService.Create(entity);
        }

        public async Task<bool> Delete(int id)
        {
            return await _nonQueryDataService.Delete(id);
        }

        public async Task<Account> Get(int id)
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();

            if (context.Accounts is not null)
            {
                Account? entity = await context.Accounts.Include(a => a.AssetTransactions)
                                                            .FirstOrDefaultAsync(e => e.Id == id);
                if (entity is not null)
                {
                    return entity;
                }
            }
            return (Account)new DomainObject();
        }

        public async Task<IEnumerable<Account>> GetAll()
        {
            using SimpleTraderDbContext context = _contextFactory.CreateDbContext();
            if (context.Accounts is not null)
            {
                IEnumerable<Account> entities = await context.Accounts.Include(a => a.AssetTransactions).ToListAsync();
                return entities;
            }
            throw new Exception();
        }

        public async Task<Account> Update(int id, Account entity)
        {
            return await _nonQueryDataService.Update(id, entity);
        }
    }
}
