using CleanArchEcommerce.Domain.Entities;

namespace CleanArchEcommerce.Domain.Repository.Users
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUserAsync(int pageNumber,int pageSize);
        Task<User> GetByIdAsync(int id);
        Task<User> GetUserByEmailAsync(string email);
        Task<User> RegisterUserAsync(User user);
        Task<int> UpdatingTokenAsync(User user, string token);
        Task<int> UpdateUserAsync(string email, User user);
        Task<int> UpdateAdminAsync(string email, User user);
        Task<int> DeleteUserAsync(int id);
        Task<bool> EmailExistAsync(string email);
    }
}
