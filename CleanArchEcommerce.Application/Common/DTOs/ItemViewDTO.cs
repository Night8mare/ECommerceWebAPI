using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.DTOs
{
    public class ItemViewDTO : IMapFrom<Item>
    {
        public int Id { get; set; }
        public int CartId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string ItemStatus { get; set; } = null!;
        public int? OrderId { get; set; }
    }
}
