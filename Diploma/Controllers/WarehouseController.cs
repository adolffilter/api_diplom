using Diploma.Database;
using Diploma.model.warehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private EfModel _efModel;

        public WarehouseController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet]
        public async Task<ActionResult<List<Warehouse>>> GetAll(string? search)
        {
            IQueryable<Warehouse> warehouse = _efModel.Warehouses;

            if(!string.IsNullOrEmpty(search))
            {
                warehouse = warehouse.Where(u => u.Description.Contains(search) || u.Address.Contains(search));
            }

            return await warehouse.ToListAsync();
        }

        [Authorize(Roles = "ProviderUser,AdminUser")]
        [HttpPost]
        public async Task<ActionResult<Warehouse>> Add(CreateWarehouseDto dto)
        {
            var warehouse = new Warehouse
            {
                Description = dto.Description,
                Address = dto.Address
            };

            await _efModel.Warehouses.AddAsync(warehouse);
            await _efModel.SaveChangesAsync();

            return warehouse;
        }

        [Authorize(Roles = "ProviderUser,AdminUser")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Warehouse>> Update(int id, CreateWarehouseDto dto)
        {
            var warehouse = await _efModel.Warehouses.FindAsync(id);

            if (warehouse == null)
                return NotFound();

            warehouse.Description = dto.Description;
            warehouse.Address = dto.Address;

            _efModel.Entry(warehouse).State = EntityState.Modified;

            await _efModel.SaveChangesAsync();

            return warehouse;
        }

        [Authorize(Roles = "ProviderUser,AdminUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var warehouse = await _efModel.Warehouses.FindAsync(id);

            if (warehouse == null)
                return NotFound();

            _efModel.Warehouses.Remove(warehouse);
            await _efModel.SaveChangesAsync();

            return Ok();
        }
    }
}
