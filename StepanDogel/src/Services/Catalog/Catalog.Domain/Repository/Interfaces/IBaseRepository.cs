
using Catalog.Domain.Data.Entities.Base;
using System.Linq.Expressions;

namespace Catalog.Domain.Interfaces
{
    public interface IBaseRepository<TModel>
            where TModel : BaseEntity
    {
        public Task<List<TModel>> GetAllAsync(CancellationToken token, params string[] includes);
        public Task<TModel?> GetByIdAsync(int id, CancellationToken token, params string[] includes);
        public Task<bool> GetAnyAsync(Expression<Func<TModel, bool>> expression, CancellationToken cancellationToken);
        public Task<int> CreateAsync(TModel model, CancellationToken token);
        public Task<TModel> UpdateAsync(TModel model);
        public Task<bool> DeleteAsync(int id, CancellationToken token);
    }
}
