using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons   //where is located
{
    public class TempName : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Flameforged Blade");
			Tooltip.SetDefault("Enemies Killed by the sword explode into balls of fire");
		}
        public override void SetDefaults()
        {
            item.damage = 35;
            item.melee = true;
            item.width = 44;
            item.height = 44;
            item.useTime = 21;
            item.useAnimation = 21;     
            item.useStyle = 5;
            item.knockBack = 2.25f;  
			item.crit = 6;
            item.value = 48000;        
            item.rare = 3;
			item.scale = 1f;
            item.autoReuse = true;
            item.useTurn = false; 
            item.channel = true;
            item.GetGlobalItem<GItem>().noWristBrace = true;
        }

        int delay = 0; //checks when the item starts and stops being used
        bool unsheathe = true; //first use (important since weapon can be held indefinitely)
        bool preHit = true; //whether the weapon has hit an enemy that loop
        bool initialStrike = true; //whether the weapon has hit an enemy since the player started holding click
        bool firstFrameHit = false; //makes prehit still accurate if OnHitNPC runs before UseItem

        public override bool? CanHitNPC(Player player, NPC target)
		{
            if (player.velocity.Y <= 5 || player.velocity.Y <= 11 && !initialStrike) return false;
            else return true;
        }

		public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{
            //Main.NewText(player.velocity.Y.ToString(), 255, 0, 50);
            player.fallStart = (int)(player.position.Y / 16f);
            player.velocity.Y = -1.5f + -0.5f * player.velocity.Y; 
            preHit = false;
            firstFrameHit = true;
            initialStrike = false;
		}

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
            if (delay == 0)
            {
                //Main.NewText("initiated");
                delay = (int)(item.useAnimation * player.meleeSpeed) - 1;
                if (unsheathe)
                {
				    Main.PlaySound(SoundID.Item1, player.Center);
                }

                if (firstFrameHit)
                {
                    preHit = false;
                    initialStrike = false;
                    firstFrameHit = false;
                }
                else
                {
                    preHit = true;
                    if (unsheathe) initialStrike = true;
                }
            }

            if (delay == 1)
            {
                delay = 0;
                firstFrameHit = false;
                if (!Main.mouseLeft)
                {
                    //Main.NewText("complete");
                    unsheathe = true;
                }
                else
                {
                    //Main.NewText("the cycle continues");
                    unsheathe = false;
                }
            }
            else delay--;

            if (player.velocity.Y == 0) initialStrike = true;

            if (player.direction == 1) hitbox.X += 17;
            else hitbox.X += 40;
            hitbox.Y += 75;
            //hitbox.Height -= 12;
            hitbox.Width -= 13;
		}
        public override void UseStyle(Player player)
        {
            player.itemRotation = 2.35f * player.direction;
            //Main.NewText(player.velocity.Y.ToString());
            //if (preHit) player.velocity.X = 0f;
            if (player.velocity.Y > 0 && preHit)
            {
                player.velocity.Y += 0.25f;
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                modPlayer.fastFallLength = 1; //increases maxFallSpeed
            }
        }
        public override void ModifyHitNPC (Player player, NPC target, ref int damage, ref float knockback, ref bool crit)
		{
            if (player.velocity.Y < 16)
            {
                damage = (int)(damage / 3 + (damage * (player.velocity.Y / 16)));
            }
            else damage = (int)(damage / 3 + damage);

            if (target.velocity.Y != 0 && target.knockBackResist >= 0)
			{
				target.velocity.Y += (5.5f * target.knockBackResist * ((15 + player.velocity.Y) / 30));
            }
        }
        public override Vector2? HoldoutOffset()
		{
			return new Vector2(-30, 28);
		}
	}
}