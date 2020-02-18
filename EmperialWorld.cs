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
			
			int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
			if (ShiniesIndex == -1)
			{
				// Shinies pass removed by some other mod.
				return;
			}
			tasks.Insert(ShiniesIndex + 1, new PassLegacy("Volcano", delegate (GenerationProgress progress)
            {
				int yTile = Main.spawnTileY;
				int xTile = WorldGen.genRand.Next(Main.spawnTileX - 400, Main.spawnTileX + 400);
				for (int xAdd = -150; xAdd < 10; xAdd++)
				{
					for (int yAdd = -20; yAdd < 100; yAdd++)
					{
						
						if (Main.tile[xTile + xAdd, yTile + yAdd] != null)
						{
							if (Main.tile[xTile + xAdd, yTile + yAdd].active())
							{
								int[] TileArray = { 0, 53, 116, 112, 234 }; // dirt & grass
								if (TileArray.Contains(Main.tile[xTile + xAdd, yTile + yAdd].type))
								{
									Main.tile[xTile + xAdd, yTile + yAdd].type = (ushort)mod.TileType("TwilightGrass");
								}
								int[] TileArray3 = { 5, 323 }; // tree
								if (TileArray3.Contains(Main.tile[xTile + xAdd, yTile + yAdd].type))
								{
									WorldGen.KillTile(xTile + xAdd, yTile + yAdd);
									//if (Main.tile[xTile + xAdd, yTile + yAdd + 1].type == mod.TileType("TwilightGrass"))
									//Main.tile[xTile + xAdd, yTile + yAdd].type = (ushort)mod.TileType("TFWood");

								}
								int[] TileArray8 = { 2 }; // dirt & grass
								if (TileArray8.Contains(Main.tile[xTile + xAdd, yTile + yAdd].type))
								{
									Main.tile[xTile + xAdd, yTile + yAdd].type = (ushort)mod.TileType("TwilightGrass");
									WorldGen.PlaceObject(xTile + xAdd, yTile + yAdd - 1, mod.TileType("TwilightTreeSap"));
									if (Main.rand.Next(15) == 0)
									{
										
										WorldGen.GrowTree(xTile + xAdd, yTile + yAdd - 1);
									}
									
								}
								int[] TileArray2 = { 1, 151, 161 }; // stones
								if (TileArray2.Contains(Main.tile[xTile + xAdd, yTile + yAdd].type))
								{
									Main.tile[xTile + xAdd, yTile + yAdd].type = (ushort)mod.TileType("TFWood");
								}
								
							}
						}
					}
				}
			}));

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
	
	