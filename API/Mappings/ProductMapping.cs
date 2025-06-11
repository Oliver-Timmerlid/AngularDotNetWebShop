using API.Dtos;
using API.Models;

namespace API.Mappings
{
    public static class ProductMapping
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                ImageUrl = product.ImageUrl,
                Ratings = product.Ratings?.Select(r => r.RatingValue).ToList() ?? new List<double>()

            };
        }

        public static Product ToEntity(this ProductCreateDto dto)
        {
            return new Product
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                ImageUrl = dto.ImageUrl
            };
        }
    }
}
