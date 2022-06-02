using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class GloomStone : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			//ItemDrop = ModContent.ItemType<GloomStone>();
			AddMapEntry(new Color(70, 29, 104));
			MineResist = 4f;
			MinPick = 209;
			HitSound = SoundID.Tink;
			//SoundType = 21;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}


	}
}