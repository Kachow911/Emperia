using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class Icarusfish : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Icarusfish");
		}
		public override void SetDefaults()
		{
			Item.width = 34;
			Item.height = 34;
			Item.maxStack = 999;
			Item.value = 1500;
			Item.rare = 1;
		}
	}
}