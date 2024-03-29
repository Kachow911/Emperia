using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Tiles
{
	public class TwilightStone : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileMerge[Type][TileType<Tiles.TwilightGrass>()] = true;
			Main.tileBlendAll[this.Type] = true;
			Main.tileBlockLight[Type] = true;
			AddMapEntry(new Color(51, 75, 102));
			MineResist = 2f;
			HitSound = SoundID.Tink;
			DustType = 121;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}


	}
}