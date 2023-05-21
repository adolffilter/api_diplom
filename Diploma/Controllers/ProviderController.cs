using Diploma.Database;
using Diploma.model.provider;
using Diploma.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProviderController : ControllerBase
    {
        private EfModel _efModel;

        public ProviderController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet]
        public async Task<List<Provider>> GetAllProvider(string? search)
        {
            IQueryable<Provider> providers = _efModel.Providers;

            if(!string.IsNullOrEmpty(search))
            {
                var q = search.ToLower().Trim();

                providers = providers.Where(u => u.FirstName.ToLower().Contains(search) 
                || u.LastName.ToLower().Contains(search) 
                || u.MidleName.ToLower().Contains(search));
            }

            return await providers.ToListAsync();
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Provider>> Add(UpdateProviderDTO dto)
        {
            var provider = new Provider
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                MidleName = dto.MidleName,
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address
            };

            await _efModel.Providers.AddAsync(provider);
            await _efModel.SaveChangesAsync();

            return provider;
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<Provider>> UpdateProvider(int id, UpdateProviderDTO dto)
        {
            var provider = await _efModel.Providers.FindAsync(id);

            if (provider == null)
                return NotFound();

            provider.FirstName = dto.FirstName;
            provider.LastName = dto.LastName;
            provider.MidleName = dto.MidleName;
            provider.PhoneNumber = dto.PhoneNumber;
            provider.Address = dto.Address;

            _efModel.Entry(provider).State = EntityState.Modified;
            await _efModel.SaveChangesAsync();

            return provider;
        }

        [Authorize]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var provider = await _efModel.Providers.FindAsync(id);

            if (provider == null)
                return NotFound();

            _efModel.Providers.Remove(provider);
            await _efModel.SaveChangesAsync();

            return Ok();
        }
    }
}
