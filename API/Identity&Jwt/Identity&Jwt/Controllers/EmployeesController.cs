using FluentValidation;
using Identity_Jwt.Data;
using Identity_Jwt.Dto_s.Departments;
using Identity_Jwt.Dto_s.Employees;
using Identity_Jwt.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Identity_Jwt.Controllers
{
    [Route("Employee/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("/GetEmployee")]
        public async Task<IActionResult> GetEmployee(int Id) 
        {
            var Employee = await context.Employees.FindAsync(Id);
            if(Employee == null)
            {
                return NotFound(new {message = "Employee Not Found!!"});
            }
            return Ok(Employee.Adapt<GetEmployeesDto>());
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Employees = await context.Employees.ToListAsync();
            if(Employees.Count == 0)
            {
                return NotFound(new { message = "No Employees Founds!" });
            }
            return Ok(Employees.Adapt<IEnumerable<GetEmployeesDto>>());
        }
        [HttpPost("/CreateEmployee")]
        public async Task<IActionResult> CreateEmployee(EmployeesDto Dto, [FromServices] IValidator<EmployeesDto> Validator)
        {
            var ValidationResults = await Validator.ValidateAsync(Dto);
            if (!ValidationResults.IsValid) {
                var ModelStates = new ModelStateDictionary();
                ValidationResults.Errors.ForEach(error => 
                ModelStates.AddModelError(error.PropertyName, error.ErrorMessage)
                );
                return ValidationProblem(ModelStates);
            }
            await context.Employees.AddAsync(Dto.Adapt<Employees>());
            await context.SaveChangesAsync();
            return Created(nameof(GetEmployee), Dto);
        }
        [HttpDelete("/DeleteEmployee")]
        public async Task<IActionResult>Delete(int Id)
        {
            var Employee = await context.Employees.FindAsync(Id);
            if(Employee == null)
            {
                return NotFound(new { message = "Employee Not Found" });
            }
             context.Employees.Remove(Employee);
            await context.SaveChangesAsync();
            return Ok("Employee Deleted Successfully !!");
        }
        [HttpPut("/UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(int Id, EmployeesDto Dto, [FromServices] IValidator<EmployeesDto> Validator)
        {
            var Employee = await context.Employees.FindAsync(Id);
            if(Employee == null )
            {
                return NotFound(new { message = "Employee Not Found" });
            }
            Employee.Name = Dto.Name;
            Employee.Description = Dto.Description;
            Employee.Department_Id = Dto.Department_Id;
            await context.SaveChangesAsync();
            return Ok(new { Message = "Data Updated Successfully!!" });
        }
    }
}
