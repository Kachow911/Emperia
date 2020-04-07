using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class TwilightFlora : ModTile
	{
		public override void SetDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileCut[Type] = true;
			Main.tileSolid[Type] = false;
			Main.tileNoAttach[Type] = true;
			Main.tileNoFail[Type] = true;
			Main.tileLavaDeath[Type] = true;
		}

		public override bool PreDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Vector2 offset = new Vector2(Main.offScreenRange, Main.offScreenRange + 2);
			if (Main.drawToScreen)
			{
				offset = Vector2.Zero;
			}

			Tile tile = Main.tile[i, j];

			if (tile.frameY == 18)
			{
				offset.Y -= 12;
			}
			spriteBatch.Draw(Main.tileTexture[Type], new Vector2(i, j) * 16 - Main.screenPosition + offset, new Rectangle(tile.frameX, tile.frameY, 16, tile.frameY == 18 ? 28 : 16), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
			return false;
		}
	}
}
