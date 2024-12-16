using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class Zones
    {
        public int Id { get; set; }
        public string Name { get; set; }


        [ForeignKey(nameof(Goverments))]
        public int Goverments_Id { get; set; }

        [ForeignKey(nameof(Cities))]
        public int Cities_Id { get; set; }

        public Goverments Goverments { get; set; }
        public Cities Cities { get; set; }
        public ICollection<Users> Users { get; set; } = new HashSet<Users>();
        public ICollection<Stores> Stores { get; set; } = new HashSet<Stores>();
    }
}
