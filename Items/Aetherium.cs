using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class Aetherium : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Aetherium");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 1000;
			Item.rare = 1;
		}
	}
}