using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles.Volcano
{
	public class VolcanoTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = false;
			//ItemDrop = ModContent.ItemType<AbyssalStone>();
			AddMapEntry(new Color(109, 72, 16));
			MineResist = 7f;
			MinPick = 100;
			SoundType = 21;
			Main.tileSpelunker[Type] = false;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return false;
		}


	}
}