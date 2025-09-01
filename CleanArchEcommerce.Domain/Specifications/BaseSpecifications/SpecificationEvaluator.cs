using CleanArchEcommerce.Domain.Entities.BaseEntity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Specifications.BaseSpecifications
{
    public static class SpecificationEvaluator<T> where T : BaseEntity<int>
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, Specification<T> spec) where T : BaseEntity<int>
        {
            var query = inputQuery;

            if (spec.Criteria != null)
                query = query.Where(spec.Criteria);

            if (spec.Includes  != null)
                query = spec.Includes
                            .Aggregate(query, (current, include) => current.Include(include));

            if (spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            if (spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            if (spec.IsPagingEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);

            if (spec.IsNoTracking)
                query = query.AsNoTracking();

            return query;
        }
    }
}
