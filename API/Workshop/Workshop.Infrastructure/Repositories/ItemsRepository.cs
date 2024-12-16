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

        public async Task<IEnumerable<ItemsDto>> GetItemsAsync()
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
            var Items = await context.Items
                               .ProjectToType<ItemsDto>(Config)
                               .ToListAsync();

            return Items;
        }
    }
}
