using AutoMapper;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Products.Commands.UpdateProduct
{
    public class UpdateProductCommand : IRequest<Result<int>>, IMapFrom<Product>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal PurchasePrice { get; set; }

        public int StockQuantity { get; set; }

        public string IsAvailable { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateProductCommand, Product>();
        }
    }
}
