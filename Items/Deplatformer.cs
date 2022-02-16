using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace Emperia.Items {
	public class Deplatformer : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Deplatformer");
			Tooltip.SetDefault("Can chop a row of several platforms all at once");
		}
		public override void SetDefaults()
		{
			Item.width = 46;
			Item.height = 46;
			Item.useTime = 34;
			Item.useAnimation = 34;
			//Item.axe = 75;
			//Item.useTurn = false;
			Item.UseSound = SoundID.Item1;
			Item.useStyle = 1;
			Item.value = 50000;
			Item.rare = 2;
			Item.autoReuse = true;
			Item.noMelee = false;
			Item.damage = 1;
        }

        public int? nextChoppedX;
		public int nextChoppedY;
		public int initialPlayerDirection;

        //public override float UseTimeMultiplier(Player player) woops this breaks it also it must need an unmodified speed?
        //{
        //	return player.tileSpeed;
        //}

        public override void ModifyTooltips(List<TooltipLine> tooltips)
        {
			TooltipLine damage = tooltips.FirstOrDefault(x => x.Name == "Damage" && x.mod == "Terraria");
			if (damage != null) tooltips.Remove(damage);
			TooltipLine crit = tooltips.FirstOrDefault(x => x.Name == "CritChance" && x.mod == "Terraria");
			if (crit != null) tooltips.Remove(crit);
			TooltipLine kback = tooltips.FirstOrDefault(x => x.Name == "Knockback" && x.mod == "Terraria");
			if (kback != null) tooltips.Remove(kback);
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

			if (Math.Abs(cursorDistanceX) <= rangeX && Math.Abs(cursorDistanceY) <= rangeY) // that last one should not need to be a condition by any means but i fucking swear i got -2 ammo once and i cant recreate it
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
        public override void MeleeEffects(Player player, Rectangle hitbox)
        {
			if (player.itemAnimation % 3 == 0 && nextChoppedX != null)
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
}
