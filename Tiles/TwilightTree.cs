using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Tiles
{
	public class TwilightTree : ModTree
	{
		private Mod Mod => ModLoader.GetMod("Emperia");


		public override int DropWood()
		{
			return ModContent.ItemType<Items.Grotto.GrottoWood>();
		}
		public override int GrowthFXGore()
		{
			return ModContent.Find<ModGore>("Gores/ExampleTreeFX").Type; //Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/OctopusHead"), 1f);
		}
		//public override void RandomUpdate(int i, int j)
        //{
			//Gore.NewGore(new Vector2(i, j), 0, ModContent.Find<ModGore>("Gores/OctopusHead"), 1f);
		//}
		public override Texture2D GetTexture()
		{
			return Mod.Assets.Request<Texture2D>("Tiles/TwilightTree").Value;
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
		{
			frameWidth=160;
			frameHeight=113;
			frame = 0;
			xOffsetLeft = 52;
			yOffset = 12;
			return Mod.Assets.Request<Texture2D>("Tiles/TwilightTree_Top").Value;
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return Mod.Assets.Request<Texture2D>("Tiles/TwilightTree_Branches").Value;
		}
		
	}
}