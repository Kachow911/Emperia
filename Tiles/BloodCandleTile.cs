using Terraria;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.ID;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Microsoft.Xna.Framework;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Tiles
{
	public class BloodCandleTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileFrameImportant[Type] = true;
			Main.tileNoAttach[Type] = true;
			Main.tileLavaDeath[Type] = true;
			Main.tileLighted[Type] = true;
			TileID.Sets.DisableSmartCursor[Type] = true;
			TileObjectData.newTile.CopyFrom(TileObjectData.StyleOnTable1x1);
			TileObjectData.newTile.AnchorBottom = new AnchorData(AnchorType.Table | AnchorType.SolidTile, TileObjectData.newTile.Width, 0); //this doesn't seem to work

			TileObjectData.newTile.CoordinateHeights = new int[]
			{
				24
			};
			TileObjectData.newTile.CoordinateWidth = 16;
			TileObjectData.newTile.DrawYOffset = -8;
			TileObjectData.newTile.StyleHorizontal = true;
			TileObjectData.addTile(Type);

			DustType = 60;
			//TODO: might need registeritemdrop for the lit variant or else it won't drop anything!
			LocalizedText name = CreateMapEntryName();
			// name.SetDefault("Blood Candle");
			AddMapEntry(new Color(255, 80, 80), name);


			if (!Main.dedServ)
			{
				flameTexture = ModContent.Request<Texture2D>("Emperia/Tiles/BloodCandleTile_Flame"); // We could also reuse Main.FlameTexture[] textures, but using our own texture is nice.
			}
		}
		private Asset<Texture2D> flameTexture;
		public override void ModifyLight(int i, int j, ref float r, ref float g, ref float b)
		{
			if (Main.tile[i, j].TileFrameX == 18) return;
			r = 0.9f;
			g = 0.1f;
			b = 0.2f;
		}
        public override bool RightClick(int i, int j)
        {
			Tile tile = Main.tile[i, j];
			if (tile.TileFrameX == 18)
			{
				Player player = Main.player[Player.FindClosest(new Vector2(i * 16, j * 16), 16, 16)];
				if (player.GetModPlayer<MyPlayer>().sacrificingBloodCandle == null && player.statLife >= 100)
				{
					player.GetModPlayer<MyPlayer>().sacrificingBloodCandle = new Vector2(i, j); //player now handles the code for the effect
					PlaySound(SoundID.Zombie89 with { Volume = 0.6f }, new Vector2(i * 16, j * 16));
				}
				else return false;
			}
			else tile.TileFrameX = 18;
			return true;
        }
        public override void MouseOver(int i, int j)
        {
			Main.LocalPlayer.cursorItemIconID = ModContent.ItemType<Items.BloodCandle>();
			Main.LocalPlayer.cursorItemIconEnabled = true;
		}
        public static void FlameSwitchFX(Vector2 position)
        {
			for (int d = 0; d < 16; d++) //red light for dust? red sparks?
			{
				int dust = Dust.NewDust(position, 8, 8, DustID.VampireHeal, 0f, Main.rand.Next(1, 3) * -1, 0, default(Color), Main.rand.NextFloat(0.9f, 1.5f));
				Main.dust[dust].noGravity = true;
				Main.dust[dust].velocity.X /= 3;
				Main.dust[dust].fadeIn = 1.1f;
			}
			PlaySound(SoundID.DD2_BetsyFlameBreath, position);
			//Terraria.Audio.SoundEngine.PlaySound(SoundID.DD2_BetsysWrathShot, bloodCandlePos);
		}
		public override void PlaceInWorld(int i, int j, Item item)
        {
			Main.tile[i, j].TileFrameX = 18;
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
		{
			Tile tile = Main.tile[i, j];
			short frameX = tile.TileFrameX;
			short frameY = tile.TileFrameY;
			if (frameX != 0) return;

			if (Main.rand.Next(5) == 0)
			{
				Vector2 position = new Vector2(i * 16 + 4, j * 16);
				if (Main.rand.Next(3) == 0) Dust.NewDust(position, 0, 0, DustID.RedTorch, 0f, -1f, 0, new Color(255, 0, 0), 0.8f);
				else
				{
					int dust = Dust.NewDust(position, 0, 0, DustID.RedTorch, 0f, 0f, 0, default(Color));
					Main.dust[dust].velocity.X /= 3;
					Main.dust[dust].velocity.Y = Main.rand.NextFloat(1.4f, 1.85f) * -1;
				}
			}


			Vector2 zero = new Vector2(Main.offScreenRange, Main.offScreenRange);

			if (Main.drawToScreen)
			{
				zero = Vector2.Zero;
			}

			int width = 16;
			int offsetY = 0;
			int height = 16;

			TileLoader.SetDrawPositions(i, j, ref width, ref offsetY, ref height, ref frameX, ref frameY);

			ulong randSeed = Main.TileFrameSeed ^ (ulong)((long)j << 32 | (long)(uint)i); // Don't remove any casts.

			Color color = new Color(200, 200, 200, 80);

			// We can support different flames for different styles here: int style = Main.tile[j, i].frameY / 54;
			for (int c = 0; c < 7; c++)
			{
				float shakeX = Utils.RandomInt(ref randSeed, -10, 11) * 0.15f;
				float shakeY = Utils.RandomInt(ref randSeed, -10, 1) * 0.35f;

				spriteBatch.Draw(flameTexture.Value, new Vector2(i * 16 - (int)Main.screenPosition.X - (width - 16f) / 2f + shakeX, j * 16 - (int)Main.screenPosition.Y + offsetY + shakeY) + zero, new Rectangle(frameX, frameY, width, height), color, 0f, default, 1f, SpriteEffects.None, 0f);
			}
		}
	}
}
