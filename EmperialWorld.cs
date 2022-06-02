using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Terraria.GameContent.Generation;
using static Terraria.ModLoader.ModContent;
using Emperia.Tiles;
using Emperia.Walls;
using System;
using Terraria.GameContent.ItemDropRules;

namespace Emperia
{
    public class EmperialWorld : ModSystem
    {
	
		public static int VolcanoTiles = 0;
		public static int GrottoTiles = 0;
		private static int twilightX;
		private static int twilightY;
		private static int TwilightSize
		{
			get
			{
				if (Main.maxTilesX == 6400)
					return 1300;
				else if (Main.maxTilesX == 8400)
					return 1900;

				return 900;
			}
		}

		private static int TwilightWidth => TwilightSize / 2;
		private static int TwilightHeight => TwilightSize / 3;

		public static bool downedEye = false;
		public static bool respawnFull = false;
		public override void NetSend(BinaryWriter writer)
		{
			BitsByte flags = new BitsByte();
			flags[0] = downedEye;
			writer.Write(flags);
		}

		public override void NetReceive(BinaryReader reader)
		{
			BitsByte flags = reader.ReadByte();
			downedEye = flags[0];
		}
		public override void ResetNearbyTileEffects()
		{
			VolcanoTiles = 0;
			GrottoTiles = 0;
		}
        public override void TileCountsAvailable(ReadOnlySpan<int> tileCounts)
        {
			VolcanoTiles = tileCounts[TileType<Tiles.Volcano.VolcanoTile>()];
			GrottoTiles = tileCounts[TileType<TwilightDirt>()] + tileCounts[TileType<TwilightBrick>()] + tileCounts[TileType<TwilightGrass>()] + tileCounts[TileType<TwilightStone>()] + tileCounts[TileType<TFWood>()] + tileCounts[TileType<TFLeaf>()];
		}
		public void MakeCircle(int X, int Y, int radius, int TileType)
		{
			
                    for (int x = X - radius; x <= X + radius; x++)
					{
                        for (int y = Y - radius; y <= Y + radius; y++)
						{
                            if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius)
							{
								WorldGen.KillTile(x, y);
								WorldGen.PlaceTile(x, y, TileType);
							}
						}
								
							
					}
		}
		public void KillCircle(int X, int Y, int radius)
		{

			for (int x = X - radius; x <= X + radius; x++)
			{
				for (int y = Y - radius; y <= Y + radius; y++)
				{
					if (Vector2.Distance(new Vector2(X, Y), new Vector2(x, y)) <= radius)
					{

						WorldGen.KillTile(x, y);
					}
				}


			}
		}
		/*public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int index = tasks.FindIndex(genPass => genPass.Name.Equals("Corruption"));

			if (index != -1)
			{
				tasks.Insert(index + 1, new PassLegacy("Twilight Terrain", GenerateTwilightTerrain));
				tasks.Insert(index + 2, new PassLegacy("Twilight Caves", GenerateTwilightCaves));
			}

			index = tasks.FindIndex(genPass => genPass.Name.Equals("Spreading Grass"));

			if (index != -1)
				tasks.Insert(index + 1, new PassLegacy("Twilight Flora", GenerateTwilightDecorations));
		}*/

		private void GenerateTwilightTerrain(GenerationProgress progress)
		{
			int mapEdgeDistance = Main.maxTilesX == 8400 ? 900 : Main.maxTilesX == 6400 ? 700 : 400;

			int x = WorldGen.genRand.Next(mapEdgeDistance, Main.maxTilesX - mapEdgeDistance);
			int y = 250;

			int badTileCount = 0;
			int airCount = 0;
			int tries = 0;
			do
			{
				x = WorldGen.genRand.Next(mapEdgeDistance, Main.maxTilesX - mapEdgeDistance);

				if (Math.Abs(x + TwilightWidth / 2 + Main.spawnTileX) < 300)
					continue;

				badTileCount = 0;
				tries++;

				for (int i = x; i < x + TwilightWidth; i++)
				{
					for (int j = y; j < y + TwilightHeight; j++)
					{
						switch (Main.tile[i, j].TileType)
						{
							case TileID.Ebonsand:
							case TileID.Ebonstone:
							case TileID.Crimsand:
							case TileID.Crimstone:
							case TileID.SnowBlock:
							case TileID.IceBlock:
							case TileID.JungleGrass:
							case TileID.JungleVines:
							case TileID.Sandstone:
							case TileID.BlueDungeonBrick:
							case TileID.GreenDungeonBrick:
							case TileID.PinkDungeonBrick:
								badTileCount++;
								break;
						}

						if (!Main.tile[i, j].HasTile)
							airCount++;
					}
				}
			} while (badTileCount > 100 && airCount > 200 && tries < 500);

			// find a spot on the ground
			while (!Main.tile[x, y].HasTile)
				y++;

			y -= 100; // move it 100 tiles up to accomodate for elevated terrain
			for (int j = y; j < y + TwilightHeight; j++)
			{
				int offset = (j - y) / 2;

				if (Main.tile[x + offset, j].HasTile)
					WorldGen.TileRunner(x + offset, j, WorldGen.genRand.Next(8, 14), 15, ModContent.TileType<TwilightStone>(), true, overRide: true);

				for (int i = x + offset; i < x + TwilightWidth - offset; i++)
				{
					if (!Main.tile[i, j].HasTile)
						continue;

					switch (Main.tile[i, j].TileType)
					{
						case TileID.Grass:
						case TileID.JungleGrass:
						case TileID.CorruptGrass:
						case TileID.CrimsonGrass:
						case TileID.Dirt:
						case TileID.ClayBlock:
						case TileID.Mud:
						case TileID.Sand:
						case TileID.SnowBlock:
						case TileID.IceBlock:
							Main.tile[i, j].TileType = (ushort)ModContent.TileType<TwilightDirt>();
							break;

						case TileID.Stone:
							Main.tile[i, j].TileType = (ushort)ModContent.TileType<TwilightStone>();
							break;
					}

					if (Main.tile[i, j].WallType == WallID.Dirt)
					{
						WorldGen.KillWall(i, j);
						Main.tile[i, j].WallType = (ushort)ModContent.WallType<TwilightWoodWall>();
					}
				}

				if (Main.tile[x + TwilightWidth - offset, j].HasTile)
					WorldGen.TileRunner(x + TwilightWidth - offset, j, WorldGen.genRand.Next(8, 14), 15, ModContent.TileType<TwilightStone>(), true, overRide: true);
			}

			twilightX = x;
			twilightY = y;
		}

		private void GenerateTwilightCaves(GenerationProgress progress)
		{
			for (int i = 0; i < WorldGen.genRand.Next(30, 40); i++)
				WorldGen.TileRunner(
					WorldGen.genRand.Next(twilightX + 60, twilightX + TwilightWidth - 60),
					WorldGen.genRand.Next(twilightY + 30, twilightY + 30 + TwilightHeight),
					WorldGen.genRand.NextFloat(12f, 20f),
					WorldGen.genRand.Next(35, 50),
					-1,
					true
					);
		}

		private void GenerateTwilightDecorations(GenerationProgress progress)
		{
			for (int i = twilightX; i < twilightX + TwilightWidth; i++)
			{
				for (int j = twilightY; j < twilightY + TwilightHeight; j++)
				{
					// spread grass all over the dirt
					WorldGen.SpreadGrass(i, j, ModContent.TileType<TwilightDirt>(), ModContent.TileType<TwilightGrass>());

					// grow the trees
					if (j < twilightY + 130 && Main.tile[i, j].TileType == ModContent.TileType<TwilightGrass>())
						WorldGen.GrowTree(i, j);

					// place down some twilight pillars and bushes
					if (IsTwilightBlock(i, j) && !Main.tile[i, j - 1].HasTile)
					{
						if (WorldGen.genRand.Next(20) == 0)
						{
							int type = WorldGen.genRand.NextFloat() <= 0.4f ? ModContent.TileType<TwilightPillar>() : ModContent.TileType<TwilightBush>();
							Tile tile1 = Main.tile[i, j];
							//Main.tile[i, j].slope(0);
							tile1.BlockType = 0;

							if (type == ModContent.TileType<TwilightPillar>())
								for (int x = -3; x <= 3; x++)
									if (Main.tile[i + x, j].HasTile)
										Main.tile[i + x, j].TileType = (ushort)ModContent.TileType<TwilightStone>();

							WorldGen.PlaceTile(i, j - 1, type, true, style: WorldGen.genRand.Next(2));
						}
					}

					// replace vanilla pots
					if (Main.tile[i, j].TileType == TileID.Pots)
						WorldGen.PlaceTile(i, j, ModContent.TileType<TwilightPot>(), true);

					// place down the flora
					if (Main.tile[i, j].TileType == ModContent.TileType<TwilightGrass>()
						//&& Main.tile[i, j].slope() == 0
						//&& Main.tile[i, j].BlockType != BlockType.HalfBlock
						&& Main.tile[i, j].BlockType == 0
						&& !Main.tile[i, j - 1].HasTile
						&& !Main.tile[i, j - 2].HasTile)
					{
						WorldGen.PlaceTile(i, j - 1, ModContent.TileType<TwilightFlora>(), true);
						Main.tile[i, j - 1].TileFrameY = (short)(WorldGen.genRand.Next(2) == 0 ? 0 : 18);
						Main.tile[i, j - 1].TileFrameX = (short)(Main.tile[i, j - 1].TileFrameY == 0 ? WorldGen.genRand.Next(9) * 18 : WorldGen.genRand.Next(7) * 18);
					}
				}
			}

			// sometimes grass blocks / dirt blocks dont convert to the twilight ones in GenerateTerrain so we clean it up here
			for (int j = twilightY; j < twilightY + TwilightHeight; j++)
			{
				int offset = (j - twilightY) / 2;
				for (int i = twilightX + offset; i < twilightX + TwilightWidth - offset; i++)
				{
					if (!Main.tile[i, j].HasTile)
						continue;

					switch (Main.tile[i, j].TileType)
					{
						case TileID.Dirt:
							Main.tile[i, j].TileType = (ushort)ModContent.TileType<TwilightDirt>();
							break;

						case TileID.Grass:
							Main.tile[i, j].TileType = (ushort)ModContent.TileType<TwilightGrass>();
							break;

						case TileID.Plants:
							Tile tile2 = Main.tile[i, j];
							tile2.HasTile = false;
							break;
					}
				}
			}
			int roomCount = WorldGen.genRand.Next(2, 4);

			if (Main.maxTilesX == 6400)
				roomCount = WorldGen.genRand.Next(4, 7);
			else if (Main.maxTilesX == 8400)
				roomCount = WorldGen.genRand.Next(7, 9);

			// generate some rooms
			for (int i = 0; i < roomCount; i++)
				GenerateUndergroundRoom(WorldGen.genRand.Next(twilightX + 90, twilightX + TwilightWidth - 110), WorldGen.genRand.Next(twilightY + 120, twilightY + TwilightHeight));

			int treeCount = WorldGen.genRand.Next(2, 4);

			// make twilight trees
			for (int i = 0; i < treeCount; i++)
			{
				int treeX = WorldGen.genRand.Next(twilightX + 50, twilightX + TwilightWidth - 50);
				int treeY = 250;

				while (!Main.tile[treeX, treeY].HasTile)
					treeY++;

				if (Main.tile[treeX, treeY].TileType == ModContent.TileType<TFWood>() || Main.tile[treeX, treeY].TileType == ModContent.TileType<TFLeaf>())
				{
					i--;
					continue;
				}

				GenerateTwilightLivingTree(treeX, treeY);
			}

			bool IsTwilightBlock(int i, int j)
				=> Main.tile[i, j].TileType == ModContent.TileType<TwilightGrass>()
				|| Main.tile[i, j].TileType == ModContent.TileType<TwilightDirt>()
				|| Main.tile[i, j].TileType == ModContent.TileType<TwilightStone>();
		}

		public static void GenerateUndergroundRoom(int i, int j)
		{
			int width = WorldGen.genRand.Next(18, 25);
			int height = WorldGen.genRand.Next(8, 11);

			// make the room
			for (int x = i; x < i + width; x++)
			{
				for (int y = j; y < j + height; y++)
				{
					Tile tile3 = Main.tile[x, y];
					if (x == i || x == i + width - 1 || y == j)
						WorldGen.TileRunner(x, y, 2f, 2, ModContent.TileType<TwilightBrick>(), true);
					else if (y == j + height - 1)
					{
						tile3.TileType = (ushort)ModContent.TileType<TwilightBrick>();
						tile3.HasTile = true;
						//Main.tile[x, y].slope(0);
						tile3.BlockType = 0;
					}
					else
						tile3.HasTile = false;

					WorldGen.SquareTileFrame(x, y);

					if (!Main.tile[x, y].HasTile)
						Main.tile[x, y].WallType = (ushort)ModContent.WallType<TwilightBrickWall>();
				}
			}

			int floorY = j + height - 2;

			// put down the furniture and stuff
			int tablePointX = WorldGen.genRand.Next(i + 2, i + width - 2);
			WorldGen.PlaceTile(tablePointX, floorY, ModContent.TileType<TwilightTable>(), true);

			int chairOffsetX = WorldGen.genRand.NextBool() ? -1 : 3;
			WorldGen.PlaceTile(tablePointX + chairOffsetX, floorY, ModContent.TileType<TwilightChair>(), true, style: chairOffsetX == -1 ? 1 : 0);

			WorldGen.PlaceTile(tablePointX + WorldGen.genRand.Next(3), floorY - 2, ModContent.TileType<TwilightLantern>(), true);

			int chestX = WorldGen.genRand.Next(i + 2, i + width - 2);

			while (Main.tile[chestX, floorY].HasTile || Main.tile[chestX + 1, floorY].HasTile)
				chestX = WorldGen.genRand.Next(i + 2, i + width - 2);

			// using Main.chest[chestID], you can add whatever items you want
			int chestID = WorldGen.PlaceChest(chestX, floorY);


			for (int x = i + 2; x < i + width - 2; x++)
				if (!Main.tile[x, floorY].HasTile && !Main.tile[x + 1, floorY].HasTile && WorldGen.genRand.Next(3) == 0)
					WorldGen.PlaceTile(x, floorY - 1, ModContent.TileType<TwilightPot>(), true);
		}

		public static void GenerateTwilightLivingTree(int i, int j)
		{
			int treeHeight = WorldGen.genRand.Next(29, 39);
			int tendrilDirection = WorldGen.genRand.NextBool() ? 1 : -1;

			WorldGen.TileRunner(i, j - treeHeight, 25f, 9, ModContent.TileType<TFLeaf>(), true);

			for (int y = j; y > j - treeHeight; y--)
			{
				int offset = (int)(Math.Sin(y) * WorldGen.genRand.NextFloat(1.8f, 2.5f));
				WorldGen.TileRunner(i + offset, y, 4f, 2, ModContent.TileType<TFWood>(), true);

				if (y % 10 == 0)
				{
					GenerateBranch(i + offset, y, tendrilDirection);
					tendrilDirection *= -1;
				}
			}

			// fix the frames of the tree
			for (int x = i - 12; x < i + 12; x++)
				for (int y = j - treeHeight - 30; y < j; y++)
					WorldGen.SquareTileFrame(x, y);

			void GenerateBranch(int x, int y, int direction)
			{
				int tendrilLength = WorldGen.genRand.Next(8, 12);

				WorldGen.TileRunner(x + (tendrilLength - 1) * direction, y - (int)Math.Pow(1.35f, tendrilLength), 15f, 4, ModContent.TileType<TFLeaf>(), true);

				for (int a = 0; a < tendrilLength; a++)
				{
					WorldGen.TileRunner(x + a * direction, y - (int)Math.Pow(1.35f, a), 1.27f, 1, ModContent.TileType<TFWood>(), true);
				}
			}
		}

		private void GenerateTwilightTrees(GenerationProgress progress)
		{
			progress.Message = "Twilight Trees";

			int x = twilightX - 250;

			for (int i = 0; i < 2; i++)
			{
				x += 50 + WorldGen.genRand.Next(201);
				int y = twilightY - TwilightSize / 4;

				while (!Main.tile[x, y].HasTile)
					y++;

				TileRunnerTree tr = new TileRunnerTree(new Vector2(x, y));

				if (WorldGen.genRand.NextBool())
				{
					y -= 8 + WorldGen.genRand.Next(9);
					tr.pos.Y = y;

					TileRunnerCave[] trs = new TileRunnerCave[4];

					for (int j = 0; j < 4; j++)
					{
						int x2 = x - 9 + j * 6;
						int y2 = y;
						while (!Main.tile[x2, y2].HasTile)
							y2++;
						trs[j] = new TileRunnerCave(new Vector2(x, y), new Vector2(x2, y2), 4, true);
						trs[j].steps = 40;
						trs[j].stepsLeft = 40;
						trs[j].type = (ushort) TileType<TFWood>();
						trs[j].addTile = true;
					}

					for (int j = 0; j < 4; j++)
					{
						trs[j].Start();
					}

					tr.strength = 6.5;
				}
				tr.Start();
			}
		}

		public override void PostWorldGen()
        {   //mostly copied from examplemod
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].TileType == TileID.Containers) //2 * 36 == locked dungeon chest
                {
					switch (Main.tile[chest.x, chest.y].TileFrameX / 36)
					{
						case 50:
							for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
							{
								if (chest.item[inventoryIndex].type == 0)
								{   //first empty inventory slot
									chest.item[inventoryIndex].SetDefaults(ItemType<Items.Sets.PreHardmode.Granite.GraniteBar>());
									chest.item[inventoryIndex].stack = WorldGen.genRand.Next(8, 13);
									break;
								}
							}
							break;
						case 0:
							for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
							{
								if (chest.item[inventoryIndex].type == 0)
								{
									if (!WorldGen.genRand.NextBool(3))
									{
										chest.item[inventoryIndex].SetDefaults(ItemType<Items.GoliathPotion>());
										chest.item[inventoryIndex].stack = WorldGen.genRand.Next(2, 4);
									}
									break;
								}
							}
							break;
					}
				}
            }
		}
	}
  
		public class EOCDropCondition : IItemDropRuleCondition
		{
			public bool CanDrop(DropAttemptInfo info) {
				if (!info.IsInSimulation) {
					return !EmperialWorld.downedEye;
				}
				return false;
			}
	
			public bool CanShowItemDropInUI() {
				return true;
			}
	
			public string GetConditionDescription() {
				return "Drops only once";
			}
		}

       
   }
	
	