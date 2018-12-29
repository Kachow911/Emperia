using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class PiranhaProjectile : ModProjectile
    {
        bool latched;
        bool returning;
		int returntimer = 45;
		
		NPC npc;
		Vector2 offset;

        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.alpha = 0;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 1000;
			Main.projFrames[projectile.type] = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Piranha");
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (!latched)
			{
				npc = target;
				offset = projectile.position - npc.position;
				latched = true;
				returntimer = 60;
			}
        }

        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            Vector2 playerCenter = player.MountedCenter;
            if ((double) projectile.velocity.X < 0.0)
            {
                projectile.spriteDirection = -1;
                projectile.rotation = (float) Math.Atan2(-(double) projectile.velocity.Y, -(double) projectile.velocity.X);
            }
            else
            {
                projectile.spriteDirection = 1;
                projectile.rotation = (float) Math.Atan2((double) projectile.velocity.Y, (double) projectile.velocity.X);
            }
			
			returntimer--;
			if (returntimer <= 0)
			{
				returning = true;
			}

            if (player.releaseUseItem && projectile.timeLeft <= 990)
            {
                returning = true;
                returntimer = 0;
            }
			
			if (returning)
			{
				projectile.tileCollide = false;
				Vector2 returnVelocity = playerCenter - projectile.position;
				returnVelocity.Normalize();
				returnVelocity *= 12f;
				projectile.velocity = returnVelocity;
				
				if (Vector2.Distance(playerCenter, projectile.position) <= 10f || Vector2.Distance(playerCenter, projectile.position) >= 5000f)
				{
					projectile.Kill();
				}
			}
			
			if (latched && returntimer > 0)
			{
				if (!npc.active)
				{
					returning = true;
                    returntimer = 0;
				}
				projectile.rotation = (float) Math.Atan2(-(double)offset.Y, -(double)offset.X);
				projectile.frameCounter++;
				projectile.velocity = Vector2.Zero;
				projectile.position = npc.position + offset;
				if (projectile.frameCounter >= 3)
				{
					projectile.frameCounter = 0;
					projectile.frame = (projectile.frame + 1) % 3;
				} 
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            returning = true;
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 0);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
			}
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
            return false;
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Vector2 vector2 = new Vector2(projectile.Center.X, projectile.Center.Y);
            float num1 = Main.player[projectile.owner].MountedCenter.X - vector2.X;
            float num2 = Main.player[projectile.owner].MountedCenter.Y - vector2.Y;
            float rotation = (float)Math.Atan2((double)num2, (double)num1);
            if (projectile.alpha == 0)
            {
                int num3 = -1;
                if ((double)projectile.position.X + (double)(projectile.width / 2) < (double)Main.player[projectile.owner].MountedCenter.X)
                    num3 = 1;
                Main.player[projectile.owner].itemRotation = Main.player[projectile.owner].direction != 1 ? (float)Math.Atan2((double)num2 * (double)num3, (double)num1 * (double)num3) : (float)Math.Atan2((double)num2 * (double)num3, (double)num1 * (double)num3);
            }
            bool flag = true;
            while (flag)
            {
                float f = (float)Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2);
                if ((double)f < 25.0)
                    flag = false;
                else if (float.IsNaN(f))
                {
                    flag = false;
                }
                else
                {
                    float num3 = projectile.type == 154 || projectile.type == 247 ? 18f / f : 12f / f;
                    float num4 = num1 * num3;
                    float num5 = num2 * num3;
                    vector2.X += num4;
                    vector2.Y += num5;
                    num1 = Main.player[projectile.owner].MountedCenter.X - vector2.X;
                    num2 = Main.player[projectile.owner].MountedCenter.Y - vector2.Y;
                    Microsoft.Xna.Framework.Color color = Lighting.GetColor((int)vector2.X / 16, (int)((double)vector2.Y / 16.0));
                    Main.spriteBatch.Draw(mod.GetTexture("Projectiles/Tether"), new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 12, 6)), color, rotation, new Vector2((float)12 * 0.5f, (float)6 * 0.5f), 1f, SpriteEffects.None, 0.0f);
                }
            }
            return true;
        }
    }
}