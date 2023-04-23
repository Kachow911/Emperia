using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class DesertEye : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Arid Eye");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.value = 1000;
			Item.rare = ItemRarityID.Green;
			Item.maxStack = Terraria.Item.CommonMaxStack;
		}
	}
}