using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class PolishedSandstone : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Polished Sandstone");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 100;
			Item.rare = ItemRarityID.Blue;
			Item.maxStack = Terraria.Item.CommonMaxStack;
		}
	}
}