namespace Identity_Jwt.Models
{
    public class Departments
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Employees> Employees { get; set; }
    }
}
