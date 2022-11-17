
using Catalog.Domain.Data.Entities.Base;

namespace Catalog.Domain.Interfaces
{
    public interface IBaseRepository<TModel, TViewModel, TPostModel, TPutModel>
        where TModel : BaseEntity
        where TViewModel : class
        where TPostModel : class
        where TPutModel : class
    {
        public Task<List<TViewModel>> GetAllAsync(CancellationToken token, params string[] includes);
        public  Task<TViewModel?> GetAsync(int id, CancellationToken token, params string[] includes);
        public Task<int> PostAsync(TPostModel model, CancellationToken token);
        public Task<bool> PutAsync(TPutModel model, CancellationToken token);
        public Task<bool> DeleteAsync(int id, CancellationToken token);
    }
}
