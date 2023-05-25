using Diploma.Database;
using Diploma.model.order;
using Diploma.model.provider;
using Diploma.model.warehouse;
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
        public async Task<List<Order>> GetAll(string? search, bool? warehouse)
        {
            IQueryable<Order> orders = _efModel.Orders
                .Include(u => u.Provider)
                    .ThenInclude(u => u.Post);

            if(warehouse != null)
            {
                orders = orders.Where(u => u.Warehouse == warehouse);
            }

            if (!string.IsNullOrEmpty(search))
            {
                var q = search.ToLower().Trim();

                orders = orders.Where(u => u.Description.ToLower().Contains(search)
                || u.Title.ToLower().Contains(search));
            }

            return await orders.ToListAsync();
        }

        [HttpGet("Warehouse")]
        public async Task<List<WarehouseOrder>> GetWarehouseAll(string? search)
        {
            IQueryable<WarehouseOrder> orders = _efModel.WarehouseOrders
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
        [HttpPost("{id}/Warehouse")]
        public async Task<ActionResult> CreateOrderWarehouse(int id, WarehouseState state)
        {
            var order = await _efModel.Orders.FindAsync(id);

            if (order == null)
                return NotFound();

            var orderWarehouse = new WarehouseOrder
            {
                Id = id,
                State = state,
                Title = order.Title,
                Description = order.Description
            };

            _efModel.Orders.Remove(order);
            await _efModel.WarehouseOrders.AddAsync(orderWarehouse);
            await _efModel.SaveChangesAsync();

            return Ok();
        }

        [Authorize]
        [HttpPatch("Warehouse/{id}/State")]
        public async Task<ActionResult> UpdateWarehouseState(WarehouseState state, int id)
        {
            var order = await _efModel.WarehouseOrders.FindAsync(id);

            if (order == null)
                return NotFound();

            order.State = state;

            _efModel.Entry(order).State = EntityState.Modified;
            await _efModel.SaveChangesAsync();

            return Ok();
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
