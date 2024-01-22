using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectManagment.Data;
using ProjectManagment.Models;
using ProjectManagment.Models.Dto;

namespace ProjectManagment.Controllers
{
    [ApiController]
    [Route("api/employee")]

    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context;
        
        public EmployeeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> GetEmployeeById(int id)
        {
            var project = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);

            if(project != null)
            {
                return Ok(project);
            }
            return NotFound(id);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> CreateEmployee([FromBody] CreateEmployeeDto employeeDto )
        {
            var employee = new Employee()
            {
                FirstName = employeeDto.FirstName,
                LastName = employeeDto.LastName,
                Email = employeeDto.Email,
                Patronymic = employeeDto.Patronymic
            };

            context.Employees.Add(employee);
            await context.SaveChangesAsync();

            return Ok(employee);
        }



        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> UpdateEmployee([FromBody] UpdateEmployeeDto employeeDto)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == employeeDto.EmployeeId);

            if (employee == null)
                return NotFound($"Не найден сотрудник с Id = {employeeDto.EmployeeId}");

            employee.FirstName = employeeDto.FirstName;
            employee.LastName = employeeDto.LastName;
            employee.Email = employeeDto.Email;
            employee.Patronymic = employeeDto.Patronymic;

            context.Employees.Update(employee);

            await context.SaveChangesAsync();

            return Ok(employee);
        }



        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            var employee = await context.Employees.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if(employee == null)
            {
                return NotFound(id);
            }

            context.Employees.Remove(employee);
            await context.SaveChangesAsync();
            return Ok(id);
        }
    }
}
