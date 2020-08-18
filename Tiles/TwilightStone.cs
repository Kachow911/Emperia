using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class TwilightStone : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileValue[Type] = 805;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileMerge[Type][mod.TileType("TwilightGrass")] = true;
			Main.tileBlendAll[this.Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = mod.ItemType("GrottoStone");
			AddMapEntry(new Color(51, 75, 102));
			mineResist = 2f;
			soundType = 21;
			dustType = 121;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}


	}
}