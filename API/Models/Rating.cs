using API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Rating:BaseEntity
    {
        [Required]
        public double RatingValue { get; set; }

        // Foreign Key
        public int ProductId { get; set; }
        // Navigation Property
        public Product Product { get; set; }
    }
}
