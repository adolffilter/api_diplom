﻿using Diploma.Database;
using Diploma.model.provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
            IQueryable<Provider> providers = _efModel.Providers
                .Include(u => u.Post);

            if (!string.IsNullOrEmpty(search))
            {
                var q = search.ToLower().Trim();

                providers = providers.Where(u => u.FirstName.ToLower().Contains(search)
                || u.LastName.ToLower().Contains(search)
                || u.MidleName.ToLower().Contains(search));
            }

            return await providers.ToListAsync();
        }

        [HttpGet("Post")]
        public async Task<ActionResult<List<ProviderPost>>> GetAllPost()
        {
            return await _efModel.ProviderPosts.ToListAsync();
        }

        [Authorize(Roles = "AdminUser")]
        [HttpPost]
        public async Task<ActionResult<Provider>> Add(CreateProviderDTO dto)
        {
            var user = await _efModel.Users.FindAsync(dto.UserId);

            if (user == null)
                return NotFound();

            var post = await _efModel.ProviderPosts.FindAsync(dto.PostId);

            if (post == null)
                return NotFound();

            var provider = new Provider
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                MidleName = user.MidleName,
                Login = user.Login,
                Password = user.Password,
                Photo = user.Photo,
                Post = post
            };

            _efModel.Users.Remove(user);
            await _efModel.Providers.AddAsync(provider);
            await _efModel.SaveChangesAsync();

            return provider;
        }

        [Authorize(Roles = "AdminUser")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Provider>> UpdateProvider(int id, UpdateProviderDTO dto)
        {
            var provider = await _efModel.Providers.FindAsync(id);

            if (provider == null)
                return NotFound();

            provider.FirstName = dto.FirstName;
            provider.LastName = dto.LastName;
            provider.MidleName = dto.MidleName;
            provider.Login = dto.Login;

            _efModel.Entry(provider).State = EntityState.Modified;
            await _efModel.SaveChangesAsync();

            return provider;
        }

        [Authorize(Roles = "AdminUser")]
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
