using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Dto_s.Cart;
using Workshop.Core.Interfaces;
using Workshop.Core.Models;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext context;

        public CartRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<string> AddBulkQuantityToCartAsync(CartItemDto Dto, int UserId)
        {
           var Item = await context.Items.FindAsync(Dto.ItemId);
            var Store = await context.Stores.FindAsync(Dto.Store_id);
            if(Item == null || Store == null) {
                return "Store or Item Not Found";
            }
            var Cart =  context.ShoppingCartItems
                .FirstOrDefault(cart => cart.Item_Id == Dto.ItemId && cart.Store_Id == Dto.Store_id && cart.Customer_Id == UserId);
            if(Cart == null)
            {
                var ShoppingCart = new ShoppingCartItems
                {
                    Customer_Id = UserId,
                    Store_Id = Dto.Store_id,
                    Item_Id = Dto.ItemId,
                    Quantity = Dto.Quantity,
                    Units_Id = Dto.UnitId,
                    UpdateeAt = DateTime.Now,
                };
                await context.ShoppingCartItems.AddAsync(ShoppingCart);
            }
            else
            {
                Cart.Quantity = Dto.Quantity;
                Cart.UpdateeAt = DateTime.Now;
                Cart.Units_Id = Dto.UnitId;
                Cart.Store_Id = Dto.Store_id;
            }
            await context.SaveChangesAsync();
            return "Item Add To Cart Successfully";
        }

        public async Task<string> AddOneQuantityToCartAsync(CartItemDto Dto, int UserId)
        {
            var Store = await context.Stores.FindAsync(Dto.Store_id);
            var Item = await context.Items.FindAsync(Dto.ItemId);
            if(Store == null || Item == null) {
                return "Item or Store not Exist";
            }
            var Cart = await context.ShoppingCartItems
                .FirstOrDefaultAsync(cart => cart.Item_Id == Dto.ItemId && cart.Store_Id == Dto.Store_id && cart.Customer_Id == UserId);
            var Message = "";
            if(Cart == null)
            {
                var NewCart = new ShoppingCartItems {
                    Customer_Id = UserId,
                    Store_Id = Dto.Store_id,
                    Item_Id = Dto.ItemId,
                    Units_Id = Dto.UnitId,
                    Quantity = 1,
                    CreatedAt = DateTime.Now,   
                };
                await context.ShoppingCartItems.AddAsync(NewCart);
                Message = "Success, New Item With Quantity one added Successfully";
            }
            else
            {
                Cart.Quantity += 1;
                Cart.UpdateeAt = DateTime.Now;
                Message = "Success, Items Quantity Updated by One Successfuly";
            }
            await context.SaveChangesAsync();
            return Message;
        }

        public async Task<IEnumerable<UserCartItemDto>> GetAllItemsFromCartAsync(int UserId)
        {
            var Items = await context.ShoppingCartItems.Where(item => item.Customer_Id == UserId)
                .Include(item => item.Items)
                .Include(item => item.Units)
                .ToListAsync();

            var ItemsDto = Items.Select(item => new UserCartItemDto
            {
                Name = item.Items.Name,
                Quantity = item.Quantity,
                Unit = item.Units.Name,
                Price = item.Items.Price,
            }).ToList();
            return ItemsDto;
        }
    }
}
