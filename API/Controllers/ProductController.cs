using API.Data.Services;
using API.Dtos;
using API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;
        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _service.GetAllAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _service.GetByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateDto product)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
            var created = await _service.CreateAsync(product);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}/edit")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductCreateDto product)
        {
            if (!ModelState.IsValid) return UnprocessableEntity(ModelState);
            var updated = await _service.UpdateAsync(id, product);
            if (!updated) return NotFound();
            return Ok(new { message = "Product updated" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _service.DeleteAsync(id);
            if (!deleted) return NotFound();
            return Ok(new { message = "Product deleted." });
        }

        [HttpPost("{id}/addRating")]
        public async Task<IActionResult> AddRating(int id, [FromBody] RatingDto rating)
        {
            var result = await _service.AddRatingAsync(id, rating);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
