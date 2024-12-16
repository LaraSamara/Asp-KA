using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workshop.Core.Dto_s.Cart;

namespace Workshop.Core.Interfaces
{
    public interface ICartRepository
    {
        Task<string> AddBulkQuantityToCartAsync(CartItemDto Dto, int UserId);
        Task<string> AddOneQuantityToCartAsync(CartItemDto Dto, int UserId);
        Task<IEnumerable<UserCartItemDto>> GetAllItemsFromCartAsync(int UserId);
    }
}
