using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
	public class Frostleaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Frostleaf");
			// Tooltip.SetDefault("’Might give you a rash’");
		}
		public override void SetDefaults()
		{
			Item.width = 18;
			Item.height = 18;
			Item.maxStack = 999;
			Item.value = 5750;
			Item.rare = 1;
		}
	}
}
