using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;



namespace Emperia.Items;
public class PlatformLayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Platform-O-Matic");
			// Tooltip.SetDefault("Places platforms with increased speed and range\nCan automatically extend a row of platforms horizontally\nRight Click to switch to chopping mode");
			Main.RegisterItemAnimation(Item.type, new DrawAnimationVertical(6, 6) { NotActuallyAnimating = true });
        }
        public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 46;
			Item.noUseGraphic = true;
			Item.useTime = 8; // 7?
			Item.useAnimation = 8;
			Item.useStyle = 5;
			Item.value = 50000;
			Item.rare = 2;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.WoodenArrowFriendly; //this is the only way i can find to make the item consume ammo
			Item.useAmmo = ItemID.WoodPlatform;
			//Item.useTurn = false;

			Item.noMelee = false;
			Item.damage = 1;

		}

		public int useMode = 1;

		public Item chosenPlatform; //set in globalitem pickammo
		public int initialPlayerDirection;

	    public int? nextChoppedX;
		public int nextChoppedY;
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine damage = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.Mod == "Terraria");
			if (damage != null) tooltips.Remove(damage);
			TooltipLine crit = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.Mod == "Terraria");
			if (crit != null) tooltips.Remove(crit);
			TooltipLine kback = tooltips.FirstOrDefault(x => x.Name == "Knockback" && x.Mod == "Terraria");
			if (kback != null) tooltips.Remove(kback);
			TooltipLine speed = tooltips.FirstOrDefault(x => x.Name == "Speed" && x.Mod == "Terraria");
			if (speed != null) tooltips.Remove(speed);
		}
		public override bool CanConsumeAmmo(Item ammo, Player player)
		{
			return false;
		}
	    public override bool CanUseItem(Player player)
	    {
			if (player.altFunctionUse == 2) //allows alt click and chop mode to be used even when out of platforms (ammo)
			{
				if (useMode == 1) Item.useAmmo = ItemID.None;
				else if (useMode == 2) Item.useAmmo = ItemID.WoodPlatform;
			}
			return base.CanUseItem(player);
		}
	    public override bool Shoot(Player player, EntitySource_ItemUse_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			player.direction = initialPlayerDirection;
			//Main.NewText(initialPlayerDirection.ToString());
			return false;
        }
        public override float UseSpeedMultiplier(Player player) //usespeedmultiplier divides instead of multiplying use time, so im getting the multiplicative inverse of everything
		{
			if (player.altFunctionUse == 2) return (1 / 1.5f);
			if (useMode == 1) return (1 / player.tileSpeed);
			if (useMode == 2) return (1 / 4.25f);
			return base.UseSpeedMultiplier(player);
		}
		public override bool AltFunctionUse(Player player)
		{
		    return true;
		}
		public override bool? UseItem(Player player)
		{

			for (int i = 0; i < 251; ++i) //makes visual use sprite
			{
				if (i == 250)
				{
					int p = Projectile.NewProjectile(player.GetSource_ItemUse(Item), player.Center.X, player.Center.Y, 0f, 0f, ModContent.ProjectileType<PlatformLayerVisual>(), 0, 0, Main.myPlayer, 0, 0);
					(Main.projectile[p].ModProjectile as PlatformLayerVisual).useMode = useMode;
				}
				if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == ModContent.ProjectileType<PlatformLayerVisual>()) i = 251;
			}

			if (player.altFunctionUse == 2)
			{
				if (useMode == 1) useMode = 2;
				else useMode = 1;
			}

			int tileX = (int)(Main.MouseWorld.X / 16);
			int tileY = (int)(Main.MouseWorld.Y / 16);
			int rangeX = Player.tileRangeX + player.blockRange; //accounts for both tool specific range and building specific range
			int rangeY = Player.tileRangeY + player.blockRange;
			int playerTileX = (int)player.Bottom.X / 16;
			int playerTileY = (int)player.Center.Y / 16;
			int cursorDistanceX = tileX - playerTileX;
			int cursorDistanceY = tileY - playerTileY;
			initialPlayerDirection = player.direction;

			if (useMode == 1 && player.altFunctionUse != 2)
			{
				if (Math.Abs(cursorDistanceX) <= rangeX && Math.Abs(cursorDistanceY) <= rangeY && chosenPlatform.stack > 0) // that last one should not need to be a condition by any means but i fucking swear i got -2 ammo once and i cant recreate it
				{
					if (!Framing.GetTileSafely(tileX, tileY).HasTile || Main.tileCut[Framing.GetTileSafely(tileX, tileY).TileType] == true || TileID.Sets.BreakableWhenPlacing[Framing.GetTileSafely(tileX, tileY).TileType] == true)
					{
						if (Framing.GetTileSafely(tileX, tileY).WallType != 0)
						{
							if (Main.tileCut[Framing.GetTileSafely(tileX, tileY).TileType] == true) //i still dont think this can spawn bait (multitiles can spawn bait from adjacent blocks as they count separately)
							{
								WorldGen.KillTile_MakeTileDust(tileX, tileY, Framing.GetTileSafely(tileX, tileY));
								WorldGen.KillTile_PlaySounds(tileX, tileY, false, Framing.GetTileSafely(tileX, tileY));
							}
							WorldGen.PlaceTile(tileX, tileY, chosenPlatform.createTile, false, true, -1, chosenPlatform.placeStyle);
							chosenPlatform.stack--;
							return true;
						}

						for (int i = -1; i <= 1; i++) //runs through the neighboring 3x3 tiles to see if it can place
						{
							for (int h = -1; h <= 1; h++)
							{
								if (Framing.GetTileSafely(tileX + i, tileY + h).HasTile)
								{
									if (Framing.GetTileSafely(tileX, tileY).HasTile)
									{
										WorldGen.KillTile_MakeTileDust(tileX, tileY, Framing.GetTileSafely(tileX, tileY));
										WorldGen.KillTile_PlaySounds(tileX, tileY, false, Framing.GetTileSafely(tileX, tileY));
										//WorldGen.KillTile_GetItemDrops(tileX, tileY, Main.tile[tileX, tileY], Main.tile[tileX, tileY].type.dropItem); 
								}
									WorldGen.PlaceTile(tileX, tileY, chosenPlatform.createTile, false, true, -1, chosenPlatform.placeStyle);
									chosenPlatform.stack--;
									return true;
								}
							}
						}
					}
					if (TileID.Sets.Platforms[Framing.GetTileSafely(tileX, tileY).TileType])
					{
						for (int i = 1; i <= rangeX + 2 - cursorDistanceX * player.direction; i++) //bonus 2 range, as a treat
						{
							if (!Framing.GetTileSafely(tileX + i * player.direction, tileY).HasTile || Main.tileCut[Framing.GetTileSafely(tileX + i * player.direction, tileY).TileType] == true || TileID.Sets.BreakableWhenPlacing[Framing.GetTileSafely(tileX + i * player.direction, tileY).TileType] == true)
							{
								if (Framing.GetTileSafely(tileX + i * player.direction, tileY).HasTile)
								{
									WorldGen.KillTile_MakeTileDust(tileX + i * player.direction, tileY, Framing.GetTileSafely(tileX, tileY));
									WorldGen.KillTile_PlaySounds(tileX + i * player.direction, tileY, false, Framing.GetTileSafely(tileX, tileY));
								}
								WorldGen.PlaceTile(tileX + i * player.direction, tileY, chosenPlatform.createTile, false, true, -1, chosenPlatform.placeStyle);
								chosenPlatform.stack--;
								return true;
							}
						}
					}
				}
            }

			if (useMode == 2 && player.altFunctionUse != 2)
			{
				nextChoppedX = null;
	
				/*if (Main.MouseWorld.X > player.position.X && player.direction == -1)
				{
					player.direction = 1;
					Item.useTurn = false;
				}
				if (Main.MouseWorld.X < player.position.X && player.direction == 1)
				{
					player.direction = -1;
					Item.useTurn = false;
				}*/
	
				if (Math.Abs(cursorDistanceX) <= rangeX && Math.Abs(cursorDistanceY) <= rangeY)
				{
					if (TileID.Sets.Platforms[Framing.GetTileSafely(tileX, tileY).TileType] == true)
					{
						player.PickTile(tileX, tileY, 59);
						nextChoppedX = tileX + 1 * player.direction;
						nextChoppedY = tileY;
						initialPlayerDirection = player.direction;
						/*for (int i = 1; i <= rangeX + 2 - cursorDistanceX * player.direction; i++) //bonus 2 range because it seems nice
	                    {
	                        if (TileID.Sets.Platforms[Framing.GetTileSafely(tileX + i * player.direction, tileY).type] == true)
	                        {
								player.PickTile(tileX + i * player.direction, tileY, 59);
	                        }
	                    }*/
						//return true;
					}
				}
				return true;
			}
			return true;
		}
	    public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if (player.itemAnimation % 3 == 0 && nextChoppedX != null && useMode == 2 && player.altFunctionUse != 2)
			{
				if (TileID.Sets.Platforms[Framing.GetTileSafely((int)nextChoppedX, nextChoppedY).TileType] == true)
				{
					player.PickTile((int)nextChoppedX, nextChoppedY, 59);
					nextChoppedX += 1 * initialPlayerDirection;
				}
				else nextChoppedX = null;
			}
		}
        public override bool? CanHitNPC(Player player, NPC target)
        {
            return false;
        }
    }
	public class PlatformLayerVisual : ModProjectile
	{

		public override void SetDefaults()
		{
			Projectile.damage = 0;
			Projectile.width = 34;
			Projectile.height = 24;
			Projectile.tileCollide = false; 
			Main.projFrames[Projectile.type] = 6;
		}
		public int useMode = 1;
		bool init = false;


		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			if (player.itemTime == 0) Projectile.Kill();
			player.heldProj = Projectile.whoAmI;
			
			if (!init && useMode == 2)
			{
				Projectile.frame = 3;
				init = true;
			}
			
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				if (player.altFunctionUse == 2) Projectile.frame = Projectile.frame = (Projectile.frame + 3) % 6;
				else if (useMode == 1) Projectile.frame = (Projectile.frame + 1) % 3;
				else if (useMode == 2) Projectile.frame = (Projectile.frame + 1) % 3 + 3;
				//Main.NewText(Projectile.frame.ToString());
			}

			Vector2 offset = new Vector2(player.direction * 19, player.gravDir * -5); //code beneath this adapted from vanilla medusa head projectile
			if (player.gravDir == -1f)
			{
				offset.Y -= 18;
			}
			//if (velocity.X != base.velocity.X || velocity.Y != base.velocity.Y)
			//{
			//	this.netUpdate = true;
			//}
			Projectile.velocity = player.GetModPlayer<MyPlayer>().MouseDirection(); //no idea why this works, maybe OffsetsPlayerOnhand code checks projectile velocity to decide its direction?
			Vector2 value = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
			if (player.direction != 1)
			{
				value.X = (float)player.bodyFrame.Width - value.X;
			}
			value -= (player.bodyFrame.Size() - new Vector2((float)player.width, 42f)) / 2f;
			Projectile.Center = (player.position + value + offset - player.GetModPlayer<MyPlayer>().MouseDirection()).Floor();
			Projectile.gfxOffY = player.gfxOffY; //for some reason this works without setting the projectile position after
			Projectile.spriteDirection = player.direction;
			//Projectile.rotation = ((player.gravDir == 1f) ? 0f : ((float)Math.PI));
		}
}

