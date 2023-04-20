using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace Emperia.Tiles.Volcano
{
	public class MoltenOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlendAll[this.Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			//ItemDrop = ModContent.ItemType<MagmousOre>();  
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Magmous Ore");
			AddMapEntry(new Color(240, 20, 20), name);
			HitSound = SoundID.Tink;
			MinPick = 75;
			DustType = 6;

		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			{
				r = 0.4f;
				g = 0.17f;
				b = 0.17f;
			}
		}
	}
}