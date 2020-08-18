using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class TwilightBrick : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileValue[Type] = 805;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = mod.ItemType("GrottoBrick");
			AddMapEntry(new Color(77, 98, 148));
			mineResist = 1f;
			soundType = 21;
			dustType = 121;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}
	}
}