using Diploma.Database;
using Diploma.model.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Numerics;
using System.Security.Claims;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private EfModel _efModel;

        public DoctorController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet]
        public async Task<ActionResult<List<Doctor>>> GetAll()
        {
            return await _efModel.Doctors.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Doctor>> GetById(int id)
        {
            var doctor = await _efModel.Doctors.FindAsync(id);

            if (doctor == null)
                return NoContent();

            return doctor;
        }

        [HttpGet("Post")]
        public async Task<ActionResult<List<PostDoctor>>> GetAllPost(int? authorId = null)
        {
            IQueryable<PostDoctor> posts = _efModel.PostDoctors.Include(u => u.Author);

            if(authorId != null)
            {
                posts = posts.Where(u => u.Author.Id == authorId);
            }

            return await posts.ToListAsync();
        }

        [Authorize(Roles = "DoctorUser")]
        [HttpPost("Post")]
        public async Task<ActionResult<PostDoctor>> DoctorAddPost(string title, string descriptin)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;

            if (identity == null)
                return NotFound();

            int idUser = Convert.ToInt32(identity.FindFirst("Id").Value);

            var author = await _efModel.Doctors.FindAsync(idUser);

            if (author == null)
                return NoContent();

            var post = new PostDoctor
            {
                Title = title,
                Descriptin = descriptin,
                Author = author
            };

            await _efModel.PostDoctors.AddAsync(post);
            await _efModel.SaveChangesAsync();

            return post;
        }
    }
}
