using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Frostleaf
{
	public class Frostleaf : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Frostleaf");
			Tooltip.SetDefault("’Might give you a rash’");
		}
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.maxStack = 999;
			item.value = 5750;
			item.rare = 1;
		}
	}
}
