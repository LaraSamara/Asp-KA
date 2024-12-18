using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Dto_s.Invoice
{
    public class InvoiceRecipetDto
    {
        public int InvoiceId { get; set; }  
        public double NetPrice { get; set; }
        public int CustomerId { get; set; }
        public DateTime CreatedAt { get; set; }
        public IEnumerable<InvoiceItemDto> InvoiceItems { get; set; }
    }
}
