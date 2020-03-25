using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class DesertEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arid Eye");
		}
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			item.maxStack = 999;
			item.value = 1000;
			item.rare = 2;
		}
	}
}