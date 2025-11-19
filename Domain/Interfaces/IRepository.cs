using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class, IBaseEntity, new()
    {
        //Repository Design Pattern: Veritabanı işlemlerini yapacak olan class ve methodların Generic yapıda oluşturulması ve kullanılması.

        // Generic Yapı: Tip bağımsız.

        //bool Add(Prompt prompt);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);

        void Update(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);


        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> filter = null,OrderType orderType = OrderType.ASC,params string[] includes);

        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes);



    }
}
