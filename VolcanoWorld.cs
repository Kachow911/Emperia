using System.IO;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using Emperia.Items;
using Emperia.Items.Weapons.Volcano;
using static Terraria.ModLoader.ModContent;

namespace Emperia
{
    public class VolcanoWorld : ModSystem
    {
		
		
		public override void ResetNearbyTileEffects()
		{
			
		}
		//public override void TileCountsAvailable(int[] tileCounts)
		//{
			
		//}

		public void PlaceVolcano(int x, int y)
        {
			//some notes:
			//It is the same on every worldsize. This will be fixed.
			//It doesn't merge with the ground well AT ALL. This will also be fixed.
			//The elevation is kinda fucked.
			//Most of the values are arbituary.
			//The code does need to be cleaned up. I'll do that in the final version.
			//Most of the methods I use are in WorldMethods. Its a file i copypaste in any worldgen project I do, so it may have some unused methods.
			
			
			//basic land of the area.
	
			for (int depth = 0; depth < 100; depth++)
			{
				if (Main.rand.Next(6) == 1)
				{
				WorldMethods.TileRunner(x, y + depth, (double)125 + Main.rand.Next(75), 1, TileType<Tiles.Volcano.VolcanoTile>(), false, 0f, 0f, true, true); //improve basic shape later
				}
			}
			
			//random bits of lava above ground, that fall. Despite not being the "prettiest" method, it probably looks and works the best.
			for (int r = 0; r < 250; r++)
			{
				Tile tile = Main.tile[x + Main.rand.Next(-75, 75), y - Main.rand.Next(10,85)];
				//tile.LiquidType = 255; 
				//tile.lava(true); think this might have a different effect than you wanted sry
				tile.LiquidType = 2;
			}
			//A line of consecutive "spikes" along the ground.
			for (int k = x - 85; k < x + 85; k++)
			{
				WorldMethods.CragSpike(k, (int)(y - Main.rand.Next(10,28)), 1, 30, (ushort)TileType<Tiles.Volcano.VolcanoTile>(), (float)Main.rand.Next(2, 6), (float)Main.rand.Next(2, 6));
			}
			
			//the main volcano
			WorldMethods.MainVolcano(x, (int)(y - Main.rand.Next(70,90)), 3, 130, (ushort)TileType<Tiles.Volcano.VolcanoTile>(), (float)(Main.rand.Next(400, 600) / 100), (float)(Main.rand.Next(400, 600) / 100));
			
			//digs the tunnel down the middle
			for (int j = y - 100; j < y + 20; j++)
			{
				WorldGen.digTunnel(x, j, 0, 0, 8, (int)(5 + (Math.Sqrt((j + 100) - y) / 1.5f)), false);
				
			}
			//giant gaping hole you see at the bottom.
			WorldMethods.RoundHole(x, y + 30, 17, 7, 10, true);
			
			//more random lava bits.
			for (int r = 0; r < 2000; r++)
			{
				Tile tile = Main.tile[x + Main.rand.Next(-30, 30), y + Main.rand.Next(-20,45)];
				//tile.LiquidType = 255;
				//tile.lava(true);
				tile.LiquidType = 2;
			}
			
			//generates the ore
			for (int OreGen = 0; OreGen < 100; OreGen++)
				{ 
					int orex= x + Main.rand.Next(-75, 75);
					int orey = y + Main.rand.Next(-20, 300);
					if (Main.tile[orex, orey].TileType == TileType<Tiles.Volcano.VolcanoTile>())
					{
						WorldGen.TileRunner(orex, orey, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), TileType<Tiles.Volcano.MoltenOre>(), false, 0f, 0f, false, true);
					}				   // A = x, B = y.
				}
				
				//Turns nearby water into lava.
			for (int LiquidX = -110; LiquidX < 110; LiquidX++)
			{
				for (int LiquidY = -20; LiquidY < 150; LiquidY++)
			{
				Tile tile = Main.tile[x + LiquidX, y + LiquidY];
				if (tile.LiquidType > 0)
				{
				//tile.lava(true);
				tile.LiquidType = 2;
				}
			}
			}
		}
		
        static bool VolcanoPlacement(int x, int y)
        {
            if (x > ((Main.maxTilesX / 2) - 200) && x < ((Main.maxTilesX / 2) + 200))
            {
                return false;
            }
			if (x < 500 || x > Main.maxTilesX - 500)
			{
				return false;
			}
            for (int i = x - 32; i < x + 32; i++)
            {
                for (int j = y - 32; j < y + 32; j++)
                {
                    int[] TileArray = { TileID.BlueDungeonBrick, TileID.GreenDungeonBrick, TileID.PinkDungeonBrick, TileID.Cloud, TileID.RainCloud, 147, 53, 40, 199, 23, 25, 203 };
                    for (int ohgodilovememes = 0; ohgodilovememes < TileArray.Length - 1; ohgodilovememes++)
                    {
                        if (Main.tile[i, j].TileType == (ushort)TileArray[ohgodilovememes])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
        }
             public override void ModifyWorldGenTasks(List<GenPass> tasks, ref double totalWeight)
             { 
                 int ShiniesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
                 if (ShiniesIndex == -1)
                 {
                // Shinies pass removed by some other Mod.
                return;
                }
            /*tasks.Insert(ShiniesIndex + 1, new PassLegacy("Volcano", delegate (GenerationProgress progress)
            {
				int vLength = WorldGen.genRand.Next(400, 450);
				int xSpawn = 0;
				int yAxis = Main.SpawnTileY;
                if (Terraria.Main.dungeonX > Main.maxTilesX / 2) //rightside dungeon
				{
					xSpawn = WorldGen.genRand.Next(Main.SpawnTileX - 400, Main.SpawnTileX - 100);
				}
				else //leftside dungeon
				{
					xSpawn = WorldGen.genRand.Next(Main.SpawnTileX + 100, Main.SpawnTileX + 400);
				}
				for (int y = 0; y < yAxis + 200; y++)
				{
					for (int x = xSpawn - vLength / 2; x <= xSpawn + vLength / 2; x++)
					{
						WorldGen.KillWall(x, y);
						if (Main.tile[x, y] != null && Main.tile[x, y].HasTile)
						{
                            if (Main.tile[x, y].TileType == TileID.Trees)
                                WorldGen.KillTile(x, y);
                            else
							    Main.tile[x, y].TileType = (ushort)ModContent.TileType<VolcanoTile>();
							if (Framing.GetTileSafely(x,y-1).type==0 && Main.rand.NextBool(3))
							{
								Main.tile[x, y-1].LiquidType = 255;
								Main.tile[x, y-1].lava(true);
							}
							//if (Main.rand.Next(500) == 6)
							//{
								//MakeCircle(x, y, 2, ModContent.TileType<MoltenOre>());
							//}
							if (Framing.GetTileSafely(x,y-1).type==0 && Framing.GetTileSafely(x + 1,y-1).type==0 && Main.rand.Next(10) == 0)
							{
								int chest = WorldGen.PlaceChest(x, y, (ushort)ModContent.TileType<VolcanoChest>(), false, 2);
							}
						}
                        else if (y > yAxis + 20)
                        {
                            Main.tile[x, y].active(true);
                            Main.tile[x, y].TileType = (ushort) ModContent.TileType<VolcanoTile>();
                        }
                        

                    }	
				}
                for (int LiquidX = -vLength / 2; LiquidX < vLength / 2; LiquidX++)
                {
                    for (int LiquidY = -20; LiquidY < 150; LiquidY++)
                    {
                        Tile tile = Main.tile[xSpawn + LiquidX, yAxis + LiquidY];
                        if (tile.LiquidType > 0)
                        {
                            tile.lava(true);
                        }
                    }
                }
                WorldMethods.MainVolcano(xSpawn, (int)(yAxis - Main.rand.Next(70, 90)), 3, 100, (ushort)ModContent.TileType<VolcanoTile>(), (float)(Main.rand.Next(400, 600) / 100), (float)(Main.rand.Next(400, 600) / 100));
                for (int j = yAxis - 100; j < yAxis + 20; j++)
                {
                    WorldGen.digTunnel(xSpawn, j, 0, 0, 8, (int)(5 + (Math.Sqrt((j + 100) - yAxis) / 1.5f)), false);

                }
                //giant gaping hole you see at the bottom.
                WorldMethods.RoundHole(xSpawn, yAxis + 30, 17, 7, 10, true);
                int chests = 0;
				while (chests <= 4)
				{
					int success = WorldGen.PlaceChest(xSpawn + Main.rand.Next(-100, 100), yAxis + Main.rand.Next(-20, 300), (ushort)ModContent.TileType<VolcanoChest>(), false, 2);
					if (success > -1)
					{
						chests++;
					}
				}




            })); */
				
			
        }
		public override void PostWorldGen()
		{
			{
				for (int i = 1; i < Main.rand.Next(4, 6); i++)
				{
					int[] itemsToPlaceInGlassChestsSecondary = new int[] { ModContent.ItemType<AshenBandage>(), ModContent.ItemType<AshenStrips>(), ItemID.SilverCoin, ItemID.Bottle, ItemID.Rope };
					int itemsToPlaceInGlassChestsSecondaryChoice = 0;
					for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
					{
						Chest chest = Main.chest[chestIndex];
						if (chest != null && Main.tile[chest.x, chest.y].TileType == TileType<Tiles.Volcano.VolcanoChest>())
						{
							for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
							{
								if (chest.item[inventoryIndex].type == 0)
								{
									chest.item[inventoryIndex].SetDefaults(itemsToPlaceInGlassChestsSecondary[itemsToPlaceInGlassChestsSecondaryChoice]); //the error is at this line
									chest.item[inventoryIndex].stack = Main.rand.Next(4, 10);
									itemsToPlaceInGlassChestsSecondaryChoice = (itemsToPlaceInGlassChestsSecondaryChoice + 1) % itemsToPlaceInGlassChestsSecondary.Length;
									break;
								}
							}
						}
					}
				}
			}
			int[] itemsToPlaceInGlassChests = new int[] { ModContent.ItemType<Eruption>(), ModContent.ItemType<Hellraiser>() };
			int itemsToPlaceInGlassChestsChoice = 0;
			for (int chestIndex = 0; chestIndex < 1000; chestIndex++)
			{
				Chest chest = Main.chest[chestIndex];
				if (chest != null && Main.tile[chest.x, chest.y].TileType/*.frameX == 47 * 36*/ == TileType<Tiles.Volcano.VolcanoChest>()) // if glass chest
				{
					for (int inventoryIndex = 0; inventoryIndex < 40; inventoryIndex++)
					{
						itemsToPlaceInGlassChestsChoice = Main.rand.Next(itemsToPlaceInGlassChests.Length);
						chest.item[0].SetDefaults(itemsToPlaceInGlassChests[itemsToPlaceInGlassChestsChoice]);
						//itemsToPlaceInGlassChestsChoice = (itemsToPlaceInGlassChestsChoice + 1) % itemsToPlaceInGlassChests.Length;
						break;
					}
				}
			}
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
        
    }
	/*public class VolcanoUpdates : ModBiome
	{
		public override void UpdateBiomes()
		{
			ZoneVolcano = EmperialWorld.VolcanoTiles > 250;
			ZoneGrotto = EmperialWorld.GrottoTiles > 100;
		}

		public override void UpdateBiomeVisuals()
		{
			Player.ManageSpecialBiomeVisuals("Emperia:Volcano", ZoneVolcano);
		}
	}*/ //lol cant figure this out neither!! cringe
}

