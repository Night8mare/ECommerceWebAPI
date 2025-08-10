using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Carts;
using CleanArchEcommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchEcommerce.Infrastructure.Repositories.Carts
{
    public class CartRepository : ICartRepository
    {
        #region field
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor
        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handling functions
        #region Create
        public async Task<Cart> CreateCartAsync(Cart cart)
        {
            Log.Information($"Adding {cart.Id}");
            await _context.Carts.AddAsync(cart);
            await _context.SaveChangesAsync();
            return cart;
        }
        #endregion
        #region Delete
        public async Task<int> DeleteCartAsync(int id)
        {
            Log.Information($"Cart deleted: {id}");
            return await _context.Carts.Where(c => c.Id == id).ExecuteDeleteAsync();
        }
        #endregion
        #region Get all cart paginated
        public async Task<List<Cart>> GetAllCartAsync(int pageNumber, int pageSize)
        {
            var Skip = (pageNumber - 1) * pageSize;
            Log.Information("Returning paginated cart.");
            return await _context.Carts.Skip(Skip).Take(pageSize).ToListAsync();
        }
        #endregion
        #region Get cart by ID
        public async Task<Cart> GetCartByIdAsync(int id)
        {
            Log.Information($"Getting Cart by ID: {id}");
            return await _context.Carts.Where(c => c.UserId == id).AsNoTracking().FirstOrDefaultAsync();
        }
        #endregion
        #region Matching user with cart
        public async Task<bool> MatchUserCartIdAsync(int userId, int cartId)
        {
            Log.Information($"Matching user ID:{userId} with cart ID: {cartId}");
            return await _context.Carts.Include(c => c.User)
                    .Where(c => c.User.Id == userId && c.Id == cartId).AnyAsync();
        }
        #endregion
        #endregion
    }
}
