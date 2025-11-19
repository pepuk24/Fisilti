using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<Prompt> Prompts { get; }
        IRepository<Purchase> Purchases { get; }
        IRepository<Subscription> Subscriptions { get; }
        IRepository<Payment> Payments { get; }
        IRepository<Favourite> Favourites { get; }
        IRepository<AuditLog> AuditLogs { get; }
        IRepository<Cart> Carts { get; }

        Task<int> CompleteAsync();
        Task BeginTransactionAsync();
        Task<int> CommitTransactionAsync();
        Task RollbackTransactionAsync();


    }
}
