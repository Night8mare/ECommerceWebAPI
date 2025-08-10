using AutoMapper;
using CleanArchEcommerce.Application.Common.DTOs;
using CleanArchEcommerce.Application.Common.Exceptions;
using CleanArchEcommerce.Application.Common.Mappings;
using CleanArchEcommerce.Domain.Entities;
using MediatR;

namespace CleanArchEcommerce.Application.Services.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<Result<ProductDTO>>, IMapFrom<Product>
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal PurchasePrice { get; set; }

        public int StockQuantity { get; set; }

        public string IsAvailable { get; set; } = null!;
        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateProductCommand, Product>();
        }
    }
}
