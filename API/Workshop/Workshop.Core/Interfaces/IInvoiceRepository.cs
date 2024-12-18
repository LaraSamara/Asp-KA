using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Dto_s.Invoice;

namespace Workshop.Core.Interfaces
{
    public interface IInvoiceRepository
    {
        Task<string> CreateInvoiceAsync(int CustomerId);
        Task<InvoiceRecipetDto> GetInvoiceRecipt(int CustomerId, int InvoiceId);
    }
}
