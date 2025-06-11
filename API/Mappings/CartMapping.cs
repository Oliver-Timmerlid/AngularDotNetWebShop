using API.Dtos;
using API.Models;

namespace API.Mappings
{
    public static class CartMapping
    {
        public static CartRowDto ToDto(this CartRow row)
        {
            return new CartRowDto
            {
                ProductId = row.ProductId,
                Amount = row.Amount,
                ProductTitle = row.Product?.Title,
                ProductPrice = row.Product?.Price ?? 0
            };
        }
        public static CartDto ToDto(this Cart cart)
        {
            var cartRows = cart.CartRows?.Select(row => row.ToDto()).ToList() ?? new List<CartRowDto>();
            var totalPrice = cartRows.Sum(row => row.ProductPrice * row.Amount);

            return new CartDto
            {
                Id = cart.Id,
                Payed = cart.Payed,
                CartRows = cartRows,
                TotalPrice = totalPrice
            };
        }

    }
}
