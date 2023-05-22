using Diploma.Database;
using Diploma.model.supply;
using Diploma.model.warehouse;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SupplyController : ControllerBase
    {
        private EfModel _efModel;

        public SupplyController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet]
        public async Task<ActionResult<List<Supply>>> GetAll(string? search)
        {
            IQueryable<Supply> supplies = _efModel.Supplies
                .Include(u => u.Product)
                .Include(u => u.Warehouse)
                .Include(u => u.Provider);

            if (!string.IsNullOrEmpty(search))
            {
                supplies = supplies.Where(u => u.Product.Name.Contains(search) || u.Product.Description.Contains(search));
            }

            return await supplies.ToListAsync();
        }

        [Authorize(Roles = "EmployeeUser,ProviderUser,AdminUser")]
        [HttpPost]
        public async Task<ActionResult<Supply>> Add(CreateSupplyDto dto)
        {
            var product = await _efModel.Products.FindAsync(dto.ProductId);

            if (product == null)
                return NotFound();

            var provider = await _efModel.Providers.FindAsync(dto.ProviderId);

            if (provider == null)
                return NotFound();

            var warehouse = await _efModel.Warehouses.FindAsync(dto.WarehouseId);

            if (warehouse == null)
                return NotFound();

            var supply = new Supply
            {
                Quantity = dto.Quantity,
                Product = product,
                Provider = provider,
                Warehouse = warehouse
            };

            await _efModel.Supplies.AddAsync(supply);
            await _efModel.SaveChangesAsync();

            return supply;
        }

        [Authorize(Roles = "EmployeeUser,ProviderUser,AdminUser")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Supply>> Update(int id, CreateSupplyDto dto)
        {
            var supply = await _efModel.Supplies.FindAsync(id);

            if (supply == null)
                return NotFound();

            var product = await _efModel.Products.FindAsync(dto.ProductId);

            if (product == null)
                return NotFound();

            var provider = await _efModel.Providers.FindAsync(dto.ProviderId);

            if (provider == null)
                return NotFound();

            var warehouse = await _efModel.Warehouses.FindAsync(dto.WarehouseId);

            if (warehouse == null)
                return NotFound();

            supply.Quantity = dto.Quantity;
            supply.Product = product;
            supply.Provider = provider;
            supply.Warehouse = warehouse;

            _efModel.Entry(supply).State = EntityState.Modified;

            await _efModel.SaveChangesAsync();

            return supply;
        }

        [Authorize(Roles = "EmployeeUser,ProviderUser,AdminUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var supply = await _efModel.Supplies.FindAsync(id);

            if (supply == null)
                return NotFound();

            _efModel.Supplies.Remove(supply);
            await _efModel.SaveChangesAsync();

            return Ok();
        }
    }
}
