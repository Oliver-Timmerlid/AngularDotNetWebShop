using API.Dtos;

namespace API.Data.Services
{
    public interface ICartService
    {
        Task<CartRowDto> AddToCartAsync(AddToCartDto dto);
        Task<bool> RemoveFromCartAsync(int productId, int userId);
        Task<CartRowDto> IncreaseAmountAsync(int productId, int userId);
        Task<CartRowDto> DecreaseAmountAsync(int productId, int userId);
    }
}
