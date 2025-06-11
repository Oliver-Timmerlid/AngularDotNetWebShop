using API.Dtos;
using API.Mappings;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;
        public ProductService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            var products = await _context.Products.Include(p => p.Ratings).ToListAsync();
            return products.Select(p => p.ToDto());
        }

        public async Task<ProductDto?> GetByIdAsync(int id)
        {
            var product = await _context.Products.Include(p => p.Ratings).FirstOrDefaultAsync(p => p.Id == id);
            return product?.ToDto();
        }

        public async Task<ProductDto> CreateAsync(ProductCreateDto dto)
        {
            var product = dto.ToEntity();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            var created = await _context.Products.Include(p => p.Ratings).FirstOrDefaultAsync(p => p.Id == product.Id);
            return created.ToDto();
        }

        public async Task<bool> UpdateAsync(int id, ProductCreateDto dto)
        {
            var existing = await _context.Products.FindAsync(id);
            if (existing == null) return false;
            existing.Title = dto.Title;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.ImageUrl = dto.ImageUrl;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<RatingDto?> AddRatingAsync(int productId, RatingDto ratingDto)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            var rating = new Rating
            {
                ProductId = productId,
                RatingValue = ratingDto.RatingValue
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

            return new RatingDto
            {
                RatingValue = rating.RatingValue
            };
        }
    }
}
