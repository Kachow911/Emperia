using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class OsmiumOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Osmium>();
			AddMapEntry(new Color(142, 156, 171));
			MineResist = 1f;
			SoundType = 21;
			DustType = 121;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return false;
		}
	}
}