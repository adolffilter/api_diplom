using Diploma.Database;
using Diploma.model.order;
using Diploma.model.provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

        [HttpGet]
        public async Task<List<Order>> GetAllProvider(string? search)
        {
            IQueryable<Order> orders = _efModel.Orders
                .Include(u => u.Provider)
                    .ThenInclude(u => u.Post);

            if (!string.IsNullOrEmpty(search))
            {
                var q = search.ToLower().Trim();

                orders = orders.Where(u => u.Description.ToLower().Contains(search)
                || u.Title.ToLower().Contains(search));
            }

            return await orders.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult> Create(CreateOrderDto dto)
        {
            var provider = await _efModel.Providers.FindAsync(dto.ProviderId);

            if(provider == null)
                return NotFound();

            var order = new Order
            {
                Title = dto.Title,
                Description = dto.Description,
                Provider = provider
            };

            await _efModel.Orders.AddAsync(order);
            await _efModel.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Provider>> Update(int id, CreateOrderDto dto)
        {
            var order = await _efModel.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            var provider = await _efModel.Providers.FindAsync(dto.ProviderId);

            if (provider == null)
                return NotFound();

            order.Title = dto.Title;
            order.Description = dto.Description;
            order.Provider = provider;

            _efModel.Entry(provider).State = EntityState.Modified;
            await _efModel.SaveChangesAsync();

            return provider;
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult> Delete(int id)
        {
            var order = await _efModel.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            _efModel.Orders.Remove(order);
            await _efModel.SaveChangesAsync();

            return Ok();
        }
    }
}
