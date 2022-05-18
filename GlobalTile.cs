using System.IO;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using static Terraria.ModLoader.ModContent;
using Emperia.Items.Sets.PreHardmode.Seashell;

namespace Emperia
{
	public class ExampleInstancedGlobalTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			if(Framing.GetTileSafely(i,j-1).TileType==0 && Main.rand.Next(250) == 0 && Main.tile[i, j].TileType == TileID.Stone && (NPC.downedMechBoss3 == true || NPC.downedMechBoss2 == true || NPC.downedMechBoss1 == true))
            {
				WorldGen.KillTile(i, j-1);
				WorldGen.PlaceTile(i, j - 1, TileType<Tiles.VitalityCrystalTile>());
			}
				
		}
        /*public override void KillTile(int i, int j, int type, ref bool fail, ref bool effectOnly, ref bool noItem)
        {
			//Player player = Main.LocalPlayer;
			//if (player != null && player.HeldItem.type == ModContent.ItemType<SeashellPickaxe>()) Main.NewText("swag");
			//if (Main.tileSpelunker[Framing.GetTileSafely(i, j).TileType]) Main.NewText(player.itemAnimation.ToString());
			//Main.NewText(player.itemAnimation.ToString());
		}*/
    }
}
