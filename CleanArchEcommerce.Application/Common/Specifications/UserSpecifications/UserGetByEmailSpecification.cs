using CleanArchEcommerce.Application.Specifications.BaseSpecifications;
using CleanArchEcommerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Common.Specifications.UserSpecifications
{
    public class UserGetByEmailSpecification : Specification<User>
    {
        public UserGetByEmailSpecification(string email) 
        {            
            AddCriteria(u => u.Email == email );
        }
    }
}
