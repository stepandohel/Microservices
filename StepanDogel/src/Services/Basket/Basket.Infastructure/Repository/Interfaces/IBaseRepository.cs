using Basket.Domain.Data.Entities.Base;

namespace Basket.Infastructure.Repository.Interfaces
{
    public interface IBaseRepository<TModel>
           where TModel : BaseEntity
    {
        public Task<List<TModel>> GetAllAsync(CancellationToken token, params string[] includes);
        public Task<TModel?> GetByIdAsync(int id, CancellationToken token, params string[] includes);
        public Task<int> PostAsync(TModel model, CancellationToken token);
        public Task<bool> PutAsync(TModel model, CancellationToken token);
        public Task<bool> DeleteAsync(int id, CancellationToken token);
    }
}
