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
using Emperia.Buffs;
using Microsoft.Xna.Framework.Graphics;
using Terraria.DataStructures;

namespace Emperia
{
	public class GTile : GlobalTile
	{
		public override void RandomUpdate(int i, int j, int type)
		{
			if(Framing.GetTileSafely(i,j-1).TileType==0 && Main.rand.Next(250) == 0 && Main.tile[i, j].TileType == TileID.Stone && (NPC.downedMechBoss3 == true || NPC.downedMechBoss2 == true || NPC.downedMechBoss1 == true))
            {
				WorldGen.KillTile(i, j-1);
				WorldGen.PlaceTile(i, j - 1, TileType<Tiles.VitalityCrystalTile>());
			}
				
		}
        public override void DrawEffects(int i, int j, int type, SpriteBatch spriteBatch, ref TileDrawInfo drawData)
        {
			if (Main.LocalPlayer.HasBuff(ModContent.BuffType<Purgation>()))
			{
				if (TileID.Sets.Conversion.Grass[type] || TileID.Sets.Conversion.Stone[type] || TileID.Sets.Conversion.Ice[type] || TileID.Sets.Conversion.Sand[type] || TileID.Sets.Conversion.HardenedSand[type] || TileID.Sets.Conversion.Sandstone[type] || TileID.Sets.Conversion.Thorn[type])
				{
					//if (type == 25 || type == 203 || type == 117 || type == 163 || type == 164 || type == 200 || type == 112 || type == 116 || type == 234 || type == 398 || type == 399 || type == 402 || type == 400 || type == 401 || type == 403 || type == 32 || type == 352 || type == 23 || type == 199 || type == 109)
					if (type != 1 && type != 2 && type != 59 && type != 69 && type != 161 && type != 53 && type != 396 && type != 397)
					{
						float brightness = 1f;
						if (Framing.GetTileSafely(i, j).Slope != SlopeType.Solid || Framing.GetTileSafely(i, j).IsHalfBlock) brightness = 0.64f;
						if (drawData.tileLight.R < 180 * brightness) drawData.tileLight.R = (byte)(180 * brightness);
						if (drawData.tileLight.B < 50 * brightness) drawData.tileLight.B = (byte)(50 * brightness);
						if (drawData.tileLight.B < 220 * brightness) drawData.tileLight.B = (byte)(220 * brightness);
						//if (Player.tileTargetX == i && Player.tileTargetY == j) Main.NewText(drawData.finalColor);
					}
				}
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
