using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Domain.RepositoryInterface.Carts
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetAllCartAsync(int pageNumber, int pageSize);
        Task<Cart> GetCartByIdAsync(int id);
        Task<int> DeleteCartAsync(int id);
        Task<Cart> CreateCartAsync(Cart cart);
        Task<bool> MatchUserCartIdAsync(int userId, int cartId);
    }
}
