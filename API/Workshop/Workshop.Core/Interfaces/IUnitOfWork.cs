using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Workshop.Core.Interfaces
{
    public interface IUnitOfWork
    {
        public IAuthRepository AuthRepository { get; }
        public ICartRepository CartRepository { get; }
        public IInvoiceRepository InvoiceRepository { get; }
        public IItemsRepository ItemsRepository { get; }
        Task<int> SaveAsync();
    }
}
