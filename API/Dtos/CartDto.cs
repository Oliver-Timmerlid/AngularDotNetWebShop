namespace API.Dtos
{
    public class CartRowDto
    {
        public int ProductId { get; set; }
        public double Amount { get; set; }
        public string ProductTitle { get; set; }
        public double ProductPrice { get; set; }
    }
    public class CartDto
    {
        public int Id { get; set; }
        public bool Payed { get; set; }
        public List<CartRowDto> CartRows { get; set; }
        public double TotalPrice { get; set; }
    }

    public class AddToCartDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
        public double Amount { get; set; }
    }

    public class UpdateCartRowDto
    {
        public int ProductId { get; set; }
        public int UserId { get; set; }
    }
}
