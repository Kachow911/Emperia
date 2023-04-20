using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Jungle
{
	public class JungleMaterial : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Botanical Element");
		}
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
						Item.value = 100;
			Item.rare = ItemRarityID.Blue;
		}
	}
}