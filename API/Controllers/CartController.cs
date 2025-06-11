using API.Data.Services;
using API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpPost("addProduct")]
        public async Task<IActionResult> AddProduct([FromBody] AddToCartDto dto)
        {
            var result = await _cartService.AddToCartAsync(dto);
            return Ok(result);
        }

        [HttpDelete("removeProduct")]
        public async Task<IActionResult> RemoveProduct([FromBody] UpdateCartRowDto dto)
        {
            var success = await _cartService.RemoveFromCartAsync(dto.ProductId, dto.UserId);
            if (!success) return NotFound();
            return Ok();
        }

        [HttpPut("increaseAmount")]
        public async Task<IActionResult> IncreaseAmount([FromBody] UpdateCartRowDto dto)
        {
            var result = await _cartService.IncreaseAmountAsync(dto.ProductId, dto.UserId);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpPut("decreaseAmount")]
        public async Task<IActionResult> DecreaseAmount([FromBody] UpdateCartRowDto dto)
        {
            var result = await _cartService.DecreaseAmountAsync(dto.ProductId, dto.UserId);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}

