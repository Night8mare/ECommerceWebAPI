using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Orders;
using CleanArchEcommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchEcommerce.Infrastructure.Repositories.Orders
{
    public class OrderRepository : IOrderRepository
    {
        #region Field
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor
        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handler Function
        #region Creating Order
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            Log.Information($"Creating Order for cart ID: {order.CartId}");
            await _context.SaveChangesAsync();
            Log.Information("Order created successfully");
            return order;
        }
        #endregion
        #region Get Order History
        public async Task<List<Order>> GetOrderHistoryAsync(int userId)
        {
            Log.Information("Executing getting order history from the database..");
            return await _context.Orders.Include(o => o.Cart).Where(o => o.Cart.UserId == userId).ToListAsync();
        }
        #endregion
        #endregion
    }
}
