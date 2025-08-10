using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Domain.RepositoryInterface.Items
{
    public interface IItemRepository
    {
        Task<Item> CreateItemAsync(Item item);
        Task<int> UpdateItemAsync(int cartId,Item item);
        Task<int> UpdateItemOrderAsync(int cartId,Item item);
        Task<bool> ItemExistAsync(int cartId,int productId);
        Task<int> DeleteItemAsync(int itemId);
        Task<List<Item>> GetAllItemIdAsync (int cartId,int pageNumber, int pageSize);
        Task<List<Item>> GetAllCartItemAsync(int cartId);
        Task<List<Item>> GetItemHistoryAsync(int userId);
        Task<List<Item>> GetAllItemAsync(int pageNumber,int pageSize);
        Task<decimal> GetTotalPriceAsync(int cartId);
        Task<bool> CheckItemAvailabilityAsync(int cartId);
        Task<bool> CheckItemQuantityAsync(int cartId);
    }
}
