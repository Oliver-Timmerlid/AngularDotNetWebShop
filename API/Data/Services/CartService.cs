using API.Dtos;
using API.Mappings;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Services
{
    public class CartService : ICartService
    {
        private readonly AppDbContext _context;

        public CartService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CartRowDto> AddToCartAsync(AddToCartDto dto)
        {
            var cart = await _context.Carts
                .Include(c => c.CartRows)
                .FirstOrDefaultAsync(c => c.UserId == dto.UserId && !c.Payed)
                ?? new Cart { UserId = dto.UserId, Payed = false, CartRows = new List<CartRow>() };

            if (cart.Id == 0)
            {
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();
            }

            var cartRow = cart.CartRows.FirstOrDefault(r => r.ProductId == dto.ProductId);
            if (cartRow == null)
            {
                cartRow = new CartRow { ProductId = dto.ProductId, Amount = dto.Amount, CartId = cart.Id };
                _context.CartRows.Add(cartRow);
            }
            else
            {
                cartRow.Amount += dto.Amount;
            }

            await _context.SaveChangesAsync();
            return cartRow.ToDto();
        }

        public async Task<bool> RemoveFromCartAsync(int productId, int userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.Payed);
            if (cart == null) return false;

            var cartRow = await _context.CartRows.FirstOrDefaultAsync(r => r.CartId == cart.Id && r.ProductId == productId);
            if (cartRow == null) return false;

            _context.CartRows.Remove(cartRow);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CartRowDto> IncreaseAmountAsync(int productId, int userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.Payed);
            if (cart == null) return null;

            var cartRow = await _context.CartRows.FirstOrDefaultAsync(r => r.CartId == cart.Id && r.ProductId == productId);
            if (cartRow == null) return null;

            cartRow.Amount += 1;
            await _context.SaveChangesAsync();
            return cartRow.ToDto();
        }

        public async Task<CartRowDto> DecreaseAmountAsync(int productId, int userId)
        {
            var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId && !c.Payed);
            if (cart == null) return null;

            var cartRow = await _context.CartRows.FirstOrDefaultAsync(r => r.CartId == cart.Id && r.ProductId == productId);
            if (cartRow == null) return null;

            if (cartRow.Amount <= 1)
            {
                _context.CartRows.Remove(cartRow);
            }
            else
            {
                cartRow.Amount -= 1;
            }
            await _context.SaveChangesAsync();
            return cartRow.ToDto();
        }
    }
}
