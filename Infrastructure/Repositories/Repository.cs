using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class Repository<TEntity,TDbContext> : IRepository<TEntity>
        where TEntity : class, IBaseEntity, new()
        where TDbContext : DbContext
    {
        readonly TDbContext dbContext;
        readonly DbSet<TEntity> dbSet;
        

        public Repository(TDbContext dbContext)
        {
            this.dbContext = dbContext;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task AddAsync(TEntity entity) => await dbSet.AddAsync(entity);

        //public async Task AddAsync(TEntity entity)
        //{
        //    await dbSet.AddAsync(entity);
        //}

        public async Task AddRangeAsync(IEnumerable<TEntity> entities) => await dbSet.AddRangeAsync(entities);

        public async Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            //Sorguyu hazırlıyor veritabanına göndermiyor.
            // Yani sadece select * from Tablo Adı ama srogu daha henüz çalıştırılşmadı çünkü hazırlanıyor başka değerlerde eklenebilir sorguya
            IQueryable<TEntity> query = dbSet.AsQueryable();
            
            //select * from Tablo adı where Koşul
            query = query.Where(filter);

            //Joinlemek için
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            //verileri Sıralamak için
            query = orderType == OrderType.ASC ? query.OrderBy(e => e.Id) : query.OrderByDescending(x => x.Id);

            //Sorgu veri tabanına gönderiliyor.
            return await query.ToListAsync();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes)
        {
            //Sorguyu hazırlıyor veritabanına göndermiyor.
            // Yani sadece select * from Tablo Adı ama sorgu daha henüz çalıştırılmadı çünkü hazırlanıyor başka değerlerde eklenebilir sorguya
            IQueryable<TEntity> query = dbSet.AsQueryable();

            //select * from Tablo adı where Koşul
            if (filter != null)
                query = query.Where(filter);

            //Joinlemek için
            if(includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include);
                }
            }

            //verileri Sıralamak için
            query = orderType == OrderType.ASC ? query.OrderBy(e => e.Id) : query.OrderByDescending(x => x.Id);

            //Sorgu veri tabanına gönderiliyor.
            return await query.ToListAsync();

        }

        public async Task<TEntity?> GetByIdAsync(int id) => await dbSet.FindAsync(id);

        public void Remove(TEntity entity)
        {
            var result = dbSet.FirstOrDefault(x=>x.Id == entity.Id);//Find(entity.Id);
            if (result != null)
            {
                result.IsActive = false;
                dbSet.Update(entity);

            }
         

        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            var result = dbSet.Where(x => entities.Any(e => e.Id == x.Id)).ToList();//Find(entity.Id);
            foreach (var item in result)
            {
                item.IsActive = false;
                dbSet.Update(item);
            }
           
        }

        public void Update(TEntity entity) => dbSet.Update(entity);

    }
}
