using API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Product: BaseEntity
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
        public string ImageUrl { get; set; }

        // Navigation Properties
        public ICollection<Rating> Ratings { get; set; }
        public ICollection<CartRow> CartRows { get; set; }
    }
}
