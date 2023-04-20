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

					Rectangle subTileQuadrant = GetSubTileTextureFromBorderType(AdjustedBorderTypeForSlope(i, j, (SubTile)subTile, GetBorderTypeFromTileFrame(i, j, subTile)));
					if (subTileQuadrant.IsEmpty) continue;

					Vector2 position = new Vector2(i * 16 + l * 8, j * 16 + k * 8);
					position -= Main.screenPosition;
					position += zero;
					spriteBatch.Draw(texture, position, subTileQuadrant, lcdColors[subTile]);//HueToRGB((int)Emperia.MouseControlledFloatX(false, 360)));
				}
			}
		}

		public enum SubTile
        {
			UpperLeft = 0,
			UpperRight = 1,
			LowerLeft = 2,
			LowerRight = 3,
        }

		public enum BorderType //see LCDScreenTileSubTiles.png
		{
			Null = -1, 
			Left = 0,
			Top = 1,
			Right = 2,
			Bottom = 3,
			TopLeft = 4,
			TopRight = 5,
			BottomLeft = 6,
			BottomRight = 7,
			None = 8,
			TopAndBottom = 9,
			BottomLeftAndTop = 10,
			BottomRightAndTop = 11,
			TriangularTopLeft = 12,
			TriangularTopRight = 13,
			TriangularBottomLeft = 14,
			TriangularBottomRight = 15,
			TriangularTopLeftAndRight = 16,
			TriangularTopRightAndLeft = 17,
			TriangularBottomLeftAndRight = 18,
			TriangularBottomRightAndLeft = 19,
			TriangularTopLeftAndBottom = 20,
			TriangularTopRightAndBottom = 21,
			TriangularBottomLeftAndTop = 22,
			TriangularBottomRightAndTop = 23,
		}

		public static Rectangle GetSubTileTextureFromBorderType(BorderType type)
		{
			if (type == BorderType.Null) return Rectangle.Empty;

			int frameX = (int)type % 4;
			int frameY = ((int)type - (int)type % 4) / 4;

			return new Rectangle(frameX * 10, frameY * 10, 8, 8);
		}
		public static BorderType AdjustedBorderTypeForSlope(int i, int j, SubTile subTile, BorderType type)
        {
			Tile tile = Main.tile[i, j];

			if (!tile.IsHalfBlock) //changes upper half subtile borders to cover exposed block sides if neighboring blocks are halfblocks
			{ 
				if (type == BorderType.Top)
				{
					if ((subTile == SubTile.UpperLeft) && Main.tile[i - 1, j].IsHalfBlock)
					{
						if (tile.Slope == SlopeType.SlopeDownLeft) return BorderType.TriangularTopRightAndLeft;
						else return BorderType.TopLeft;
					}
					if ((subTile == SubTile.UpperRight) && Main.tile[i + 1, j].IsHalfBlock)
					{
						if (tile.Slope == SlopeType.SlopeDownRight) return BorderType.TriangularTopLeftAndRight;
						else return BorderType.TopRight;
					}
				}
				else if (type == BorderType.None)
				{
					if ((subTile == SubTile.UpperLeft) && Main.tile[i - 1, j].IsHalfBlock) return BorderType.Left;
					if ((subTile == SubTile.UpperRight) && Main.tile[i + 1, j].IsHalfBlock) return BorderType.Right;
				}
			}

			if (tile.IsHalfBlock)
			{
				switch (subTile)
				{
					case SubTile.UpperLeft:
					case SubTile.UpperRight:
						return BorderType.Null;
					case SubTile.LowerLeft:
					case SubTile.LowerRight:
						switch (type)
						{
							case BorderType.Left: return BorderType.TopLeft; //basically, imagine this just finding the equivalent subtile with a border line drawn at the top
							case BorderType.Right: return BorderType.TopRight;
							case BorderType.Bottom: return BorderType.TopAndBottom;
							case BorderType.BottomLeft: return BorderType.BottomLeftAndTop;
							case BorderType.BottomRight: return BorderType.BottomRightAndTop;
							case BorderType.None: return BorderType.Top;
						}
					break;
				}
			}

			switch (tile.Slope)
			{
				//For the purposes of this code, subTiles refer to fourths of a tile. Splitting a normal tile into 4 corners would just make 4 smaller squares.
				//But for slopes, one subTile is empty air, so we will not draw it (return BorderType.Null;), one is an unchanged square, so we will draw it no differently, (break;) and the remaining two are slanted or triangular.
				//The triangular subtiles must account for borderless variations, to blend with potential adjacent blocks, except in opposite directions, where a slope cannot connect.
				//So for example, SlopeDownRight cannot connect to the block up above or to the left, and its triangular subtiles are TopRight and BottomLeft.
				//The TopRight subtile /| must account for connecting to a tile to the right /|[], so we get t he equivalent subTile with no right border (Top). The BottomLeft subtile can connect underneath, which requires the equivalent variant with no bottom border (Left).
				//WIP, im pretty sure this is not what i meant by the original comment below, mixing up subtile and bordertype

				//Slopes in Terraria can be split into 4 corners, called subTiles for purposes here. Splitting a normal block into 4 corners would just make 4 smaller squares.
				//But for slopes, one subTile is empty air, so we will not draw it (return BorderType.Null;), one is an unchanged square, so we will draw it no differently, (break;) and the remaining two are slanted or triangular.
				//The triangular subtiles must account for borderless variations, to blend with potential adjacent blocks, except in opposite directions, where a slope cannot connect.
				//So for example, SlopeDownRight cannot connect to the block up above or to the left, and its triangular subtiles are upper right (5) and bottom left (6).
				//The upper right subtile /| can connect to the right /|[], so we get a variant with no right border (1). The bottom left subtile can connect underneath, meaning a variant with no bottom border (0).
				case SlopeType.SlopeDownRight:
					switch (subTile)
					{
						case SubTile.UpperLeft: return BorderType.Null;
						case SubTile.LowerRight: break;
						case SubTile.UpperRight:
						case SubTile.LowerLeft:
							switch (type)
							{
								case BorderType.Left: return BorderType.TriangularTopLeft;
								case BorderType.Top: return BorderType.TriangularTopLeft;
								case BorderType.TopRight: return BorderType.TriangularTopLeftAndRight; 
								case BorderType.BottomLeft: return BorderType.TriangularTopLeftAndBottom;
							}
						break;
					}
					break;
				case SlopeType.SlopeDownLeft:
					switch (subTile)
					{
						case SubTile.UpperRight: return BorderType.Null;
						case SubTile.LowerLeft: break;
						case SubTile.UpperLeft:
						case SubTile.LowerRight:
							switch (type)
							{
								case BorderType.Top: return BorderType.TriangularTopRight;
								case BorderType.Right: return BorderType.TriangularTopRight;
								case BorderType.TopLeft: return BorderType.TriangularTopRightAndLeft;
								case BorderType.BottomRight: return BorderType.TriangularTopRightAndBottom;
							}
						break;
					}
					break;
				case SlopeType.SlopeUpRight:
					switch (subTile)
					{
						case SubTile.LowerLeft: return BorderType.Null;
						case SubTile.UpperRight: break;
						case SubTile.UpperLeft:
						case SubTile.LowerRight:
							switch (type)
							{
								case BorderType.Left: return BorderType.TriangularBottomLeft;
								case BorderType.Bottom: return BorderType.TriangularBottomLeft;
								case BorderType.TopLeft: return BorderType.TriangularBottomLeftAndTop;
								case BorderType.BottomRight: return BorderType.TriangularBottomLeftAndRight;
							}
						break;
					}
					break;
				case SlopeType.SlopeUpLeft:
					switch (subTile)
					{
						case SubTile.UpperLeft: break;
						case SubTile.LowerRight: return BorderType.Null;
						case SubTile.UpperRight:
						case SubTile.LowerLeft:
							switch (type)
							{
								case BorderType.Right: return BorderType.TriangularBottomRight;
								case BorderType.Bottom: return BorderType.TriangularBottomRight;
								case BorderType.TopRight: return BorderType.TriangularBottomRightAndTop;
								case BorderType.BottomLeft: return BorderType.TriangularBottomRightAndLeft;
							}
							break;
					}
					break;
			}
			return type;
        }
		public static BorderType GetBorderTypeFromTileFrame(int i, int j, int subTile)
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
						case 0: return BorderType.Left;
						case 1: return BorderType.None;
						case 2: return BorderType.Left;
						case 3: return BorderType.None;
                    }
					break;

				case (18, 0):
				case (36, 0):
				case (54, 0):
					switch (subTile)
					{
						case 0: return BorderType.Top;
						case 1: return BorderType.Top;
						case 2: return BorderType.None;
						case 3: return BorderType.None;
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
					return BorderType.None;


				case (18, 36):
				case (36, 36):
				case (54, 36):
					switch (subTile)
					{
						case 0: return BorderType.None;
						case 1: return BorderType.None;
						case 2: return BorderType.Bottom;
						case 3: return BorderType.Bottom;
					}
					break;

				case (72, 0):
				case (72, 18):
				case (72, 36):
					switch (subTile)
					{
						case 0: return BorderType.None;
						case 1: return BorderType.Right;
						case 2: return BorderType.None;
						case 3: return BorderType.Right;
					}
					break;

				case (90, 0):
				case (90, 18):
				case (90, 36):
					switch (subTile)
					{
						case 0: return BorderType.Left;
						case 1: return BorderType.Right;
						case 2: return BorderType.Left;
						case 3: return BorderType.Right;
					}
					break;

				case (108, 0):
				case (126, 0):
				case (144, 0):
					switch (subTile)
					{
						case 0: return BorderType.TopLeft;
						case 1: return BorderType.TopRight;
						case 2: return BorderType.Left;
						case 3: return BorderType.Right;
					}
					break;

				case (108, 54):
				case (126, 54):
				case (144, 54):
					switch (subTile)
					{
						case 0: return BorderType.Left;
						case 1: return BorderType.Right;
						case 2: return BorderType.BottomLeft;
						case 3: return BorderType.BottomRight;
					}
					break; 

				case (108, 72):
				case (126, 72):
				case (144, 72):
					switch (subTile)
					{
						case 0: return BorderType.Top;
						case 1: return BorderType.Top;
						case 2: return BorderType.Bottom;
						case 3: return BorderType.Bottom;
					}
					break;

				case (162, 0):
				case (162, 18):
				case (162, 36):
					switch (subTile)
					{
						case 0: return BorderType.TopLeft;
						case 1: return BorderType.Top;
						case 2: return BorderType.BottomLeft;
						case 3: return BorderType.Bottom;
					}
					break;

				case (216, 0):
				case (216, 18):
				case (216, 36):
					switch (subTile)
					{
						case 0: return BorderType.Top;
						case 1: return BorderType.TopRight;
						case 2: return BorderType.Bottom;
						case 3: return BorderType.BottomRight;
					}
					break;

				case (162, 54):
				case (180, 54):
				case (198, 54):
					switch (subTile)
					{
						case 0: return BorderType.TopLeft;
						case 1: return BorderType.TopRight;
						case 2: return BorderType.BottomLeft;
						case 3: return BorderType.BottomRight;
					}
					break;

				case (0, 54):
				case (36, 54):
				case (72, 54):
					switch (subTile)
					{
						case 0: return BorderType.TopLeft;
						case 1: return BorderType.Top;
						case 2: return BorderType.Left;
						case 3: return BorderType.None;
					}
					break;

				case (18, 54):
				case (54, 54):
				case (90, 54):
					switch (subTile)
					{
						case 0: return BorderType.Top;
						case 1: return BorderType.TopRight;
						case 2: return BorderType.None;
						case 3: return BorderType.Right;
					}
					break;

				case (0, 72):
				case (36, 72):
				case (72, 72):
					switch (subTile)
					{
						case 0: return BorderType.Left;
						case 1: return BorderType.None;
						case 2: return BorderType.BottomLeft;
						case 3: return BorderType.Bottom;
					}
					break;

				case (18, 72):
				case (54, 72):
				case (90, 72):
					switch (subTile)
					{
						case 0: return BorderType.None;
						case 1: return BorderType.Right;
						case 2: return BorderType.Bottom;
						case 3: return BorderType.BottomRight;
					}
					break;
			}

			Main.NewText("Error: frame" + frame.ToString() + "not accounted for", Color.Red); //not an issue that's ever happened
			return BorderType.None;
		}
	}
}