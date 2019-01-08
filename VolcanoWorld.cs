using System.IO;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;

namespace Emperia
{
    public class VolcanoWorld : ModWorld
    {
		
		
		public override void ResetNearbyTileEffects()
		{
			
		}
		public override void TileCountsAvailable(int[] tileCounts)
		{
			
		}
	
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
				WorldMethods.TileRunner(x, y + depth, (double)125 + Main.rand.Next(75), 1, mod.TileType("VolcanoTile"), false, 0f, 0f, true, true); //improve basic shape later
				}
			}
			
			//random bits of lava above ground, that fall. Despite not being the "prettiest" method, it probably looks and works the best.
			for (int r = 0; r < 250; r++)
			{
				Tile tile = Main.tile[x + Main.rand.Next(-75, 75), y - Main.rand.Next(10,85)];
				tile.liquid = 255;
				tile.lava(true);
			}
			//A line of consecutive "spikes" along the ground.
			for (int k = x - 85; k < x + 85; k++)
			{
				WorldMethods.CragSpike(k, (int)(y - Main.rand.Next(10,28)), 1, 30, (ushort)mod.TileType("VolcanoStone"), (float)Main.rand.Next(2, 6), (float)Main.rand.Next(2, 6));
			}
			
			//the main volcano
			WorldMethods.MainVolcano(x, (int)(y - Main.rand.Next(70,90)), 3, 130, (ushort)mod.TileType("VolcanoStone"), (float)(Main.rand.Next(400, 600) / 100), (float)(Main.rand.Next(400, 600) / 100));
			
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
				tile.liquid = 255;
				tile.lava(true);
			}
			
			//generates the ore
			for (int OreGen = 0; OreGen < 100; OreGen++)
				{ 
					int orex= x + Main.rand.Next(-75, 75);
					int orey = y + Main.rand.Next(-20, 300);
					if (Main.tile[orex, orey].type == mod.TileType("VolcanoStone"))
					{
						WorldGen.TileRunner(orex, orey, (double)WorldGen.genRand.Next(3, 6), WorldGen.genRand.Next(3, 6), mod.TileType("MoltenOre"), false, 0f, 0f, false, true);
					}				   // A = x, B = y.
				}
				
				//Turns nearby water into lava.
			for (int LiquidX = -110; LiquidX < 110; LiquidX++)
			{
				for (int LiquidY = -20; LiquidY < 150; LiquidY++)
			{
				Tile tile = Main.tile[x + LiquidX, y + LiquidY];
				if (tile.liquid > 0)
				{
				tile.lava(true);
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
                        if (Main.tile[i, j].type == (ushort)TileArray[ohgodilovememes])
                        {
                            return false;
                        }
                    }
                }
            }
            return true;
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
				int vLength = WorldGen.genRand.Next(400, 450);
				int xSpawn = 0;
				int yAxis = Main.spawnTileY;
                if (Terraria.Main.dungeonX > Main.maxTilesX / 2) //rightside dungeon
				{
					xSpawn = WorldGen.genRand.Next(Main.spawnTileX - 400, Main.spawnTileX - 100);
				}
				else //leftside dungeon
				{
					xSpawn = WorldGen.genRand.Next(Main.spawnTileX + 100, Main.spawnTileX + 400);
				}
				for (int y = 0; y < yAxis + 200; y++)
				{
					for (int x = xSpawn - vLength / 2; x <= xSpawn + vLength / 2; x++)
					{
						WorldGen.KillWall(x, y);
						if (Main.tile[x, y] != null && Main.tile[x, y].active())
						{
                            if (Main.tile[x, y].type == TileID.Trees)
                                WorldGen.KillTile(x, y);
                            else
							    Main.tile[x, y].type = (ushort)mod.TileType("VolcanoTile");
							if (Framing.GetTileSafely(x,y-1).type==0 && Main.rand.NextBool(3))
							{
								Main.tile[x, y-1].liquid = 255;
								Main.tile[x, y-1].lava(true);
							}
							//if (Main.rand.Next(500) == 6)
							//{
								//MakeCircle(x, y, 2, mod.TileType("MoltenOre"));
							//}
							if (Framing.GetTileSafely(x,y-1).type==0 && Framing.GetTileSafely(x + 1,y-1).type==0 && Main.rand.Next(10) == 0)
							{
								int chest = WorldGen.PlaceChest(x, y, (ushort)mod.TileType("VolcanoChest"), false, 2);
							}
						}
                        else if (y > yAxis + 20)
                        {
                            Main.tile[x, y].active(true);
                            Main.tile[x, y].type = (ushort) mod.TileType("VolcanoTile");
                        }
                        

                    }	
				}
                for (int LiquidX = -vLength / 2; LiquidX < vLength / 2; LiquidX++)
                {
                    for (int LiquidY = -20; LiquidY < 150; LiquidY++)
                    {
                        Tile tile = Main.tile[xSpawn + LiquidX, yAxis + LiquidY];
                        if (tile.liquid > 0)
                        {
                            tile.lava(true);
                        }
                    }
                }
                WorldMethods.MainVolcano(xSpawn, (int)(yAxis - Main.rand.Next(70, 90)), 3, 100, (ushort)mod.TileType("VolcanoTile"), (float)(Main.rand.Next(400, 600) / 100), (float)(Main.rand.Next(400, 600) / 100));
                for (int j = yAxis - 100; j < yAxis + 20; j++)
                {
                    WorldGen.digTunnel(xSpawn, j, 0, 0, 8, (int)(5 + (Math.Sqrt((j + 100) - yAxis) / 1.5f)), false);

                }
                //giant gaping hole you see at the bottom.
                WorldMethods.RoundHole(xSpawn, yAxis + 30, 17, 7, 10, true);
                /*int chests = 0;
				while (chests <= 4)
				{
					int success = WorldGen.PlaceChest(xSpawn + Main.rand.Next(-100, 100), yAxis + Main.rand.Next(-20, 300), (ushort)mod.TileType("VolcanoChest"), false, 2);
					if (success > -1)
					{
						string[] lootTable = { "Hellraiser", "Eruption", "MoltenHook", };
						/*Main.chest[success].item[0].SetDefaults(mod.ItemType(lootTable[chests]), false);
						int[] lootTable2 = { mod.ItemType("MoltenChunk"), ItemID.ObsidianSkinPotion};
						Main.chest[success].item[1].SetDefaults(lootTable2[Main.rand.Next(4)], false);
						Main.chest[success].item[1].stack = WorldGen.genRand.Next(3, 8);
						Main.chest[success].item[2].SetDefaults(lootTable2[Main.rand.Next(4)], false);
						Main.chest[success].item[2].stack = WorldGen.genRand.Next(3, 8);
						Main.chest[success].item[3].SetDefaults(lootTable2[Main.rand.Next(4)], false);
						Main.chest[success].item[3].stack = WorldGen.genRand.Next(3, 8);
						Main.chest[success].item[4].SetDefaults(lootTable2[Main.rand.Next(4)], false);
						Main.chest[success].item[4].stack = WorldGen.genRand.Next(1, 2);
						chests++;
					}
				}*/




            }));
				
			
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
}

