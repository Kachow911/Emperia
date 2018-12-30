using System.IO;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
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
			int genIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Final Cleanup"));
			tasks.Insert(genIndex + 1, new PassLegacy("Volcanic Spreading", delegate (GenerationProgress progress)
			{
		
				progress.Message = "Volcano Spreading";
				int volcanoBiomeLength = Main.rand.Next(240, 320);
				int xSpawn = 0;
				if (Terraria.Main.dungeonX > Main.maxTilesX / 2)
				{
					xSpawn = WorldGen.genRand.Next((Main.maxTilesX / 2) - 500, (Main.maxTilesX / 2) -100);
				}
				else 
				{
					xSpawn = WorldGen.genRand.Next((Main.maxTilesX / 2) + 100, (Main.maxTilesX / 2) + 500);
				}
				int yAxis = Main.spawnTileY;
				for (int y = 0; y < Main.maxTilesY; y++)
				{
					for (int i = xSpawn - volcanoBiomeLength / 2; i < xSpawn + volcanoBiomeLength / 2; i++)
					{
						if (Main.tile[i, y] != null)
						{
							if (Main.tile[i, y].active())
							{
								if (Main.tile[i, y].type == TileID.Trees)
									WorldGen.KillTile(i, y);
								if (Main.tile[i, y- 1] == null && Main.rand.NextBool(10))
									Main.tile[i, y].type = TileID.Lavafall;
									
								Main.tile[i, y].type = (ushort)mod.TileType("VolcanoTile");
							}
						}
					}
				}
				
				
            }));
		}

       
    }

}	