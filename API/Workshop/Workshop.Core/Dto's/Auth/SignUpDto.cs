using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Models;

namespace Workshop.Core.Dto_s.Auth
{
    public class SignUpDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public int Goverments_Id { get; set; }
        [Required]
        public int Cities_Id { get; set; }
        [Required]
        public int Zones_Id { get; set; }
        [Required]
        public int Classifications_Id { get; set; }
    }
}
