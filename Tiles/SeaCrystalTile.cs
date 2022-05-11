using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Emperia.Npcs.SeaCrab;
using Terraria.ID;
using static Terraria.ModLoader.ModContent;
namespace Emperia.Tiles
{
	public class SeaCrystalTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			MineResist = 0.1f;
			TileObjectData.addTile(Type);
			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x1);
			Main.tileSolid[Type] = false;
			Main.tileMergeDirt[Type] = true;
			Main.tileLighted[Type] = true;
			Main.tileFrameImportant[Type] = true;
			DustType = 75;
			ModTranslation name = CreateMapEntryName();
			name.SetDefault("Sea Crystal");
			AddMapEntry(new Color(100, 185, 50), name);
			TileObjectData.addTile(Type);
		}

        public override bool IsTileDangerous(int i, int j, Player player)
        {
            return true;
        }
        public override void NumDust(int i, int j, bool fail, ref int num)
		{
			num = fail ? 1 : 5;
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
				r = 0.4f;
				g = 0.8f;
				b = 0.2f;
		}

		public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
			NPC.NewNPC(NPC.GetSource_NaturalSpawn(), i * 16 + 14, j * 16 + 50, NPCType<SeaCrab>());
		}
		
	}
}