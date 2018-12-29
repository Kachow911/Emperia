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
	
		
		public override void Initialize()
		{
			

		}
		
		public override void ResetNearbyTileEffects()
		{
			
		}
		public override void TileCountsAvailable(int[] tileCounts)
		{
			
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
			tasks.Insert(genIndex + 1, new PassLegacy("Twilight Forest", delegate (GenerationProgress progress)
			{
		
				progress.Message = "Abyss Generating";
                int XTILE = Main.spawnTileX;
                int yAxis = Main.maxTilesY / 2;
				MakeCircle(XTILE, yAxis, 75, mod.TileType("AphoticStone"));
				KillCircle(XTILE, yAxis, 50);
				for (int i = 0; i < 5; i++)
				{
					int xSpawn = Main.spawnTileX + Main.rand.Next(-100, 100);
					int ySpawn = Main.maxTilesY / 12;
					MakeCircle(xSpawn, xSpawn, 10, mod.TileType("Aetherium"));
				}
				
				
            }));
		}

       
    }

}	