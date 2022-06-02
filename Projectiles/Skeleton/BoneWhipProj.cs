using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Skeleton
{
    public class BoneWhipProj : ModProjectile
    {
		int returnTimer = 30;
        public override void SetDefaults()
        {
            Projectile.width = 22;
            Projectile.height = 22;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
			Main.projFrames[Projectile.type] = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("BoneWhip");
        }
        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            Vector2 playerCenter = player.MountedCenter;
            if ((double) Projectile.velocity.X < 0.0)
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float) Math.Atan2(-(double) Projectile.velocity.Y, -(double) Projectile.velocity.X) - 1.57f;
            }
            else
            {
                Projectile.spriteDirection = 1;
                Projectile.rotation = (float) Math.Atan2((double) Projectile.velocity.Y, (double) Projectile.velocity.X) + 1.57f;
            }
			if (returnTimer <= 0)
			{
				Projectile.rotation += 3.14f;
			}
			returnTimer--;
			if (returnTimer <= 0)
			{
				Projectile.tileCollide = false;
				Vector2 returnVelocity = playerCenter - Projectile.position;
				returnVelocity.Normalize();
				returnVelocity *= 20f;
				Projectile.velocity = returnVelocity;
				
				if (Vector2.Distance(playerCenter, Projectile.position) <= 10f || Vector2.Distance(playerCenter, Projectile.position) >= 5000f)
				{
					Projectile.Kill();
				}
			}
	
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            returnTimer = 0;
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            bool doneOnce = false;
            Vector2 vector2 = new Vector2(Projectile.Center.X, Projectile.Center.Y);
            float num1 = Main.player[Projectile.owner].MountedCenter.X - vector2.X;
            float num2 = Main.player[Projectile.owner].MountedCenter.Y - vector2.Y;
            float rotation = (float)Math.Atan2((double)num2, (double)num1) + 1.57f;
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
                    if ((double)f < 40.0)
                    {
                        Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Skeleton/Handle").Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 18, 14)), Color.White, rotation, new Vector2((float)18 * 0.5f, (float)14 * 0.5f), 1f, SpriteEffects.None, 0);
                        //doneOnce = true;
                    }
                    else
                        Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Skeleton/Chain").Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 18, 14)), Color.White, rotation, new Vector2((float)18 * 0.5f, (float)14 * 0.5f), 1f, SpriteEffects.None, 0);
                }
            }
            return true;
        }
    }
}