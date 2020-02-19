using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.PreHardmode.Seashell
{
	public class SeaCrystal : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sea Crystal");
		}
		public override void SetDefaults()
		{
			item.width = 16;
			item.height = 20;
			item.maxStack = 999;
			item.value = 20000;
			item.rare = 1;
		}
	}
}