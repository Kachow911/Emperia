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
		}

       
    }

}	