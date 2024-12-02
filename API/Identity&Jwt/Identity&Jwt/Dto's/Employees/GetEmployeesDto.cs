using System.ComponentModel.DataAnnotations.Schema;

namespace Identity_Jwt.Dto_s.Employees
{
    public class GetEmployeesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Department_Id { get; set; }
    }
}
