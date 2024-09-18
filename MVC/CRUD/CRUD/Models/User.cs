using System.ComponentModel.DataAnnotations;

namespace CRUD.Models
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [DataType(DataType.EmailAddress)]
        [Required]
        public string Email { get; set; } = null!;
        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; } = null!;
        public bool IsActived { get; set; } = true;
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
