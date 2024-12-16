using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class InvoicesDetails
    {
        [ForeignKey(nameof(Invoices))]
        public int Invoice_Id { get; set; }
        [ForeignKey(nameof(Items))]
        public int Item_Id { get; set; }
        [ForeignKey(nameof(Units))]
        public int Unit_Id { get; set; }    
        public double Price { get; set; }
        public double Quantity { get; set; }
        public int Factor { get; set; }
        public DateTime CreatedAt { get; set; }

        public Invoices Invoices { get; set; }
        public Items Items { get; set; }
        public Units Units { get; set; }
    }
}
