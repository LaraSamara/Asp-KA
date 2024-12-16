using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class CustomersStores
    {
        [ForeignKey(nameof(Users))]
        public int Customer_Id { get; set; }
        [ForeignKey(nameof(Stores))]
        public int Store_Id { get; set; }

        public Stores Stores { get; set; }
        public Users Users { get; set; }
    }
}
