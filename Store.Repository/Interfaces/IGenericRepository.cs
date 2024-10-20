using Store.Data.Entities;
using Store.Repository.Specification;

namespace Store.Repository.Interfaces
{
    public interface IGenericRepository<TEntity , TKey> where TEntity : BaseEntity<TKey>
    {

        Task<TEntity> GetByIdAsync(TKey id);
        Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync();

        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        Task<IReadOnlyList<TEntity>> GetAllAsync();
        Task<TEntity> GetWithSpecsByIdAsync(ISpecification<TEntity> specs);
        Task<IReadOnlyList<TEntity>> GetWithSpecsAllAsync(ISpecification<TEntity> specs);
        Task<int> CountSpecsAsync(ISpecification<TEntity> specs);


    }
}
