using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Items.Weapons
{
    public class Greatsword : ModItem
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Greatsword");
			//Tooltip.SetDefault("Enemies Killed by the sword explode into balls of fire");
		}
        public override void SetDefaults()
        {
            Item.damage = 35;
            Item.DamageType = DamageClass.Melee;
            Item.width = 44;
            Item.height = 44;
            Item.useTime = 120;
            Item.useAnimation = 120;     
            Item.useStyle = 5;
            Item.knockBack = 5f;  
			//Item.crit = 6;
            Item.value = 48000;        
            Item.rare = 3;
			Item.scale = 1f;
            //Item.autoReuse = true;
            Item.useTurn = false; 
            //drawOffsetX = -32;
            //drawOriginOffsetY = -32;
        }

        int delay = 0;

        /*public override bool? CanHitNPC(Player player, NPC target)
		{
            if (player.velocity.Y <= 5 || player.velocity.Y <= 11 && !initialStrike) return false;
            else return true;
        }*/

		/*public override void OnHitNPC(Player player, NPC target, int damage, float knockback, bool crit)
		{

		}*/

		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox)
		{
            if (delay == 0)
            {
                //Main.NewText("initiated");
                delay = (int)(Item.useAnimation * player.GetAttackSpeed(DamageClass.Melee)) - 1;
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item1, player.Center);
            }

            if (delay == 1)
            {
                delay = 0;
            }
            else delay--;
            //if (player.direction == 1) hitbox.X += 17;
            //else hitbox.X += 40;
            //hitbox.Y += 75;
            hitbox.Height -= 16;
            hitbox.Width -= 16;
            hitbox.Y += 40;
            hitbox.X += 40;
		}
        int rotationAngle = 45;
        public override void UseStyle(Player player, Rectangle heldItemFrame)
        {   
            //if ()
            //player.itemRotation = 2.35f * player.direction;
            if (delay >= 75) rotationAngle = 45 - 2 *(120 - delay);
            if (delay <= 60 && delay > 45) rotationAngle = -45 + 3 * (135 - 3 * (delay - 15));
            if (delay <= 45) rotationAngle = 90;
            //rotationAngle = 1 * (120 - delay);
            
            //int rotAngleCycle = rotationAngle;
            //if (rotationAngle > 45) rotAngleCycle = 45;
            //if (rotationAngle < -45) rotAngleCycle = -45;

            //if (rotationAngle > 60) rotAngleCycle = 60 + (int)((rotationAngle - 60) / 3);
            //if (rotationAngle < -60) rotAngleCycle = -60 - (int)((rotationAngle - -60) / 3);
            //int rotAngleCycleY = rotAngleCycle;

            //if (rotationAngle > 60) rotAngleCycle = 60 - (rotationAngle - 60);
            //if (rotationAngle < -60) rotAngleCycle = -60 - (rotationAngle - -60);

            //if (Math.Abs(rotationAngle) <= 45) rotAngleCycle = rotationAngle;
            //else rotAngleCycle = 45 - (rotationAngle - 45);
            //rotationAngle % 90
            //player.itemRotation = MathHelper.ToRadians(rotationAngle) * player.direction;
            //if (delay > 90) rotationAngle = -45;
            //else if (delay > 60) rotationAngle = 0;
            //else if (delay > 30) rotationAngle = 45;
            //else rotationAngle = 90;
            player.itemRotation = MathHelper.ToRadians(rotationAngle) * player.direction;
            //if (delay > 60) player.itemRotation = MathHelper.ToRadians(0) * player.direction;
            //else player.itemRotation = MathHelper.ToRadians(-45) * player.direction;
            //player.itemLocation.X += (-45 * (4 / 9));
            player.itemLocation.X += player.direction * (int)(rotationAngle * 0.007f * 64);
            player.itemLocation.Y += (int)(Math.Abs(rotationAngle) * 0.003f * 64);

            Main.NewText(rotationAngle.ToString());
            //Main.NewText(rotAngleCycle.ToString(), 155, 50, 50);

            //        modulo this by 45 so that the sword goes in and out past every 45 degrees 

            //player.itemLocation.X = player.position.X + 100;
            //Main.NewText(player.velocity.Y.ToString());
            //Main.NewText(delay.ToString());
            //if (preHit) player.velocity.X = 0f;

            //jankiness comes from rotationangle value not being reset at the end of a use
        }
        public override Vector2? HoldoutOffset()
		{
            //Main.NewText((-4 + (rotationAngle / 10)).ToString());
            //Main.NewText(rotationAngle.ToString());
			return new Vector2(-4, -20);
		}
	}
}