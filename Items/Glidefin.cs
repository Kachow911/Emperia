using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class Glidefin : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Glidefin");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 15000;
			item.rare = 4;
		}
	}
}