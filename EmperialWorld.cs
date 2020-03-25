using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.Utilities;
using Emperia;
using Terraria;
using Terraria.ID;
using Terraria.Enums;
using Terraria.ModLoader;
using Terraria.ObjectData;
using Terraria.DataStructures;
using System.IO;
using System;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Terraria.ModLoader.IO;
using System.Reflection;
using Terraria.Utilities;
using System.Runtime.Serialization.Formatters.Binary;
using Emperia;

namespace Emperia
{
    public class EmperialWorld : ModWorld
    {
	
		public static int VolcanoTiles = 0;
		private static int twilightX;
		private static int twilightY;
		private const int twilightSize = 600;

		public override void Initialize()
		{
			
		}
		
		public override void ResetNearbyTileEffects()
		{
			VolcanoTiles = 0;
		}
		public override void TileCountsAvailable(int[] tileCounts)
		{
			VolcanoTiles = tileCounts[mod.TileType("VolcanoTile")];
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
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			int index = tasks.FindIndex(genPass => genPass.Name.Equals("Corruption"));
			if (index != -1)
			{
				tasks.Insert(index + 1, new PassLegacy("Twilight Trees", GenerateTwilightTrees));
				tasks.Insert(index + 1, new PassLegacy("Twilight Caves", GenerateTwilightCaves));
			}

			index = tasks.FindIndex(genPass => genPass.Name.Equals("Spreading Grass"));
			if (index != -1)
			{
				tasks.Insert(index + 1, new PassLegacy("Twilight Terrain", GenerateTwilightTerrain));
			}
		}
		private void GenerateTwilightTrees(GenerationProgress progress)
		{
			progress.Message = "Twilight Trees";

			int x = twilightX - 250;

			for (int i = 0; i < 2; i++)
			{
				x += 50 + WorldGen.genRand.Next(201);
				int y = twilightY - twilightSize / 4;

				while (!Main.tile[x, y].active())
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
						while (!Main.tile[x2, y2].active())
							y2++;
						trs[j] = new TileRunnerCave(new Vector2(x, y), new Vector2(x2, y2), 4, true);
						trs[j].steps = 40;
						trs[j].stepsLeft = 40;
						trs[j].type = (ushort) mod.TileType("TFWood");
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
		private void GenerateTwilightTerrain(GenerationProgress progress)
		{
			progress.Message = "Twilight Terrain";

			int x;
			int y;

			for (int i = 0; i < twilightSize; i++)
			{
				for (int j = 0; j < twilightSize / 2; j++)
				{
					x = i + twilightX - twilightSize / 2;
					y = j + twilightY - twilightSize / 4;

					float successChance = i >= 100 ? 60 - i * 0.1f : i * 0.1f;
					if (successChance > 30 - j * 0.1f)
					{
						successChance = 30 - j * 0.1f;
					}
					if (WorldGen.genRand.NextFloat() < successChance)
					{
						switch (Main.tile[x, y].type)
						{
							case TileID.Grass:
							case TileID.Dirt:
							case TileID.Stone:
							case TileID.ClayBlock:
							case TileID.Sand:
							case TileID.SnowBlock:
							case TileID.IceBlock:
								Main.tile[x, y].type = (ushort)mod.TileType("TwilightDirt");
								break;
						}

						if (Main.tile[x, y].wall > 0)
						{
							Main.tile[x, y].wall = WallID.ObsidianBackUnsafe;
						}

						WorldGen.SpreadGrass(x, y, mod.TileType("TwilightDirt"), mod.TileType("TwilightGrass"));

						if (Main.tile[x, y].type == mod.TileType("TwilightGrass"))// && Main.tile[x, y].slope() == 0 && !Main.tile[x, y].halfBrick() && !Main.tile[x, y - 1].active() && !Main.tile[x, y - 2].active())
						{
							WorldGen.PlaceTile(x, y - 1, TileID.Plants, true);
						}
					}
				}
			}

			x = twilightX - 275 + WorldGen.genRand.Next(76);

			while (x < twilightX + 275)
			{
				y = twilightY - twilightSize / 4;

				while (!Main.tile[x, y].active())
					y++;

				new TileRunner(new Vector2(x, y), new Vector2(-1 + WorldGen.genRand.NextFloat() * 2, -1), new Point16(-10, 10), new Point16(-10, 10), 4.5, 16, (ushort)mod.TileType("TwilightDirt"), true, true).Start();

				x += 25 + WorldGen.genRand.Next(76);
			}
		}
		private void GenerateTwilightCaves(GenerationProgress progress)
		{
			progress.Message = "Twilight Caves";

			int y;

			int width = 0;
			int size = twilightSize;
			List<int> suitablePoints = new List<int>();

			while (suitablePoints.Count == 0)
			{
				for (int i = Main.maxTilesX / 5; i < Main.maxTilesX - Main.maxTilesX / 5; i++)
				{
					y = 250;
					while (!Main.tile[i, y].active())
						y++;

					switch (Main.tile[i, y].type)
					{
						case TileID.Grass:
						case TileID.Dirt:
						case TileID.Stone:
						case TileID.ClayBlock:
						case TileID.Sand:
						case TileID.SnowBlock:
						case TileID.IceBlock:
							if (++width >= size && (i - size / 2 < Main.maxTilesX / 2 - 750 || i - size / 2 > Main.maxTilesX / 2 + 750) && (i - size / 2 < WorldGen.UndergroundDesertLocation.X - 300 || i - size / 2 > WorldGen.UndergroundDesertLocation.Right + 300))
							{
								suitablePoints.Add(i - size / 2);
							}
							break;
						default:
							width = 0;
							break;
					}
				}
				size -= 50;
			}

			twilightX = suitablePoints[WorldGen.genRand.Next(suitablePoints.Count)];

			twilightY = 250;
			while (!Main.tile[twilightX, twilightY].active())
				twilightY++;

			int x = twilightX - 250 + WorldGen.genRand.Next(301);

			Vector2[] points = new Vector2[8 + WorldGen.genRand.Next(5)];

			for (int i = 0; i < points.Length; i++)
			{
				y = twilightY - twilightSize / 4;

				while (!Main.tile[x, y].active())
					y++;
				if (i != 0 && i != points.Length - 1)
					y += 25 + WorldGen.genRand.Next(6);

				points[i] = new Vector2(x, y);

				if (i == 0)
				{
					x += WorldGen.genRand.Next(11);
				}
				else
				{
					new TileRunnerCave(points[i - 1], points[i], i == 1 || i == points.Length - 1 ? 4 : 6).Start();
					x += i == points.Length - 2 ? WorldGen.genRand.Next(11) : 10 + WorldGen.genRand.Next(16);
				}
			}

			x = (int)points[2].X;
			y = twilightY - twilightSize / 4;

			while (!Main.tile[x, y].active())
				y++;

			int point = 2 + WorldGen.genRand.Next(points.Length - 4);

			new TileRunnerCave(new Vector2(x, y), new Vector2(points[point].X - 8 + WorldGen.genRand.Next(17), points[point].Y), 4).Start();

			point = 2 + WorldGen.genRand.Next(points.Length - 4);

			new TileRunner(points[point], Vector2.Zero, new Point16(-10, 10), new Point16(-10, 10), 30, 10, 0, false, true).Start();

			new TileRunnerCave(new Vector2(points[point].X - 5, points[point].Y + 5), new Vector2(0, points[point].Y + 12), 4).Start();
			new TileRunnerCave(new Vector2(points[point].X + 5, points[point].Y + 5), new Vector2(0, points[point].Y + 12), 4).Start();
		}

		public override void PostWorldGen()
        {   //mostly copied from examplemod
            for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
            {
                Chest chest = Main.chest[chestIndex];
                if (chest != null && Main.tile[chest.x, chest.y].type == TileID.Containers && Main.tile[chest.x, chest.y].frameX == 50 * 36) //2 * 36 == locked dungeon chest
                {
                    for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
                    {
                        if (chest.item[inventoryIndex].type == 0)
                        {   //first empty inventory slot
                            chest.item[inventoryIndex].SetDefaults(mod.ItemType("GraniteBar"));
							chest.item[inventoryIndex].stack = WorldGen.genRand.Next(8, 12);
                            break;
                        }
                    }
                }
            }
		}
		}


       
    }
	
	