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
		bool init = false;
		int returntimer = 34;
		
		NPC NPC;
		Vector2 offset;

        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
            Projectile.ignoreWater = true;
			Main.projFrames[Projectile.type] = 3;
        }

        public override void SetStaticDefaults()
        {
            // DisplayName.SetDefault("Piranha");
        }

        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
			if (!latched)
			{
				NPC = target;
				offset = Projectile.position - NPC.position;
				latched = true;
				returntimer = 36;
			}
        }

        public override void AI()
        {
			if(!init)
			{
				init = true;
				if (Main.player[Projectile.owner].yoyoString)
					returntimer = 42;
			}
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.MountedCenter;
            if ((double) Projectile.velocity.X < 0.0)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = (float) Math.Atan2(-(double) Projectile.velocity.Y, -(double) Projectile.velocity.X);
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float) Math.Atan2((double) Projectile.velocity.Y, (double) Projectile.velocity.X);
            }
			
			returntimer--;
			if (returntimer <= 0)
			{
				returning = true;
			}

            if (player.releaseUseItem && Projectile.timeLeft <= 990)
            {
                returning = true;
                returntimer = 0;
            }
			
			if (returning)
			{
				Projectile.tileCollide = false;
				Vector2 returnVelocity = playerCenter - Projectile.position;
				returnVelocity.Normalize();
				returnVelocity *= 12f;
				Projectile.velocity = returnVelocity;
				
				if (Vector2.Distance(playerCenter, Projectile.position) <= 10f || Vector2.Distance(playerCenter, Projectile.position) >= 5000f)
				{
					Projectile.Kill();
				}
			}
			
			if (latched && returntimer > 0)
			{
				if (!NPC.active)
				{
					returning = true;
                    returntimer = 0;
				}
				Projectile.rotation = (float) Math.Atan2(-(double)offset.Y, -(double)offset.X);
				Projectile.frameCounter++;
				Projectile.velocity = Vector2.Zero;
				Projectile.position = NPC.position + offset;
				if (Projectile.frameCounter >= 3)
				{
					Projectile.frameCounter = 0;
					Projectile.frame = (Projectile.frame + 1) % 3;
				}
				
				if ((double) offset.X > 0.0)
				{
					Projectile.spriteDirection = -1;
					Projectile.rotation = (float) Math.Atan2((double)offset.Y, (double)offset.X);
				}
				else
				{
					Projectile.spriteDirection = 1;
					Projectile.rotation = (float) Math.Atan2(-(double)offset.Y, -(double)offset.X);
				}
			}
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            returning = true;
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 0);
				Main.dust[dust].scale = 1.5f;
				Main.dust[dust].noGravity = true;
			}
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            Vector2 vector2 = new Vector2(Projectile.Center.X, Projectile.Center.Y);
            float num1 = Main.player[Projectile.owner].MountedCenter.X - vector2.X;
            float num2 = Main.player[Projectile.owner].MountedCenter.Y - vector2.Y;
            float rotation = (float)Math.Atan2((double)num2, (double)num1);
            if (Projectile.alpha == 0)
            {
                int num3 = -1;
                if ((double)Projectile.position.X + (double)(Projectile.width / 2) < (double)Main.player[Projectile.owner].MountedCenter.X)
                    num3 = 1;
                Main.player[Projectile.owner].itemRotation = Main.player[Projectile.owner].direction != 1 ? (float)Math.Atan2((double)num2 * (double)num3, (double)num1 * (double)num3) : (float)Math.Atan2((double)num2 * (double)num3, (double)num1 * (double)num3);
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
                    float num3 = Projectile.type == 154 || Projectile.type == 247 ? 18f / f : 12f / f;
                    float num4 = num1 * num3;
                    float num5 = num2 * num3;
                    vector2.X += num4;
                    vector2.Y += num5;
                    num1 = Main.player[Projectile.owner].MountedCenter.X - vector2.X;
                    num2 = Main.player[Projectile.owner].MountedCenter.Y - vector2.Y;
                    Microsoft.Xna.Framework.Color color = Lighting.GetColor((int)vector2.X / 16, (int)((double)vector2.Y / 16.0));
                    Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Tether").Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 12, 6)), color, rotation, new Vector2((float)12 * 0.5f, (float)6 * 0.5f), 1f, SpriteEffects.None, 0);
                }
            }
            return true;
        }
    }
}
