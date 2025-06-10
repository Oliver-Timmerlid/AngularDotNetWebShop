using API.Models.Base;

namespace API.Models
{
    public class Cart:BaseEntity
    {
        public bool Payed { get; set; }

        // Foreign Key
        public int UserId { get; set; }
        // Navigation Property
        public User User { get; set; }

        // Navigation Property for CartRows
        public ICollection<CartRow> CartRows { get; set; }
    }
}
