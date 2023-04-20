using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;

namespace Emperia.Tiles
{
	public class MoonPedestal : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileLighted[Type] = true;
			ItemDrop = ModContent.ItemType<Items.MoonPedestalItem>();

			TileObjectData.newTile.CopyFrom(TileObjectData.Style1x2);
			TileObjectData.newTile.Origin = new Point16(0, 1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table | AnchorType.SolidTile, TileObjectData.newTile.Width, 0);
			TileObjectData.addTile(Type);
		}

		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			r = .55f;
			g = .30f;
			b = .94f;
		}


		public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			EmperialWorld.respawnFull = true;
			if (Main.rand.NextBool(8))
			{
				Vector2 placePosition = new Vector2(i * 16, j * 16 - 16) + new Vector2(0, 10).RotatedByRandom(MathHelper.ToRadians(360));
				int dust = Dust.NewDust(placePosition, 4, 4, DustID.Venom);
				Main.dust[dust].noGravity = true;
			}


		}
		
	}
}
