using API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class CartRow: BaseEntity
    {
        [Required]
        public double Amount { get; set; }

        // Foreign Keys
        public int CartId { get; set; }
        public int ProductId { get; set; }

        // Navigation Properties
        public Cart Cart { get; set; }
        public Product Product { get; set; }
    }
}
