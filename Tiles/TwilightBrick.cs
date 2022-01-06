using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class TwilightBrick : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Grotto.GrottoBrick>();
			AddMapEntry(new Color(77, 98, 148));
			MineResist = 1f;
			SoundType = 21;
			DustType = 121;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}
	}
}