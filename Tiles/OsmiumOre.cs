using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using Emperia.Projectiles.Yeti;


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

        public override bool Dangersense(int i, int j, Player player)
        {
			//Color color = Main.LocalPlayer.dangerSense ? new Color(255, 50, 50, Main.mouseTextColor) : Lighting.GetColor(i, j);
			return true;
        }
        public override void NearbyEffects(int i, int j, bool closer)
		{

			//if (Tile.GetGlobalTile<GTile>().gelPad)
			//Tile[] osmiumUnchecked = new Tile[100];
			//Tile[] osmiumChecked = new Tile[100];
			//List<Tile> osmiumUnchecked = new List<Tile>();
			//closer = true; this seemingly makes no difference
			Vector2 tilePos = new Vector2(i * 16 + 8, j * 16 + 8);
			//Vector2 tile = new Vector2(i, j);
			//Player player = MyPlayer;
			Player player = Main.player[Player.FindClosest(tilePos, 16, 16)];
            float distance = (tilePos - player.Center).Length();

			NPC closestNPC = null;
			float npcDistance;
			for (int k = 0; k < Main.maxNPCs; k++)
			{
				if ((tilePos - Main.npc[k].Center).Length() < 100 && Main.npc[k].active)
                {
                    closestNPC = Main.npc[k];
					npcDistance = (tilePos - Main.npc[k].Center).Length();
				}
            }

			Vector2 playerTile = new Vector2((int)player.Center.X / 16, (int)player.Center.Y / 16);
			Vector2 closestTile = new Vector2(i, j);

			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (modPlayer.osmiumCooldown > 0) return;
				
				if (closestNPC != null)
				{
					Vector2 npcTile = new Vector2((int)closestNPC.Center.X / 16, (int)closestNPC.Center.Y / 16);
					for (int g = 0; g <= 10; g++) //since nearbyeffects always starts from upper left we can be sure this checks all relevant tiles
					{
						for (int h = 0; h <= 10; h++)
						{
							if (Framing.GetTileSafely(i + g, j + h).TileType == ModContent.TileType<OsmiumOre>())
							{
								if ((new Vector2(i + g, j + h) - npcTile).Length() < (closestTile - npcTile).Length()) closestTile = new Vector2(i + g, j + h);
							}
						}
					}
					Projectile.NewProjectile(Wiring.GetProjectileSource((int)closestTile.X, (int)closestTile.Y), closestTile * 16, closestNPC.Center - closestTile * 16, 1, 30, -2, player.whoAmI);
					modPlayer.osmiumCooldown = 60;
				}
				else if (distance < 100)
				{
				/*for (int g = -1; g <= 1; g++) //idk if theres a smarter way to do this but this runs through the neighboring 3x3 tiles to see if it can place
				{
					for (int h = -1; h <= 1; h++)
					{
						if (Framing.GetTileSafely(i + g, j + h).type == ModContent.TileType<OsmiumOre>())
						{
							//osmiumUnchecked.Add(Framing.GetTileSafely(i + g, j + h));
							Main.NewText("congrats");
                        }
					}
				}*/
					for (int g = 0; g <= 10; g++) //since nearbyeffects always starts from upper left we can be sure this checks all relevant tiles
					{
						for (int h = 0; h <= 10; h++)
						{
							if (Framing.GetTileSafely(i + g, j + h).TileType == ModContent.TileType<OsmiumOre>())
		                    {
								if ((new Vector2(i + g, j + h) - playerTile).Length() < (closestTile - playerTile).Length()) closestTile = new Vector2(i + g, j + h);
							}
						}
					}
							//ModContent.TileType<OsmiumOre>();
							//Main.NewText(Framing.GetTileSafely(i, j).type.ToString());
							//Projectile.NewProjectile(Wiring.GetProjectileSource(i, j), tilePos, player.Center - tilePos, 498, 30, 8);
							Projectile.NewProjectile(Wiring.GetProjectileSource((int)closestTile.X, (int)closestTile.Y), closestTile * 16, player.Center - closestTile * 16, 498, 30, 8);
							modPlayer.osmiumCooldown = 60;
				}
		}
	}
}