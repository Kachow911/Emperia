using Emperia.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Diagnostics;
using System.Linq;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Tiles
{
	public class LCDScreenTile : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			//Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Placeable.LCDScreen>();
			AddMapEntry(new Color(50, 50, 50));
			MineResist = 0.8f;
			//SoundType = 21;
			HitSound = SoundID.Shatter;
			DustType = 54; //6
		}
        public override void KillTile(int i, int j, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
            if (!fail && !effectOnly && LCDSystem.activeLCDs.ContainsKey((i, j)))
            {
				LCDSystem.activeLCDs.Remove((i, j));
            }
		}
        public override void PostDraw(int i, int j, SpriteBatch spriteBatch)
        {
			if (!LCDSystem.activeLCDs.ContainsKey((i, j))) return;
			LCDSystem.activeLCDs.TryGetValue((i, j), out var lcdColors);

			Vector2 zero = Main.drawToScreen ? Vector2.Zero : new Vector2(Main.offScreenRange);
			Texture2D texture = ModContent.Request<Texture2D>("Emperia/Tiles/LCDScreenTileSubTiles", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
			for (int k = 0; k < 2; k++)
			{
				for (int l = 0; l < 2; l++)
				{
					int subTile = l + k * 2; //gives each of the 4 quadrants per tile a unique int value
					if (lcdColors[subTile] == Color.Black) continue;

					Rectangle subTileQuadrant = GetSubTileTextureFromType(FactorInSlopeForSubTileType(i, j, subTile, GetSubTileTypeFromTileFrame(i, j, subTile)));
					if (subTileQuadrant.IsEmpty) continue;

					Vector2 position = new Vector2(i * 16 + l * 8, j * 16 + k * 8);
					position -= Main.screenPosition;
					position += zero;
					spriteBatch.Draw(texture, position, subTileQuadrant, lcdColors[subTile]);//HueToRGB((int)Emperia.MouseControlledFloatX(false, 360)));
				}
			}
		}
		public Color HueToRGB(int hue)
        {
			int[] rgb = new int[3];

			if (hue < 60)
			{
			    rgb[0] = 255;
			    rgb[1] = hue * 255 / 60;
			    rgb[2] = 0;
			}
			else if (hue < 120)
			{
			    rgb[0] = (120 - hue) * 255 / 60;
			    rgb[1] = 255;
			    rgb[2] = 0;
			}
			else if (hue < 180)
			{
				rgb[0] = 0;
				rgb[1] = 255;
				rgb[2] = (hue - 120) * 255 / 60;
			}
			else if (hue < 240)
			{
				rgb[0] = 0;
				rgb[1] = (240 - hue) * 255 / 60;
				rgb[2] = 255;
			}
			else if (hue < 300)
			{
				rgb[0] = (hue - 240) * 255 / 60;
				rgb[1] = 0;
				rgb[2] = 255;
			}
			else
			{
				rgb[0] = 255;
				rgb[1] = 0;
				rgb[2] = (360 - hue) * 255 / 60;
			}
			return new Color(rgb[0], rgb[1], rgb[2]);
        }
		public static Rectangle GetSubTileTextureFromType(int type)
		{
			if (type == -1) return Rectangle.Empty;
			int posX;
			int posY = 0;
			while (type >= 4)
            {
				type -= 4;
				posY += 10;
            }
			posX = type * 10;

			return new Rectangle(posX, posY, 8, 8);
		}
		public static int FactorInSlopeForSubTileType(int i, int j, int subTile, int type)
        {
			Tile tile = Main.tile[i, j];

			if (!tile.IsHalfBlock) //changes upper half subtiles to blend with neighboring halfblocks
			{ 
				if (type == 1)
				{
					if ((subTile == 0) && Main.tile[i - 1, j].IsHalfBlock)
					{
						if (tile.Slope == SlopeType.SlopeDownLeft) return 17;
						else return 4;
					}
					if ((subTile == 1) && Main.tile[i + 1, j].IsHalfBlock)
					{
						if (tile.Slope == SlopeType.SlopeDownRight) return 16;
						else return 5;
					}
				}
				else if (type == 8)
				{
					if ((subTile == 0) && Main.tile[i - 1, j].IsHalfBlock) return 0;
					if ((subTile == 1) && Main.tile[i + 1, j].IsHalfBlock) return 2;
				}
			}

			if (tile.IsHalfBlock)
			{
				switch (subTile)
				{
					case 0:
					case 1:
						return -1;
					case 2:
					case 3:
						switch (type)
						{
							case 0: return 4; //basically, imagine this just finding the equivalent subtile with a border line drawn at the top
							case 2: return 5;
							case 3: return 9;
							case 6: return 10;
							case 7: return 11;
							case 8: return 1;
						}
					break;
				}
			}

			switch (tile.Slope)
			{
				//Slopes in Terraria can be split into 4 corners, called subTiles for purposes here. Splitting a normal block into 4 corners would just make 4 smaller squares.
				//But for slopes, one subTile is empty air, so we will not draw it (return -1;), one is an unchanged square, so we will draw it no differently, (break;) and the remaining two are slanted or triangular.
				//The triangular subtiles must account for borderless variations, to blend with potential adjacent blocks, except in opposite directions, where a slope cannot connect.
				//So for example, SlopeDownRight cannot connect to the block up above or to the left, and its triangular subtiles are upper right (5) and bottom left (6).
				//The upper right subtile /| can connect to the right /|[], meaning a variant with no right border (1). The bottom left subtile can connect underneath, meaning a variant with no bottom border (0).
				case SlopeType.SlopeDownRight:
					switch (subTile)
					{
						case 0: return -1;
						case 3: break;
						case 1:
						case 2:
							switch (type)
							{
								case 0: return 12;
								case 1: return 12;
								case 5: return 16; 
								case 6: return 20;
							}
						break;
					}
					break;
				case SlopeType.SlopeDownLeft:
					switch (subTile)
					{
						case 1: return -1;
						case 2: break;
						case 0:
						case 3:
							switch (type)
							{
								case 1: return 13;
								case 2: return 13;
								case 4: return 17;
								case 7: return 21;
							}
						break;
					}
					break;
				case SlopeType.SlopeUpRight:
					switch (subTile)
					{
						case 2: return -1;
						case 1: break;
						case 0:
						case 3:
							switch (type)
							{
								case 0: return 14;
								case 3: return 14;
								case 4: return 22;
								case 7: return 18;
							}
						break;
					}
					break;
				case SlopeType.SlopeUpLeft:
					switch (subTile)
					{
						case 0: break;
						case 3: return -1;
						case 1:
						case 2:
							switch (type)
							{
								case 2: return 15;
								case 3: return 15;
								case 5: return 23;
								case 6: return 19;
							}
							break;
					}
					break;
			}
			return type;
        }
		public static int GetSubTileTypeFromTileFrame(int i, int j, int subTile)
        {
			Tile tile = Main.tile[i, j];
			(int, int) frame = (tile.TileFrameX, tile.TileFrameY);

			switch (frame)
            {
				case (0, 0):
				case (0, 18):
				case (0, 36):
					switch (subTile)
                    {
						case 0: return 0;
						case 1: return 8;
						case 2: return 0;
						case 3: return 8;
                    }
					break;

				case (18, 0):
				case (36, 0):
				case (54, 0):
					switch (subTile)
					{
						case 0: return 1;
						case 1: return 1;
						case 2: return 8;
						case 3: return 8;
					}
					break;

				case (18, 18):
				case (36, 18):
				case (54, 18):

				case (108, 18):
				case (126, 18):
				case (144, 18):
				case (108, 36):
				case (126, 36):
				case (144, 36):

				case (180, 0):
				case (180, 18):
				case (180, 36):

				case (198, 0):
				case (198, 18):
				case (198, 36):
					return 8; //subtile with no borders


				case (18, 36):
				case (36, 36):
				case (54, 36):
					switch (subTile)
					{
						case 0: return 8;
						case 1: return 8;
						case 2: return 3;
						case 3: return 3;
					}
					break;

				case (72, 0):
				case (72, 18):
				case (72, 36):
					switch (subTile)
					{
						case 0: return 8;
						case 1: return 2;
						case 2: return 8;
						case 3: return 2;
					}
					break;

				case (90, 0):
				case (90, 18):
				case (90, 36):
					switch (subTile)
					{
						case 0: return 0;
						case 1: return 2;
						case 2: return 0;
						case 3: return 2;
					}
					break;

				case (108, 0):
				case (126, 0):
				case (144, 0):
					switch (subTile)
					{
						case 0: return 4;
						case 1: return 5;
						case 2: return 0;
						case 3: return 2;
					}
					break;

				case (108, 54):
				case (126, 54):
				case (144, 54):
					switch (subTile)
					{
						case 0: return 0;
						case 1: return 2;
						case 2: return 6;
						case 3: return 7;
					}
					break; 

				case (108, 72):
				case (126, 72):
				case (144, 72):
					switch (subTile)
					{
						case 0: return 1;
						case 1: return 1;
						case 2: return 3;
						case 3: return 3;
					}
					break;

				case (162, 0):
				case (162, 18):
				case (162, 36):
					switch (subTile)
					{
						case 0: return 4;
						case 1: return 1;
						case 2: return 6;
						case 3: return 3;
					}
					break;

				case (216, 0):
				case (216, 18):
				case (216, 36):
					switch (subTile)
					{
						case 0: return 1;
						case 1: return 5;
						case 2: return 3;
						case 3: return 7;
					}
					break;

				case (162, 54):
				case (180, 54):
				case (198, 54):
					switch (subTile)
					{
						case 0: return 4; //return 0;
						case 1: return 5; //return 2;
						case 2: return 6;
						case 3: return 7;
					}
					break;

				case (0, 54):
				case (36, 54):
				case (72, 54):
					switch (subTile)
					{
						case 0: return 4;
						case 1: return 1;
						case 2: return 0;
						case 3: return 8;
					}
					break;

				case (18, 54):
				case (54, 54):
				case (90, 54):
					switch (subTile)
					{
						case 0: return 1;
						case 1: return 5;
						case 2: return 8;
						case 3: return 2;
					}
					break;

				case (0, 72):
				case (36, 72):
				case (72, 72):
					switch (subTile)
					{
						case 0: return 0;
						case 1: return 8;
						case 2: return 6;
						case 3: return 3;
					}
					break;

				case (18, 72):
				case (54, 72):
				case (90, 72):
					switch (subTile)
					{
						case 0: return 8;
						case 1: return 2;
						case 2: return 3;
						case 3: return 7;
					}
					break;
			}

			Main.NewText("Error: frame" + frame.ToString() + "not accounted for", Color.Red); //not an issue that's ever happened
			return 8;
		}
	}
}