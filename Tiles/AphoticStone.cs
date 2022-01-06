using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class AphoticStone : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = false;
			//ItemDrop = ModContent.ItemType<Tiles.AbyssalStone>();
			AddMapEntry(new Color(73, 64, 64));
			MineResist = 5f;
			MinPick = 209;
			SoundType = 21;
			Main.tileSpelunker[Type] = false;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return false;
		}


	}
}