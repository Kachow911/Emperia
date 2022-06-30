using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Tiles
{
	public class TwilightTree : ModTree
	{
		private Mod Mod => ModLoader.GetMod("Emperia");

		public override void SetStaticDefaults()
		{
			GrowsOnTileId = new int[1] { TileType<TwilightGrass>() };
		}

		public override TreePaintingSettings TreeShaderSettings => new TreePaintingSettings
        {
			UseSpecialGroups = true,
			SpecialGroupMinimalHueValue = 11f / 72f,
			SpecialGroupMaximumHueValue = 0.25f,
			SpecialGroupMinimumSaturationValue = 0.88f,
			SpecialGroupMaximumSaturationValue = 1f
		};

        public override int DropWood()
		{
			return ModContent.ItemType<Items.Grotto.GrottoWood>();
		}
		public override int TreeLeaf()
		{
			return ModContent.Find<ModGore>("Gores/ExampleTreeFX").Type; //Gore.NewGore(NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/OctopusHead"), 1f);
		}
        //public override void RandomUpdate(int i, int j)
        //{
        //Gore.NewGore(new Vector2(i, j), 0, ModContent.Find<ModGore>("Gores/OctopusHead"), 1f);
        //}

        public override Asset<Texture2D> GetTexture()
        {
			return Request<Texture2D>("Tiles/TwilightTree");
		}

        public override Asset<Texture2D> GetTopTextures()
        {
			/*frameWidth=160;
			frameHeight=113;
			frame = 0;
			xOffsetLeft = 52;
			yOffset = 12;*/
			return Request<Texture2D>("Tiles/TwilightTree_Top");
		}

        public override Asset<Texture2D> GetBranchTextures()
        {
			return Request<Texture2D>("Tiles/TwilightTree_Branches");
		}

		public override void SetTreeFoliageSettings(Tile tile, ref int xoffset, ref int treeFrame, ref int floorY, ref int topTextureFrameWidth, ref int topTextureFrameHeight)
        {
			//
        }
    }
}