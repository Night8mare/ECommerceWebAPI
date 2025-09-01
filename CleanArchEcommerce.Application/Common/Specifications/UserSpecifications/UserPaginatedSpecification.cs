using CleanArchEcommerce.Application.Specifications.BaseSpecifications;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.Specifications.UserSpecifications
{
    public class UserPaginatedSpecification : Specification<User>
    {
        public UserPaginatedSpecification(int pageNumber, int pageSize) 
        {
            ApplyPaging(pageNumber, pageSize);
        }
    }
}
