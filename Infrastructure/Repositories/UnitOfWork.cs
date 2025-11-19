using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class UnitOfWork<TDbContext> : IUnitOfWork
        where TDbContext: DbContext
    {

        readonly DbContext dbContext;

        IDbContextTransaction transaction;


        public IRepository<Category>? categories;

        public IRepository<Prompt>? prompts;

        public IRepository<Purchase>? purchases;

        public IRepository<Subscription>? subscriptions;

        public IRepository<Payment>? payments;

        public IRepository<Favourite>? favourites;

        public IRepository<AuditLog>? auditLogs;

        public IRepository<Cart>? carts;



        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        public IRepository<Category> Categories => categories ??= new Repository<Category,DbContext>(dbContext);

        public IRepository<Prompt> Prompts => prompts ??= new Repository<Prompt, DbContext>(dbContext);

        public IRepository<Purchase> Purchases => purchases ??= new Repository<Purchase, DbContext>(dbContext);

        public IRepository<Subscription> Subscriptions => subscriptions ??= new Repository<Subscription, DbContext>(dbContext);

        public IRepository<Payment> Payments => payments ??= new Repository<Payment, DbContext>(dbContext);

        public IRepository<Favourite> Favourites => favourites ??= new Repository<Favourite, DbContext>(dbContext);

        public IRepository<AuditLog> AuditLogs => auditLogs ??= new Repository<AuditLog, DbContext>(dbContext);

        public IRepository<Cart> Carts => carts ??= new Repository<Cart, DbContext>(dbContext);

        public async Task BeginTransactionAsync() => transaction = await dbContext.Database.BeginTransactionAsync();

        public async Task<int> CommitTransactionAsync()
        {
            try
            {
                if (transaction == null) return 0;

                int result = await dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
                await dbContext.DisposeAsync();
                transaction = null;
                return result;
            }
            catch (Exception)
            {
                await RollbackTransactionAsync();
                return 0;
            }
            

        }

        //public async Task<int> CompleteAsync() => await dbContext.SaveChangesAsync();


        public async Task<int> CompleteAsync()
        {
            try
            {
                await BeginTransactionAsync();
                return await CommitTransactionAsync();
            }
            catch (Exception)
            {

                await RollbackTransactionAsync();
                return 0;
            }
           
           
        }


        public void Dispose()
        {
            transaction?.Dispose();
            dbContext.Dispose();
        }

        public async Task RollbackTransactionAsync()
        {
            if (transaction == null) return;

            await transaction.RollbackAsync();
            await transaction.DisposeAsync();
            transaction = null;
        }
    }
}
