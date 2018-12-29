using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class GloomStone : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileValue[Type] = 805;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = mod.ItemType("GloomStone");
			AddMapEntry(new Color(70, 29, 104));
			mineResist = 4f;
			minPick = 209;
			soundType = 21;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}


	}
}