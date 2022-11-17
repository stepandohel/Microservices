using Catalog.Domain.Data;
using Catalog.Domain.Data.Entities.Base;
using Catalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Catalog.Domain
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity
    {
        protected readonly CatalogDbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;

        public BaseRepository(CatalogDbContext dbContext)
        {
            _dbContext = dbContext;
            _entities = _dbContext.Set<TEntity>();
        }

        public virtual async Task<List<TEntity>> GetAllAsync(CancellationToken cancellationToken, params string[] includes)
        {
            var data = _entities.AsQueryable();
            data = includes.Aggregate(data, (current, include) => current.Include(include));
            var items = await data.AsNoTracking().ToListAsync(cancellationToken);

            return items;
        }

        public virtual async Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken, params string[] includes)
        {
            var data = _entities.AsQueryable();
            data = includes.Aggregate(data, (current, include) => current.Include(include));
            var item = await data.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

            return item;
        }

        public async Task<bool> GetAnyAsync(Expression<Func<TEntity, bool>> expression, CancellationToken cancellationToken)
        {
            return await _entities.AnyAsync(expression, cancellationToken);
        }

        public virtual async Task<int> CreateAsync(TEntity model, CancellationToken cancellationToken)
        {
            await _entities.AddAsync(model, cancellationToken);
            return model.Id;
        }

        public virtual async Task<TEntity> UpdateAsync(TEntity model)
        {
            if (model is not null)
            {
                _entities.Update(model);
            }
            return model;
        }

        public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var item = await _entities.FindAsync(id);
            if (item is not null)
            {
                _entities.Remove(item);
                return true;
            }

            return false;
        }
    }
}