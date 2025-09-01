using CleanArchEcommerce.Application.Specifications.BaseSpecifications;
using CleanArchEcommerce.Domain.Entities.BaseEntity;
using CleanArchEcommerce.Domain.RepositoryInterface.Generic;
using CleanArchEcommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchEcommerce.Infrastructure.Repositories.Generic
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseEntity<int>
    {
        #region Field
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;
        #endregion
        #region Constructor
        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
        #endregion
        #region Handler Function

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> GetBySpecAsync(Specification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<T>> GetAllPagedWithSpecAsync(Specification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).ToListAsync();
        }

        public async Task<int> GetAllCountWithSpecAsync(Specification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).CountAsync();
        }

        public async Task<int> GetAllCountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> DeleteWithSpecAsync(Specification<T> spec)
        {
            return await SpecificationEvaluator<T>.GetQuery(_dbSet, spec).ExecuteDeleteAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return await _context.SaveChangesAsync();
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
