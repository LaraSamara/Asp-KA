using Microsoft.EntityFrameworkCore;
using Workshop.Core.Dto_s.Invoice;
using Workshop.Core.Interfaces;
using Workshop.Core.Models;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly AppDbContext context;

        public InvoiceRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> CreateInvoiceAsync(int CustomerId)
        {
            //Step 1: Get Cart Items
            var CartItems = await context.ShoppingCartItems.Where(item => item.Customer_Id == CustomerId)
                .Include(item => item.Items)
                .ToListAsync();
            if (CartItems == null || CartItems.Count == 0)
            {
                return "No Items Found";
            }

            //Step 2: Find Total Price and Unavailable Items
            double TotalPrice = 0;
            var UnAvailableItems = new List<string>();
            foreach (var CartItem in CartItems)
            {
                var Item = await context.InvItemsStores
                    .FirstOrDefaultAsync(item => item.Items_Id == CartItem.Item_Id && item.Stores_Id == CartItem.Store_Id);
                // Add Unavailable Item
                if (Item == null || Item.Balance - Item.RecervedQuantiry < CartItem.Quantity)
                {
                    UnAvailableItems.Add(Item.Items.Name);
                    continue;
                }
                //Update Total Price with Available Item Price
                TotalPrice += (CartItem.Quantity * CartItem.Items.Price);
                //Update Quantity of Available Item
                Item.RecervedQuantiry += CartItem.Quantity;
                context.InvItemsStores.Update(Item);
            }
            // Check if all Items are Unavailable
            if (UnAvailableItems.Count == CartItems.Count)
            {
                return "All Items in Cart are UnAvailable :(";
            }
            // Step 3: create Invoice
            var Invoice = new Invoices
            {
                Custmer_Id = CustomerId,
                CreatedAt = DateTime.Now,
                NetPrice = TotalPrice,
                Transaction_Type = 1,
                Payment_Type = 1,
                IsPosted = true,
                IsClosed = false,
                IsReviewed = false,
            };
            await context.Invoices.AddAsync(Invoice);
            await context.SaveChangesAsync();

            foreach (var CartItem in CartItems)
            {
            // Create Invoice Details for the Available Item
            var InvoiceDetails = new InvoicesDetails
                {
                    Invoice_Id = Invoice.Id,
                    Item_Id = CartItem.Item_Id,
                    Unit_Id = CartItem.Units_Id,
                    Price = CartItem.Items.Price,
                    Quantity = CartItem.Quantity,
                    Factor = 1,
                    CreatedAt = DateTime.Now,
                };
                await context.InvoicesDetails.AddAsync(InvoiceDetails);
            }
            // Step 4: Remove Available Quantity
            context.ShoppingCartItems.RemoveRange(
                CartItems.Where(i => !UnAvailableItems.Contains(i.Items.Name))
                );
            await context.SaveChangesAsync();

            if (UnAvailableItems.Any())
            {
                var UnAvailableItemsMessage = String.Join(", ", UnAvailableItems.Select(
                    async item =>
                    {
                        var CartItem = await context.InvItemsStores.FirstOrDefaultAsync(i => i.Items.Name == item);
                        if (CartItem != null)
                        {
                            return $"{item} (AvailableQuantity: {CartItem.Balance - CartItem.RecervedQuantiry}) ";
                        }
                        return $" Item {item} Unavailable";
                    }));
                return $"Success, Invoice {Invoice.Id} Created Successfully with Price {TotalPrice}, However There is some items not exist {UnAvailableItems}";
            }
            return $"Success, Invoice {Invoice.Id} Created Successfully with Price {TotalPrice}";
        }

        public Task<IEnumerable<InvoiceReciptDto>> GetInvoiceRecipt(int CustomerId, int InvoiceId)
        {
            throw new NotImplementedException();
        }
    }
}
