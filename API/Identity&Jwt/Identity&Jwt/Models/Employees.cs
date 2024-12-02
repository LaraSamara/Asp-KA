using System.ComponentModel.DataAnnotations.Schema;

namespace Identity_Jwt.Models
{
    public class Employees
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [ForeignKey(nameof(Departments))]
        public int Department_Id { get; set; }
        public Departments Departments { get; set; }

    }
}
