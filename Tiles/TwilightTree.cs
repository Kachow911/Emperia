using Microsoft.Xna.Framework.Graphics;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class TwilightTree : ModTree
	{
		private Mod mod
		{
			get
			{
				return ModLoader.GetMod("Emperia");
			}
		}

		
		public override int DropWood()
		{
			return mod.ItemType("DaysVerge");
		}
		public override int GrowthFXGore()
		{
			return mod.GetGoreSlot("Gores/ExampleTreeFX"); //Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/OctopusHead"), 1f);
		}
		//public override void RandomUpdate(int i, int j)
        //{
			//Gore.NewGore(new Vector2(i, j), 0, mod.GetGoreSlot("Gores/OctopusHead"), 1f);
		//}
		public override Texture2D GetTexture()
		{
			return mod.GetTexture("Tiles/TwilightTree");
		}

		public override Texture2D GetTopTextures(int i, int j, ref int frame, ref int frameWidth, ref int frameHeight, ref int xOffsetLeft, ref int yOffset)
		{
			frameWidth=160;
			frameHeight=113;
			frame = 0;
			xOffsetLeft = 75;
			return mod.GetTexture("Tiles/TwilightTree_Top");
		}

		public override Texture2D GetBranchTextures(int i, int j, int trunkOffset, ref int frame)
		{
			return mod.GetTexture("Tiles/TwilightTree_Branches");
		}
	}
}