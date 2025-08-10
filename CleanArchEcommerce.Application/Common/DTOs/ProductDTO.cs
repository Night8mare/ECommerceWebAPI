using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Application.Common.DTOs
{
    public class ProductDTO : IMapFrom<Product>
    {
        public string Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal PurchasePrice { get; set; }

        public int StockQuantity { get; set; }

        public string IsAvailable { get; set; } = null!;
    }
}
