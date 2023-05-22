using Diploma.Database;
using Diploma.model.order;
using Diploma.model.user;
using Diploma.model.warehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private EfModel _efModel;

        public OrderController(EfModel model)
        {
            _efModel = model;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult<List<Order>>> GetAll(string? search)
        {
            var user = await GetUser();

            if (user == null)
                return NotFound();

            IQueryable<Order> orders = _efModel.Orders
                .Include(u => u.Product)
                .Include(u => u.User)
                .Where(u => u.User.Id == user.Id);

            if (!string.IsNullOrEmpty(search))
            {
                orders = orders.Where(u => u.Product.Description.Contains(search) || u.Product.Name.Contains(search));
            }

            return await orders.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Order>> Add(CreateOrderDto dto)
        {
            var user = await GetUser();

            if (user == null)
                return NotFound();

            var product = await _efModel.Products.FindAsync(dto.ProductId);

            if (product == null)
                return NotFound();

            var order = new Order
            {
                Product = product,
                User = user,
                Quantity = dto.Quantity,
                Addres = dto.Addres
            };

            await _efModel.Orders.AddAsync(order);
            await _efModel.SaveChangesAsync();

            return order;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Order>> Update(int id, CreateOrderDto dto)
        {
            var order = await _efModel.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            var product = await _efModel.Products.FindAsync(dto.ProductId);

            if (product == null)
                return NotFound();

            order.Product = product;
            order.Quantity = dto.Quantity;
            order.Addres = dto.Addres;

            _efModel.Entry(order).State = EntityState.Modified;

            await _efModel.SaveChangesAsync();

            return order;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _efModel.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            _efModel.Orders.Remove(order);
            await _efModel.SaveChangesAsync();

            return Ok();
        }

        [NonAction]
        private async Task<User?> GetUser()
        {
            if (HttpContext.User.Identity is not ClaimsIdentity identity)
                return null;

            var id = Convert.ToInt32(identity.FindFirst("Id")?.Value);

            var user = await _efModel.Users.FindAsync(id);

            return user;
        }
    }
}
