namespace Basket.Infastructure.Services.Interfaces
{
    public interface ICommonService<TViewModel, TPostModel, TPutModel>
        where TViewModel : class
        where TPostModel : class
        where TPutModel : class
    {
        public Task<int> AddAsync(TPostModel postModel, CancellationToken cancellationToken);
        public Task<bool> UpdateAsync(int Id,TPutModel putModel, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken);
        public Task<List<TViewModel>> GetAllAsync(CancellationToken cancellationToken);
    }
}
