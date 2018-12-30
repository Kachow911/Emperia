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
	public class ExampleInstancedGlobalTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			if(Framing.GetTileSafely(i,j-1).type==0 && Main.rand.Next(100) == 0 && Main.tile[i, j].type == TileID.Stone && (NPC.downedMechBoss3 == true || NPC.downedMechBoss2 == true || NPC.downedMechBoss1 == true))
				WorldGen.PlaceObject(i,j-1,mod.TileType("VitalityCrystalTile"));
				
		}
	}
}
