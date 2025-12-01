using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Query;

namespace NotesApp.DAL.Repositories.Interfaces.Base
{
    public interface IRepositoryBase<T> where T : class
    {
        Task<T> CreateAsync(T entity);

        void Delete(T entity);

        EntityEntry<T> Update(T entity);

        Task<IEnumerable<T>> GetAllAsync(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);


        Task<T?> GetFirstOrDefaultAsync(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default);
    }
}
