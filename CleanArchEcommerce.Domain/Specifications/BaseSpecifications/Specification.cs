using CleanArchEcommerce.Domain.Entities.BaseEntity;
using System.Linq.Expressions;

namespace CleanArchEcommerce.Application.Specifications.BaseSpecifications
{
    public abstract class Specification<T> where T : BaseEntity<int>
    {
        public Expression<Func<T, bool>> Criteria { get; private set; }
        public List<Expression<Func<T, object>>> Includes { get; } = new();
        public Expression<Func<T, object>> OrderBy { get; private set; }
        public Expression<Func<T, object>> OrderByDescending { get; private set; }
        public int Skip { get; private set; } = 1;
        public int Take { get; private set; } = 10;
        public bool IsPagingEnabled { get; private set; } = false;
        public bool IsNoTracking {  get; private set; } = true;
        public Specification() { }

        protected void AddCriteria(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        protected void AddInclude(Expression<Func<T, object>> includeExpression)
        {
            Includes.Add(includeExpression);
        }
        protected void ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
        {
            OrderBy = orderByExpression;
        }
        protected void ApplyOrderByDescending(Expression<Func<T, object>> orderByDescendingExpression)
        {
            OrderByDescending = orderByDescendingExpression;
        }
        protected void ApplyPaging(int pageNumber, int pageSize)
        {
            Skip = (pageNumber - 1) * pageSize;
            Take = pageSize;
            IsPagingEnabled = true;
        }
        protected void ApplyNoTracking()
        {
            IsNoTracking = true;
        }
    }
}
