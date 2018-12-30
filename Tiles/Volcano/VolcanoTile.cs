using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles.Volcano
{
	public class VolcanoTile : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileValue[Type] = 805;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = false;
			//drop = mod.ItemType("AbyssalStone");
			AddMapEntry(new Color(109, 72, 16));
			mineResist = 7f;
			minPick = 100;
			soundType = 21;
			Main.tileSpelunker[Type] = false;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return false;
		}


	}
}