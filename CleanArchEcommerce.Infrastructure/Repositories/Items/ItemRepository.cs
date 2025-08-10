using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.RepositoryInterface.Items;
using CleanArchEcommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchEcommerce.Infrastructure.Repositories.Items
{
    public class ItemRepository : IItemRepository
    {
        #region Field
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor
        public ItemRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handler Function
        #region Create item
        public async Task<Item> CreateItemAsync(Item item)
        {
            await _context.Items.AddAsync(item);
            Log.Information($"Creating item list for cart ID: {item.CartId}..");
            await _context.SaveChangesAsync();
            Log.Information("Saved Successfully..");
            return item;
        }
        #endregion
        #region Item Exist
        public async Task<bool> ItemExistAsync(int cartId, int productId)
        {
            Log.Information($"Cheching if product Id:{productId} exist in the item table..");
            return await _context.Items.Where(i => i.ProductId == productId && i.CartId == cartId && i.ItemStatus == "Pending").AnyAsync();
        }
        #endregion
        #region Update Item
        public async Task<int> UpdateItemAsync(int cartId, Item item)
        {
            Log.Information($"Updating item for cart ID {cartId}");
            return await _context.Items
                .Where(i => i.CartId == cartId)
                .ExecuteUpdateAsync(setter =>
                setter
                    .SetProperty(i => i.ProductId, item.ProductId)
                    .SetProperty(i => i.Quantity, item.Quantity)
                    .SetProperty(i => i.TotalAmount, item.TotalAmount)
                    .SetProperty(i => i.ItemStatus, item.ItemStatus)
                    );
        }
        #endregion
        #region Delete item
        public async Task<int> DeleteItemAsync(int itemId)
        {
            Log.Information($"Deleting item ID:{itemId}");
            return await _context.Items
                .Where(i => i.Id == itemId).ExecuteDeleteAsync();
        }
        #endregion
        #region Get all item
        public async Task<List<Item>> GetAllItemAsync(int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            Log.Information("Getting all item list from DataBase");
            return await _context.Items.Skip(skip).Take(pageSize).ToListAsync();
        }
        #endregion
        #region Get all item by Cart ID Paginated
        public async Task<List<Item>> GetAllItemIdAsync(int cartId,int pageNumber, int pageSize)
        {
            var skip = (pageNumber - 1) * pageSize;
            Log.Information($"Get all items for cart ID: {cartId}");
            return await _context.Items.Where(i => i.CartId == cartId).Skip(skip).Take(pageSize).ToListAsync();
        }
        #endregion
        #region Get all item by Cart ID
        public async Task<List<Item>> GetAllCartItemAsync(int cartId)
        {
            Log.Information($"Getting all item by Cart ID: {cartId}");
            return await _context.Items.Where(i => i.CartId == cartId && i.ItemStatus == "Pending").ToListAsync();
        }
        #endregion
        #region Get all item history by user ID
        public async Task<List<Item>> GetItemHistoryAsync(int userId)
        {
            Log.Information("Executing get item history..");
            return await _context.Items.Include(i => i.Cart).Where(i => i.Cart.UserId == userId && i.ItemStatus == "Ordered").ToListAsync();
        }
        #endregion
        #region Getting Total price
        public async Task<decimal> GetTotalPriceAsync(int cartId)
        {
            Log.Information($"Getting total price for cart ID: {cartId}");
            return await _context.Items.Where(i => i.CartId == cartId).SumAsync(i => i.TotalAmount);
        }
        #endregion
        #region Checking item availability
        public async Task<bool> CheckItemAvailabilityAsync(int cartId)
        {
            Log.Information($"Checking item availability ID: {cartId}");
            return await _context.Items
                    .Include(i => i.Product)
                    .Where(i => i.CartId == cartId && i.Product.IsAvailable == "OutOfStock" && i.OrderId == null).AnyAsync();
        }
        #endregion
        #region Checking item Quantity
        public async Task<bool> CheckItemQuantityAsync(int cartId)
        {
            Log.Information($"Checking item quantity ID: {cartId}");
            return await _context.Items
                    .Include(i => i.Product)
                    .Where(i => i.CartId == cartId && i.Quantity > i.Product.StockQuantity && i.OrderId == null).AnyAsync();
        }
        #endregion
        #region Update item for order ID
        public async Task<int> UpdateItemOrderAsync(int cartId, Item item)
        {
            Log.Information($"Updating item in cart ID: {cartId}/ item: {item.Id}");
            return await _context.Items
                .Where(i => i.CartId == cartId)
                .ExecuteUpdateAsync(setter =>
                setter
                    .SetProperty(i => i.ItemStatus, item.ItemStatus)
                    .SetProperty(i => i.OrderId, item.OrderId)
                    );
        }
        #endregion
        #endregion
    }
}
