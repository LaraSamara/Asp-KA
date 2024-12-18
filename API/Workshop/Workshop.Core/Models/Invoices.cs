using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Models
{
    public class Invoices
    {
        public int Id { get; set; }
        [ForeignKey(nameof(Users))]
        public int Custmer_Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public double NetPrice { get; set; }
        public int Transaction_Type { get; set; }
        public int Payment_Type { get; set; }
        public bool IsPosted { get; set; }
        public bool IsReviewed { get; set; }
        public bool IsClosed { get; set; }

        public IEnumerable<InvoicesDetails> InvoicesItems { get; set; } = new HashSet<InvoicesDetails>();
        public Users Users { get; set; }
    }
}
