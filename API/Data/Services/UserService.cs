using API.Dtos;
using API.Mappings;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _context;
        public UserService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();
            return users.Select(u => u.ToDto());
        }
        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            return user?.ToDto();
        }
        public async Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
        {
            var user = userCreateDto.ToEntity();
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user.ToDto();
        }
        public async Task<UserDto> UpdateAsync(UserDto userDto)
        {
            var existing = await _context.Users.FindAsync(userDto.Id);
            if (existing == null) return null;
            existing.Email = userDto.Email;
            existing.FirstName = userDto.FirstName;
            existing.LastName = userDto.LastName;
            await _context.SaveChangesAsync();
            return existing.ToDto();
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Cart> GetCartAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.CartRows)
                .ThenInclude(cr => cr.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }
    }
}
