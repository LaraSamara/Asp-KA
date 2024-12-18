using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Workshop.Core.Interfaces;
using Workshop.Core.Models;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly AppDbContext context;

        public IAuthRepository AuthRepository { get; }

        public ICartRepository CartRepository { get; }

        public IInvoiceRepository InvoiceRepository { get; }

        public IItemsRepository ItemsRepository { get; }
        public UnitOfWork(AppDbContext context, UserManager<Users> userManager, SignInManager<Users> signIn, IConfiguration configuration)
        {
            this.context = context;
            AuthRepository = new AuthRepository(userManager, signIn, configuration);
            CartRepository = new CartRepository(context);
            InvoiceRepository = new InvoiceRepository(context);
            ItemsRepository = new ItemsRepository(context);
        }
        public async Task<int> SaveAsync()
        {
            return await context.SaveChangesAsync();
        }
        public void Dispose() { 
            context.Dispose();
        }
    }
}
