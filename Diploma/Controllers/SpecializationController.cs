using Diploma.Database;
using Diploma.model.specialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SpecializationController: ControllerBase
{
    private EfModel _efModel;

    public SpecializationController(EfModel model)
    {
        _efModel = model;
    }

    [HttpGet]
    public async Task<ActionResult<List<Specialization>>> GetAll()
    {
        var specializations = await _efModel.Specializations.ToListAsync();

        return specializations;
    }

    [Authorize(Roles = "AdminUser")]
    [HttpPost]
    public async Task<ActionResult<Specialization>> Create(string title, int salary)
    {
        var specialization = new Specialization
        {
            Title = title,
            Salary = salary
        };

        await _efModel.Specializations.AddAsync(specialization);
        await _efModel.SaveChangesAsync();

        return specialization;
    }
}