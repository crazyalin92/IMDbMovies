using DataAccess.DataBase;
using IMDbMovies.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly ApplicationContext dbContext;

        private readonly DbSet<TEntity> dbSet;
        public Repository(ApplicationContext context)
        {
            dbContext = context;
            dbSet = dbContext.Set<TEntity>();
        }

        public async Task<int> CreateAsync(TEntity entity)
        {
            await dbSet.AddAsync(entity);
            return await dbContext.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int Id)
        {
            var deleteEntity = await dbSet.FirstOrDefaultAsync(m => m.Id == Id);
            dbSet.Remove(deleteEntity);

            return await dbContext.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(int Id)
        {
            return await dbSet.FirstOrDefaultAsync(e => e.Id == Id);
        }

        public async Task<bool> UpdateAsync(TEntity model)
        {
            var updateEntity = await dbSet.FirstOrDefaultAsync(m => m.Id == model.Id);
            if (updateEntity != null)
            {
                dbSet.Update(updateEntity);
                return await dbContext.SaveChangesAsync() > 0;
            }

            return false;
        }

        public async Task<List<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate)
        {
            var query = dbSet.AsQueryable();

            var result = query.Where(predicate);

            return await result.ToListAsync();
        }

        public async Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AnyAsync(predicate);
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> predicate)
        {
            var query = dbSet.AsQueryable();

            var result = query.Where(predicate);

            return result;
        }

        public IQueryable<TEntity> GetAll()
        {
            var query = dbSet.AsQueryable();

            return query;
        }
    }
}
