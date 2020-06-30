using Emperia.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	public class Lasagna : ModItem
	{
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lasagna");
            Tooltip.SetDefault("Mondays are the worst");
        }

        public override void SetDefaults()
        {
            item.width = 28;
            item.height = 30;
            item.useStyle = 2;
            item.useAnimation = 17;
            item.useTime = 17;
            item.useTurn = true;
            item.UseSound = SoundID.Item3;
            item.maxStack = 30;
            item.consumable = true;
            item.rare = 1;
            item.value = 200;
            item.buffType = BuffID.WellFed;
            item.buffTime = 108000;
            //item.createTile = ModContent.TileType<TwilightLantern>();
        }
    }
}
