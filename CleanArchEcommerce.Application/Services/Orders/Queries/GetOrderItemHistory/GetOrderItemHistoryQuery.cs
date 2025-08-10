using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Application.Services.Orders.Queries.GetOrderItemHistory
{
    public class GetOrderItemHistoryQuery : IRequest<Result<List<ItemViewDTO>>>
    {
    }
}
