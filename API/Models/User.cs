using API.Models.Base;
using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class User:BaseEntity
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Navigation Property
        public ICollection<Cart> Carts { get; set; }
    }
}
