using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Sets.Hardmode.Jungle
{
	public class JungleMaterial : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Botanical Element");
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