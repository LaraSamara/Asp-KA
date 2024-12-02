using FluentValidation;
using Identity_Jwt.Data;
using Identity_Jwt.Dto_s.Departments;
using Identity_Jwt.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Identity_Jwt.Controllers
{
    [Route("Departments/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("/GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var Departments = await context.Departments.ToListAsync();
            if (Departments.Count == 0)
            {
                return NotFound(new {message = "No Departments Found!!" });
            }

            return Ok(Departments.Adapt<IEnumerable<GetDepartmentsDto>>());
        }
        [HttpGet("/GetDepartment")]
        public async Task<IActionResult> GetDepartment(int Id)
        {
            var Department = await context.Departments.FindAsync(Id);
            if(Department == null)
            {
                return NotFound("Department not Found");
            }
            return Ok(Department.Adapt<GetDepartmentsDto>());
        }
        [HttpPost("/Create")]
        [Authorize]
        public async Task<IActionResult> CreateDepartment(DepartmentsDto Dto, [FromServices] IValidator<DepartmentsDto> Validator)
        {
            var ValidationResults = await Validator.ValidateAsync(Dto);
            if (!ValidationResults.IsValid)
            {
                var ModelStates = new ModelStateDictionary();
                ValidationResults.Errors.ForEach(error =>
                ModelStates.AddModelError(error.PropertyName, error.ErrorMessage)
                );
                return ValidationProblem(ModelStates);
            }
            await context.Departments.AddAsync(Dto.Adapt<Departments>());
            await context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetDepartment), Dto);
        }
        [HttpDelete("/Delete")]
        public async Task<IActionResult> DeleteDepartment(int Id)
        {
            var Department = await context.Departments.FindAsync(Id);
            if(Department == null)
            {
                return NotFound(new { message = "Department Not found!!" });
            }
            context.Departments.Remove(Department);
            await context.SaveChangesAsync();
            return Ok(new { message = "Department Deleted Successfully" });

        }
        [HttpPut("/UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentsDto Dto, [FromServices] IValidator<DepartmentsDto> Validator, int Id)
        {
            var Department = await context.Departments.FindAsync(Id);
            if(Department == null)
            {
                return NotFound(new {message = "Department Not Found!!"});
            }
            Department.Name = Dto.Name;
            await context.SaveChangesAsync();
            return Ok("Data Updated Successfully!!");
        }
    }
}
