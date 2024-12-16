using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class ItemsUnits
    {
        [ForeignKey(nameof(Items))]
        public int Items_Id { get; set; }
        [ForeignKey(nameof(Units))]
        public int Units_Id { get; set; }

        public int Factor { get; set; }
        public Items Items { get; set; }
        public Units Units { get; set; }
    }
}
