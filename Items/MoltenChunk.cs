using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items
{
	public class MoltenChunk : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Molten Chunk");
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