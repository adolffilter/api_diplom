using Diploma.Database;
using Diploma.model.product;
using Diploma.model.warehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private EfModel _efModel;

        public ProductController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAll(string? search)
        {
            IQueryable<Product> products = _efModel.Products;

            if (!string.IsNullOrEmpty(search))
            {
                products = products.Where(u => u.Description.Contains(search) || u.Name.Contains(search));
            }

            return await products.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Product>> Add(CreateProductDto dto)
        {
            var product = new Product
            {
                Description = dto.Description,
                Name = dto.Name,
                Price = dto.Price
            };

            await _efModel.Products.AddAsync(product);
            await _efModel.SaveChangesAsync();

            return product;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> Update(int id, CreateProductDto dto)
        {
            var product = await _efModel.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            product.Description = dto.Description;
            product.Name = dto.Name;
            product.Price = dto.Price;

            _efModel.Entry(product).State = EntityState.Modified;

            await _efModel.SaveChangesAsync();

            return product;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var product = await _efModel.Products.FindAsync(id);

            if (product == null)
                return NotFound();

            _efModel.Products.Remove(product);
            await _efModel.SaveChangesAsync();

            return Ok();
        }
    }
}
