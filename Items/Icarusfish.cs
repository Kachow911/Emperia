using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class Icarusfish : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Icarusfish");
		}
		public override void SetDefaults()
		{
			item.width = 34;
			item.height = 34;
			item.maxStack = 999;
			item.value = 1500;
			item.rare = 1;
		}
	}
}