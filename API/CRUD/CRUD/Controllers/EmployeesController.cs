using CRUD.Data;
using CRUD.DTOs.Employee;
using CRUD.Models;
using Mapster;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CRUD.Controllers
{
    [Route("Employee")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmployeesController(ApplicationDbContext context)
        {
            this.context = context;
        }
        [HttpGet("/GetAllEmployee")]
        public IActionResult GetAll() {
            var employees = context.Employees.ToList();
            return Ok(employees.Adapt<IEnumerable<GetEmployeeDto>>());
        }
        [HttpGet("/GetEmployee")]
        public IActionResult Get(int Id)
        {
            var employee = context.Employees.Find(Id);
            if(employee is null)
            {
                return NotFound();
            }
            return Ok(employee.Adapt<GetEmployeeDto>());
        }
        [HttpPost("/CreateEmployee")]
        public IActionResult Create(EmployeeDto request)
        {
            context.Employees.Add(request.Adapt<Employee>()); 
            context.SaveChanges();
            return Ok("Success");
        }
        [HttpDelete("/DeleteEmployee")]
        public IActionResult Delete(int Id)
        {
            var employee = context.Employees.Find(Id);
            if(employee is null)
            {
                return NotFound();
            }
            context.Employees.Remove(employee);
            context.SaveChanges();
            return Ok("Success");
        }
        [HttpPut("/UpdateEmployee")]
        public IActionResult Update(int Id, EmployeeDto request)
        {
            var employee = context.Employees.Find(Id);
            if(employee is null)
            {
                return NotFound();
            }
            employee.Name = request.Name;
            employee.Description = request.Description;
            employee.DepartmentId = request.DepartmentId;
            context.SaveChanges();
            return Ok("Success");
        }
    }
}
