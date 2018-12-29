using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class Aetherium : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileValue[Type] = 805;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			drop = mod.ItemType("Aetherium");
			AddMapEntry(new Color(117, 241, 255));
			mineResist = 5f;
			minPick = 60;
			soundType = 21;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}


	}
}