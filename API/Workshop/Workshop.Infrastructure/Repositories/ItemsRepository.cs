using Mapster;
using Microsoft.EntityFrameworkCore;
using Workshop.Core.Dto_s.Items;
using Workshop.Core.Interfaces;
using Workshop.Core.Mapping;
using Workshop.Infrastructure.Data;

namespace Workshop.Infrastructure.Repositories
{
    public class ItemsRepository: IItemsRepository
    {
        private readonly AppDbContext context;

        public ItemsRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<PagedResponse> GetItemsAsync(int PageIndex, int PageSize)
        {
            // Manual 
            /*
            var Items = await context.Items
                .Include(U => U.ItemsUnits)
                .Select(
                Item => new ItemsDto {
                    Id = Item.Id,
                    Name = Item.Name,
                    Description = Item.Description,
                    Price = Item.Price,
                    ItemsUnits = Item.ItemsUnits.Select(Unit => Unit.Units.Name).ToList(),
                    Stores = Item.InvItemsStores.Select(Store => Store.Stores.Name).ToList(),
                })
                .ToListAsync();
            */
            // Mapster
            var Config = MappingProfile.Config;
            var Items =  context.Items
                               .ProjectToType<ItemsDto>(Config)
                               .AsQueryable();
            return await PaginationAsync(Items, PageIndex, PageSize);

        }
        private async Task<PagedResponse> PaginationAsync(IQueryable<ItemsDto> Quary, int PageIndex, int PageSize)
        {
            var Skip = (PageIndex - 1) * PageSize;
            var Items = await Quary.Skip(Skip).Take(PageSize).ToListAsync();
            var Dto = new PagedResponse
            {
                PageIndex = PageIndex,
                PageSize = PageSize,
                TotalItems = Quary.Count(),
                Items = Items
            };
            return Dto;
        }
    }
}
