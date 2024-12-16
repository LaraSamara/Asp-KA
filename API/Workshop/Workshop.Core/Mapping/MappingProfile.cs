using Mapster;
using Workshop.Core.Dto_s.Items;
using Workshop.Core.Models;

namespace Workshop.Core.Mapping
{
    public class MappingProfile
    {
        private static readonly TypeAdapterConfig _Config = new TypeAdapterConfig();
        static MappingProfile() {
            _Config.NewConfig<Items, ItemsDto>()
                .Map(dest => dest.ItemsUnits, src => src.ItemsUnits.Select(Unite => Unite.Units.Name).ToList())
                .Map(dest => dest.Stores, src => src.InvItemsStores.Select(Store => Store.Stores.Name).ToList());
        }
        // read-only static property (Getter Method To Achive Encapsulation)
        public static TypeAdapterConfig Config => _Config;
    }
}
