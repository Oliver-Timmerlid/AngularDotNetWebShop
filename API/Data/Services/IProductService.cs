using API.Dtos;
using API.Models;

namespace API.Data.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync();
        Task<ProductDto?> GetByIdAsync(int id);
        Task<ProductDto> CreateAsync(ProductCreateDto product);
        Task<bool> UpdateAsync(int id, ProductCreateDto product);
        Task<bool> DeleteAsync(int id);
        Task<RatingDto?> AddRatingAsync(int productId, RatingDto rating);

    }
}
