using CleanArchEcommerce.Domain.Entities;
using CleanArchEcommerce.Domain.Repository.Users;
using CleanArchEcommerce.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace CleanArchEcommerce.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        #region Field
        private readonly ApplicationDbContext _context;
        #endregion
        #region Constructor
        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion
        #region Handling functions
            #region Delete user
            public async Task<int> DeleteUserAsync(int id)
            {
                Log.Information($"Deleting user ID: {id}");
                return await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
            }
            #endregion
            #region Checking Email Exist
            public async Task<bool> EmailExistAsync(string email)
            {
                Log.Information($"Checking email: {email} Exist..");
                return await _context.Users.AnyAsync(u => u.Email == email);
            }
            #endregion
            #region Getting all use list paginated
            public async Task<List<User>> GetAllUserAsync(int pageNumber, int pageSize)
            {
                var SkipPage = (pageNumber - 1) * pageSize;
                Log.Information("Getting all use list paginated");
                return await _context.Users.Skip(SkipPage).Take(pageSize).ToListAsync();
            }
            #endregion
            #region Get user by ID
            public async Task<User> GetByIdAsync(int id)
            {
                Log.Information($"Getting use by ID :{id}");
                return await _context.Users.FindAsync(id);
            }
            #endregion
            #region Get user by Email
            public async Task<User> GetUserByEmailAsync(string email)
            {
                Log.Information($"Getting user by email : {email}");
                return await _context.Users.AsNoTracking()
                                              .FirstOrDefaultAsync(u => u.Email == email);
            }
            #endregion
            #region Register user
            public async Task<User> RegisterUserAsync(User user)
            {
                await _context.Users.AddAsync(user);
                Log.Information($"Creating user : {user.Email}");
                await _context.SaveChangesAsync();
                Log.Information($"User {user.Email} created successfully");
                return user;
            }
            #endregion
            #region Update Admin
            public async Task<int> UpdateAdminAsync(string email, User user)
            {
                Log.Information($"Updating Admin email : {email}");
                return await _context.Users
                        .Where(u => u.Email == email)
                        .ExecuteUpdateAsync(setters =>
                        setters
                            .SetProperty(u => u.FirstName, user.FirstName)
                            .SetProperty(u => u.LastName, user.LastName)
                            .SetProperty(u => u.Email, user.Email)
                            .SetProperty(u => u.Address, user.Address)
                            .SetProperty(u => u.PostalCard, user.PostalCard)
                            .SetProperty(u => u.State, user.State)
                            .SetProperty(u => u.City, user.City)
                            .SetProperty(u => u.Country, user.Country)
                            .SetProperty(u => u.PhoneNo, user.PhoneNo)
                            .SetProperty(u => u.PasswordHash, user.PasswordHash)
                            .SetProperty(u => u.Role, user.Role)
                        );
            }
            #endregion
            #region Update user
            public async Task<int> UpdateUserAsync(string email, User user)
            {
                Log.Information($"Updating user email: {email}");
                return await _context.Users
                        .Where(u => u.Email == email)
                        .ExecuteUpdateAsync(setters => 
                        setters
                            .SetProperty(u => u.FirstName, user.FirstName)
                            .SetProperty(u => u.LastName, user.LastName)
                            .SetProperty(u => u.Email, user.Email)
                            .SetProperty(u => u.Address, user.Address)
                            .SetProperty(u => u.PostalCard, user.PostalCard)
                            .SetProperty(u => u.State, user.State)
                            .SetProperty(u => u.City, user.City)
                            .SetProperty(u => u.Country, user.Country)
                            .SetProperty(u => u.PhoneNo, user.PhoneNo)
                            .SetProperty(u => u.PasswordHash, user.PasswordHash)
                        );
            }
            #endregion
            #region Updating Token
            public async Task<int> UpdatingTokenAsync(User user, string token)
            {
                Log.Information($"Updating token for user: {user.Email}");
                return await _context.Users.Where(u => u.Email == user.Email)
                        .ExecuteUpdateAsync(setters => setters
                            .SetProperty(u => u.Token, token));
            }
            #endregion
        #endregion
    }
}
