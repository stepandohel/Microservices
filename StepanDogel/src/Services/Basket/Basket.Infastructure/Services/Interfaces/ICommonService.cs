namespace Basket.Infastructure.Services.Interfaces
{
    public interface ICommonService<TViewModel, TPostModel, TPutModel>
        where TViewModel : class
        where TPostModel : class
        where TPutModel : class
    {
        public Task<List<TViewModel>> GetAllAsync(CancellationToken cancellationToken);
        public Task<int> CreateAsync(TPostModel postModel, CancellationToken cancellationToken);
        public Task<TViewModel> UpdateAsync(int Id, TPutModel putModel, CancellationToken cancellationToken);
        public Task<bool> DeleteAsync(int Id, CancellationToken cancellationToken);
    }
}
