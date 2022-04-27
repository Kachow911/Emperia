using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Emperia.Projectiles;


namespace Emperia.Tiles
{
	public class OsmiumOre : ModTile
	{
		public override void SetStaticDefaults()
		{
			Main.tileSolid[Type] = true;
			Main.tileMergeDirt[Type] = true;
			Main.tileBlockLight[Type] = true;
			ItemDrop = ModContent.ItemType<Items.Osmium>();
			AddMapEntry(new Color(142, 156, 171));
			MineResist = 4f;
			SoundType = 21;
			DustType = 121;
		}
		
		public override bool CanExplode(int i, int j)
		{
			return false;
		}

        public override bool IsTileDangerous(int i, int j, Player player)
        {
			//Color color = Main.LocalPlayer.dangerSense ? new Color(255, 50, 50, Main.mouseTextColor) : Lighting.GetColor(i, j);
			return true;
        }
        public override void NearbyEffects(int i, int j, bool closer)
		{
			Vector2 tileCenter = new Vector2(i * 16 + 8, j * 16 + 8);
			Player player = Main.player[Player.FindClosest(tileCenter, 16, 16)];
            float playerDistance = (tileCenter - player.Center).Length();

			NPC closestNPC = null; //will be null unless an NPC is within range of a tile
			float npcDistance = 244; //+144 to account for spikes spawned from osmium up to 10 tiles away
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				if ((tileCenter - Main.npc[k].Center).Length() <= npcDistance && Main.npc[k].active)
                {
                    closestNPC = Main.npc[k];
					npcDistance = (tileCenter - Main.npc[k].Center).Length(); //theoretically there could be a list of NPCs within range but this is good enough
				}
            }

			Vector2 closestTile = new Vector2(i, j);
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();

			if (modPlayer.osmiumCooldown > 0) return;
				
				if (closestNPC != null) //this runs more often than it needs to if an NPC is to the left of or beneath the osmium ore, since the +144 distance range only needs to account for additional tiles above or to the right
				{
					Vector2 npcTile = new Vector2((int)closestNPC.Center.X / 16, (int)closestNPC.Center.Y / 16);
					for (int g = 0; g <= 10; g++) //Checks all relevant tiles in a 10x10 to set closestTile. Nearbyeffects starts from bottom? left 
					{
						for (int h = 0; h <= 10; h++)
						{
							if (Framing.GetTileSafely(i + g, j + h).TileType == ModContent.TileType<OsmiumOre>())
							{
								if ((new Vector2(i + g, j + h) - npcTile).Length() < (closestTile - npcTile).Length())
								{
									closestTile = new Vector2(i + g, j + h);
									npcDistance = (closestTile - npcTile).Length(); //necessary to check if the distance is under 100 rather than under 244 later
								}
							}
						}
					}
				}
				else if (playerDistance < 100)
				{
					Vector2 playerTile = new Vector2((int)player.Center.X / 16, (int)player.Center.Y / 16);
					for (int g = 0; g <= 10; g++) //Checks all relevant tiles in a 10x10 to set closestTile. Nearbyeffects starts from bottom? left 
					{
						for (int h = 0; h <= 10; h++)
						{
							if (Framing.GetTileSafely(i + g, j + h).TileType == ModContent.TileType<OsmiumOre>())
		                    {
								if ((new Vector2(i + g, j + h) - playerTile).Length() < (closestTile - playerTile).Length()) closestTile = new Vector2(i + g, j + h);
							}
						}
					}
				}
				if (closestNPC != null && npcDistance < 100)
				{ 
					//Main.NewText(npcDistance.ToString());
					//Main.NewText((closestTile - new Vector2((int)closestNPC.Center.X / 16, (int)closestNPC.Center.Y / 16)).Length().ToString(), 0);
					int spike = Projectile.NewProjectile(Wiring.GetProjectileSource((int)closestTile.X, (int)closestTile.Y), closestTile * 16 + new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<OsmiumSpike>(), 30, -2, player.whoAmI);
					Main.projectile[spike].rotation = (closestTile * 16 + new Vector2(8, 8) - closestNPC.Center).ToRotation() - 1.57f;
					Main.projectile[spike].position += new Vector2(-48, -48) * Vector2.Normalize(closestTile * 16 + new Vector2(8, 8) - closestNPC.Center);
					Main.projectile[spike].friendly = true;
					modPlayer.osmiumCooldown = 60;
				}
				else if (playerDistance < 100)
			    {
					int spike = Projectile.NewProjectile(Wiring.GetProjectileSource((int)closestTile.X, (int)closestTile.Y), closestTile * 16 + new Vector2(0, 8), Vector2.Zero, ModContent.ProjectileType<OsmiumSpike>(), 30, -2, player.whoAmI);
					Main.projectile[spike].rotation = (closestTile * 16 + new Vector2(8, 8) - player.Center).ToRotation() - 1.57f;
					Main.projectile[spike].position += new Vector2(-48, -48) * Vector2.Normalize(closestTile * 16 + new Vector2(8, 8) - player.Center);
					Main.projectile[spike].hostile = true;
					modPlayer.osmiumCooldown = 60;
				}
		}
	}
	//should make tiles in range tileentities at some point, so they update every frame, probably
}