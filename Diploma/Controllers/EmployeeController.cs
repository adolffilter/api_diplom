using Diploma.Database;
using Diploma.model.employee;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Diploma.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private EfModel _efModel;

        public EmployeeController(EfModel model)
        {
            _efModel = model;
        }

        [HttpGet]
        public async Task<List<Employee>> GetAllProvider(string? search)
        {
            IQueryable<Employee> employee = _efModel.Employees
                .Include(u => u.Warehouse);

            if (!string.IsNullOrEmpty(search))
            {
                var q = search.ToLower().Trim();

                employee = employee.Where(u => u.FirstName.ToLower().Contains(search)
                || u.LastName.ToLower().Contains(search)
                || u.MidleName.ToLower().Contains(search));
            }

            return await employee.ToListAsync();
        }

        [Authorize(Roles = "AdminUser")]
        [HttpPost]
        public async Task<ActionResult<Employee>> Add(CreateEmployeeDto dto)
        {
            var warehouse = await _efModel.Warehouses.FindAsync(dto.WarehouseId);

            if (warehouse == null)
                return NotFound();

            var user = await _efModel.Users.FindAsync(dto.UserId);

            if (user == null)
                return NotFound();

            var employee = new Employee
            {
                PhoneNumber = dto.PhoneNumber,
                Address = dto.Address,
                Warehouse = warehouse,
                Login = user.Login,
                Password = user.Password,
                Photo = user.Photo,
                FirstName = user.FirstName,
                LastName = user.LastName,
                MidleName = user.MidleName,
            };

            _efModel.Users.Remove(user);
            await _efModel.Employees.AddAsync(employee);
            await _efModel.SaveChangesAsync();

            return employee;
        }

        [Authorize(Roles = "AdminUser")]
        [HttpPut("{id}")]
        public async Task<ActionResult<Employee>> UpdateProvider(int id, UpdateEmployeeDto dto)
        {
            var employee = await _efModel.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            var warehouse = await _efModel.Warehouses.FindAsync(dto.WarehouseId);

            if (warehouse == null)
                return NotFound();

            employee.PhoneNumber = dto.PhoneNumber;
            employee.Address = dto.Address;
            employee.Warehouse = warehouse;

            _efModel.Entry(employee).State = EntityState.Modified;
            await _efModel.SaveChangesAsync();

            return employee;
        }

        [Authorize(Roles = "AdminUser")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var employee = await _efModel.Employees.FindAsync(id);

            if (employee == null)
                return NotFound();

            _efModel.Employees.Remove(employee);
            await _efModel.SaveChangesAsync();

            return Ok();
        }
    }
}
