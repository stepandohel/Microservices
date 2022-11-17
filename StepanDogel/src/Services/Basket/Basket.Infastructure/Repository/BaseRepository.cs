using Basket.Domain.Data;
using Basket.Domain.Data.Entities.Base;
using Basket.Infastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Basket.Infastructure.Repository
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity>
        where TEntity : BaseEntity

    {
        protected readonly BasketDbContext _dbContext;
        protected readonly DbSet<TEntity> _entities;
        public BaseRepository(BasketDbContext dbContext)
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

        public virtual async Task<int> PostAsync(TEntity model, CancellationToken cancellationToken)
        {
            await _entities.AddAsync(model, cancellationToken);

            return model.Id;
        }

        public virtual async Task<bool> PutAsync(TEntity model, CancellationToken token)
        {
            if (model != null)
            {
                _entities.Update(model);

                return true;
            }
            return false;
        }

        public virtual async Task<bool> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var item = await _entities.FindAsync(id, cancellationToken);

            if (item is not null)
            {
                _entities.Remove(item);

                return true;
            }

            return false;
        }
    }
}
