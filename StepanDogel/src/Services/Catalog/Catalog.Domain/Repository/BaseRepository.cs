using AutoMapper;
using Catalog.Domain.Data;
using Catalog.Domain.Data.Entities.Base;
using Catalog.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Catalog.Domain
{
    public class BaseRepository<TModel, TViewModel, TPostModel, TPutModel> : IBaseRepository<TModel, TViewModel, TPostModel, TPutModel>
        where TModel : BaseEntity
        where TViewModel : class
        where TPostModel : class
        where TPutModel : class
    {
        private readonly EfDbContext _dbContext;
        private readonly IMapper _mapper;
        public BaseRepository(EfDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> PostAsync(TPostModel model, CancellationToken token)
        {
            try
            {
                var post = _mapper.Map<TModel>(model);
                await _dbContext.Set<TModel>().AddAsync(post);
                await _dbContext.SaveChangesAsync();

                return post.Id;
            }
            catch
            {
                throw new Exception(); 
            }
        }

        public async Task<bool> DeleteAsync(int id, CancellationToken token)
        {
            try
            {
                var item = await _dbContext.Set<TModel>().FindAsync(id);
                if (item is not null)
                {
                    _dbContext.Set<TModel>().Remove(item);
                    await _dbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception(); 
            }
        }

        public async Task<TViewModel?> GetAsync(int id, CancellationToken token, params string[] includes)
        {
            var data = _dbContext.Set<TModel>().AsQueryable();
            data = includes.Aggregate(data, (current, include) => current.Include(include));
            var item = await data.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

            return _mapper.Map<TViewModel>(item);
        }

        public async Task<List<TViewModel>> GetAllAsync(CancellationToken token, params string[] includes)
        {
            var data = _dbContext.Set<TModel>().AsQueryable();
            data = includes.Aggregate(data, (current, include) => current.Include(include));
            var items = await data.AsNoTracking().ToListAsync();

            return _mapper.Map<List<TViewModel>>(items);
        }

        public async Task<bool> PutAsync(TPutModel model, CancellationToken token)
        {
            try
            {
                if (model != null)
                {
                    _dbContext.Set<TModel>().Update(_mapper.Map<TModel>(model));
                    await _dbContext.SaveChangesAsync();

                    return true;
                }
                return false;
            }
            catch
            {
                throw new Exception();  
            }
        }
    }
}
