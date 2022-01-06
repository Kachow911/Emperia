using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class Aetherium : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileOreFinderPriority[Type] = 150;
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Aetherium>();
			AddMapEntry(new Color(117, 241, 255));
			MineResist = 5f;
			MinPick = 60;
			SoundType = 21;
			Main.tileSpelunker[Type] = true;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return true;
		}


	}
}