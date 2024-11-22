using CRUD.Data;
using CRUD.DTOs.Department;
using CRUD.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.Intrinsics.Arm;

namespace CRUD.Controllers
{
    [Route("Department")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartmentsController(ApplicationDbContext context) {
            this.context = context;
        }
        [HttpGet("/GetAllDepartment")]
        public IActionResult GetAll()
        {
            var departments = context.Departments.Select(
                dep => new GetDepartmentDto
                {
                    Id = dep.Id,
                    Name = dep.Name,
                }).ToList();
            return Ok(departments);
        }
        [HttpGet("/GetDepartment")]
        public IActionResult Get(int Id)
        {
            var department = context.Departments.Find(Id);
            if (department == null)
            {
                return NotFound();
            }
            var model = new GetDepartmentDto
            {
                Id = department.Id,
                Name = department.Name,
            };
            return Ok(model);
        }
        [HttpPost("/CreateDepartment")]
        public IActionResult Create(DepartmentDto Dto)
        {
            var department = new Department
            {
                Name = Dto.Name,
            };
            context.Departments.Add(department);
            context.SaveChanges();
            return CreatedAtAction(nameof(Get),Dto);
        }
        [HttpDelete("/DeleteDepartment")]
        public IActionResult Delete(int Id)
        {
            var department = context.Departments.Find(Id);
            if (department == null)
            {
                return NotFound();
            }
            context.Departments.Remove(department);
            context.SaveChanges();
            return Ok("Success");
        }
        [HttpPut("/UpdateDepartment")]
        public IActionResult Update(int Id, DepartmentDto department)
        {
            var dep = context.Departments.Find(Id);
            if (dep == null)
            {
                return NotFound();
            }
            dep.Name = department.Name;
            context.SaveChanges();
            return Ok("Success");
        }
    }
}
