using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class Units
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<ItemsUnits> ItemsUnits { get; set; } = new HashSet<ItemsUnits>();
        public ICollection<InvItemsStores> InvItemsStores { get; set; } = new HashSet<InvItemsStores>();
        public ICollection<ShoppingCartItems> shoppingCartItems { get; set; } = new HashSet<ShoppingCartItems>();   
        public ICollection<InvoicesDetails> InvoicesDetails { get; set; }= new HashSet<InvoicesDetails>();

    }
}
