using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items {
	public class PlatformLayer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Platform Layer");//Platform-O-Matic (steel box with sandwich shaped top and a conveyor belt mouth, red dot n green dot, gets held out, conveyor belt is animated)
			Tooltip.SetDefault("Places platforms with increased speed and range\nCan automatically extend a row of platforms horizontally");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 8; // 7?
			Item.useAnimation = 8;
			//Item.useTurn = false;
			Item.useStyle = 5;
			Item.value = 50000;
			Item.rare = 2;
			Item.autoReuse = true;
			Item.shoot = ProjectileID.WoodenArrowFriendly; //complete placeholder, only does this because it makes the item consume ammo
			Item.useAmmo = ItemID.WoodPlatform;
		}

		public Item chosenPlatform;
		public int initialPlayerDirection;

		public override bool CanConsumeAmmo(Player player)
		{
			return false;
		}
        public override bool Shoot(Player player, ProjectileSource_Item_WithAmmo source, Vector2 position, Vector2 velocity, int type, int damage, float knockback)
        {
			player.direction = initialPlayerDirection;
			//Main.NewText(initialPlayerDirection.ToString());
			return false;
        }
        public override float UseTimeMultiplier(Player player)
		{
			return player.tileSpeed;
		}
        public override bool? UseItem(Player player)
		{
			int tileX = (int)(Main.MouseWorld.X / 16);
			int tileY = (int)(Main.MouseWorld.Y / 16);
			int rangeX = Player.tileRangeX + player.blockRange; //accounts for both tool specific range and building specific range
			int rangeY = Player.tileRangeY + player.blockRange;
			int playerTileX = (int)player.Bottom.X / 16;
			int playerTileY = (int)player.Center.Y / 16;
			int cursorDistanceX = tileX - playerTileX;
			int cursorDistanceY = tileY - playerTileY;
			initialPlayerDirection = player.direction;


            if (Math.Abs(cursorDistanceX) <= rangeX && Math.Abs(cursorDistanceY) <= rangeY && chosenPlatform.stack > 0) // that last one should not need to be a condition by any means but i fucking swear i got -2 ammo once and i cant recreate it
            {
                if (!Framing.GetTileSafely(tileX, tileY).IsActive || Main.tileCut[Framing.GetTileSafely(tileX, tileY).type] == true)
                {
                    if (Framing.GetTileSafely(tileX, tileY).wall != 0)
                    {
						if (Main.tileCut[Framing.GetTileSafely(tileX, tileY).type] == true) //i dont think this can spawn bait
						{
							WorldGen.KillTile_MakeTileDust(tileX, tileY, Framing.GetTileSafely(tileX, tileY));
							WorldGen.KillTile_PlaySounds(tileX, tileY, false, Framing.GetTileSafely(tileX, tileY));
						}
						WorldGen.PlaceTile(tileX, tileY, chosenPlatform.createTile, false, true, -1, chosenPlatform.placeStyle);
						chosenPlatform.stack--;
                        return true;
                    }
                    for (int i = -1; i <= 1; i++) //idk if theres a smarter way to do this but this runs through the neighboring 3x3 tiles to see if it can place
                    {
                        for (int h = -1; h <= 1; h++)
                        {
                            if (Framing.GetTileSafely(tileX + i, tileY + h).IsActive)
                            {
								if (Main.tileCut[Framing.GetTileSafely(tileX, tileY).type] == true)
                                {
									WorldGen.KillTile_MakeTileDust(tileX, tileY, Framing.GetTileSafely(tileX, tileY));
									WorldGen.KillTile_PlaySounds(tileX, tileY, false, Framing.GetTileSafely(tileX, tileY)); 
								}
								WorldGen.PlaceTile(tileX, tileY, chosenPlatform.createTile, false, true, -1, chosenPlatform.placeStyle);
								chosenPlatform.stack--;
								return true;
                            }
                        }
                    }
                }
                if (TileID.Sets.Platforms[Framing.GetTileSafely(tileX, tileY).type])
                {
                    for (int i = 1; i <= rangeX + 2 - cursorDistanceX * player.direction; i++) //bonus 2 range because it seems nice
                    {
                        if (!Framing.GetTileSafely(tileX + i * player.direction, tileY).IsActive || Main.tileCut[Framing.GetTileSafely(tileX + i * player.direction, tileY).type] == true) //needs to mine/kill tile if cuttable
                        {
							if (Main.tileCut[Framing.GetTileSafely(tileX + i * player.direction, tileY).type] == true) 
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
			return true;
		}
    }
}
