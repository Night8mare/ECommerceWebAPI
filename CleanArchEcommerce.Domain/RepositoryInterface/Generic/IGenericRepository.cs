using CleanArchEcommerce.Application.Specifications.BaseSpecifications;
using CleanArchEcommerce.Domain.Entities.BaseEntity;

namespace CleanArchEcommerce.Domain.RepositoryInterface.Generic
{
    public interface IGenericRepository<T> where T : BaseEntity<int>
    {
        Task<T> GetByIdAsync(int id);
        Task<T> GetBySpecAsync(Specification<T> spec);
        Task<IEnumerable<T>> GetAllPagedWithSpecAsync(Specification<T> spec);
        Task<int> GetAllCountAsync();
        Task<int> GetAllCountWithSpecAsync(Specification<T> spec);
        Task<T> AddAsync(T entity);
        Task<int> DeleteWithSpecAsync(Specification<T> spec);
        Task<int> UpdateAsync(T entity);
        Task SaveChangesAsync();
    }
}
