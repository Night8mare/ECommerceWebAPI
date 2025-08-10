using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.DTOs
{
    public class OrderDto : IMapFrom<Order>
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public DateTime OrderDate { get; set; }

        public decimal TotalAmount { get; set; }

        public string OrderStatus { get; set; } = null!;
    }
}
