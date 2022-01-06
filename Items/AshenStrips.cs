using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class AshenStrips : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ashen Strip");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 999;
			Item.value = 1000;
			Item.rare = 2;
		}
	}


}