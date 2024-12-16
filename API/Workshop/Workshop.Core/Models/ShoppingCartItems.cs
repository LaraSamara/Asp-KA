using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    // Middle point between store and customer, customer and Items
    public class ShoppingCartItems
    {
        [ForeignKey(nameof(User))]
        public int Customer_Id { get; set; }
        [ForeignKey(nameof(Stores))]
        public int Store_Id { get; set; }
        [ForeignKey(nameof(Items))]
        public int Item_Id { get; set; }
        [ForeignKey(nameof(Units))]
        public int Units_Id { get; set; }
        
        public double Quantity { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdateeAt { get; set; }

        public Users User { get; set; }
        public Stores Stores { get; set; }
        public Items Items { get; set; }
        public Units Units { get; set; }
    }
}
