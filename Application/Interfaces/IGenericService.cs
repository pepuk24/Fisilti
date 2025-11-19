using Application.Common;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IGenericService<TEntity,TDto>
        where TEntity : class,IBaseEntity,new()
        where TDto : class,new()
    {
        Task<Result<TDto>> AddAsync(TDto entity);
        Task<Result<IEnumerable<TDto>>> AddRangeAsync(IEnumerable<TDto> entities);

        Task<Result<TDto>> Update(TDto entity);
        Task<Result<TDto>> Remove(TDto entity);
        Task<Result<IEnumerable<TDto>>> RemoveRange(IEnumerable<TDto> entities);


        Task<Result<TDto?>> GetByIdAsync(int id);
        Task<Result<IEnumerable<TDto>>> GetAllAsync(Expression<Func<TEntity, bool>> filter = null, OrderType orderType = OrderType.ASC, params string[] includes);

        Task<Result<IEnumerable<TDto>>> FindAsync(Expression<Func<TEntity, bool>> filter, OrderType orderType = OrderType.ASC, params string[] includes);
    }
}
