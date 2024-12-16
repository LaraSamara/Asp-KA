using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class InvItemsStores
    {
        [ForeignKey(nameof(Items))]
        public int Items_Id { get; set; }
        [ForeignKey(nameof(Stores))]
        public int Stores_Id { get; set; }
        [ForeignKey(nameof(Units))]
        public int Units_Id { get; set; }

        public double Balance { get; set; }
        // small units of Balance
        public int Factor {  get; set; }
        public double RecervedQuantiry {  get; set; }
        public DateTime LastUpdate { get; set; }
        public Units Units { get; set; }
        public Items Items { get; set; }
        public Stores Stores { get; set; }

    }
}
