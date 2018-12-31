using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Tiles.Volcano
{
	public class MoltenOre : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileSpelunker[Type] = true;
			Main.tileSolid[Type] = true;
			Main.tileBlendAll[this.Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			Main.tileLighted[Type] = true;
			drop = mod.ItemType("MagmousOre");  
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Magmous Ore");
			AddMapEntry(new Color(240, 20, 20), name);
			soundType = 21;
			minPick = 75;
			dustType = 6;

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