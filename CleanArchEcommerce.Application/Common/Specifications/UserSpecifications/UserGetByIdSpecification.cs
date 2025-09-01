using CleanArchEcommerce.Application.Specifications.BaseSpecifications;
using CleanArchEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Specifications.UserSpecifications
{
    public class UserGetByIdSpecification : Specification<User>
    {
        public UserGetByIdSpecification(int id) 
        {
            AddCriteria(u => u.Id == id);
        }
    }
}
