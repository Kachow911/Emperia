using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Desert
{
	public class PolishedSandstone : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Polished Sandstone");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 100;
			item.rare = 1;
		}
	}
}