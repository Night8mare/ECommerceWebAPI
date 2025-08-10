using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Domain.RepositoryInterface.Orders
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);
        Task<List<Order>> GetOrderHistoryAsync(int userId);
    }
}
