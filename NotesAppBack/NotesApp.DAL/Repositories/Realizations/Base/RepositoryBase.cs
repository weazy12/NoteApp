using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using NotesApp.DAL.Data;
using NotesApp.DAL.Repositories.Interfaces.Base;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace NotesApp.DAL.Repositories.Realizations.Base
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly NotesAppDbContext _notesAppDbContext;

        protected RepositoryBase(NotesAppDbContext notesAppDbContext)
        {
            _notesAppDbContext = notesAppDbContext;
        }
        public async Task<T> CreateAsync(T entity)
        {
            var tmp =  await _notesAppDbContext.Set<T>().AddAsync(entity);
            return tmp.Entity;

        }

        public void Delete(T entity)
        {
            _notesAppDbContext.Set<T>().Remove(entity);
        }

        public async Task<IEnumerable<T>> GetAllAsync(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).ToListAsync();
        }

        public async Task<T?> GetFirstOrDefaultAsync(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default)
        {
            return await GetQueryable(predicate, include).FirstOrDefaultAsync();
        }

        public EntityEntry<T> Update(T entity)
        {
            return _notesAppDbContext.Set<T>().Update(entity);
        }
        private IQueryable<T> GetQueryable(
           Expression<Func<T, bool>>? predicate = default,
           Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = default,
           Expression<Func<T, T>>? selector = default)
        {
            var query = _notesAppDbContext.Set<T>().AsNoTracking();

            if (include is not null)
            {
                query = include(query);
            }

            if (predicate is not null)
            {
                query = query.Where(predicate);
            }

            if (selector is not null)
            {
                query = query.Select(selector);
            }

            return query.AsNoTracking();
        }
    }
}
