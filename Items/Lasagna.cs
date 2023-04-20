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
            // DisplayName.SetDefault("Lasagna");
            // Tooltip.SetDefault("Mondays are the worst");
        }

        public override void SetDefaults()
        {
            Item.width = 28;
            Item.height = 30;
            Item.useStyle = 2;
            Item.useAnimation = 17;
            Item.useTime = 17;
            Item.useTurn = true;
            Item.UseSound = SoundID.Item3;
            Item.maxStack = 30;
            Item.consumable = true;
            Item.rare = 1;
            Item.value = 200;
            Item.buffType = BuffID.WellFed;
            Item.buffTime = 108000;
            //Item.createTile = ModContent.TileType<TwilightLantern>();
        }
    }
}
