using Microsoft.EntityFrameworkCore;
using Store.Data.Contexts;
using Store.Data.Entities;
using Store.Repository.Interfaces;
using Store.Repository.Specification;

namespace Store.Repository.Repository
{
    public class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : BaseEntity<TKey>
    {
        private readonly StoreDbContext _context;

        public GenericRepository(StoreDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TEntity entity)
        => await _context.Set<TEntity>().AddAsync(entity);

        public async Task<int> CountSpecsAsync(ISpecification<TEntity> specs)
        => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs).CountAsync();

        public void Delete(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

        public async Task<IReadOnlyList<TEntity>> GetAllAsNoTrackingAsync()
           => await _context.Set<TEntity>().AsNoTracking().ToListAsync();


        public async Task<IReadOnlyList<TEntity>> GetAllAsync()
           => await _context.Set<TEntity>().ToListAsync();

        public async Task<TEntity> GetByIdAsync(TKey id)
           => await _context.Set<TEntity>().FindAsync(id);

        public async Task<IReadOnlyList<TEntity>> GetWithSpecsAllAsync(ISpecification<TEntity> specs)
        => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs).ToListAsync();

        public async Task<TEntity> GetWithSpecsByIdAsync(ISpecification<TEntity> specs)
        => await SpecificationEvaluator<TEntity, TKey>.GetQuery(_context.Set<TEntity>(), specs).FirstOrDefaultAsync();

        public void Update(TEntity entity)
           => _context.Set<TEntity>().Update(entity);

    }
}