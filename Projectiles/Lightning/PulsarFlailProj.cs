using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Buffs;

namespace Emperia.Projectiles.Lightning
{
    public class PulsarFlailProj : ModProjectile
    {
		int returnTimer = 25;
        public override void SetDefaults()
        {
            Projectile.width = 36;
            Projectile.height = 36;
            Projectile.alpha = 0;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Melee;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 1000;
			Main.projFrames[Projectile.type] = 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[Projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (modPlayer.lightningSet)
                modPlayer.lightningDamage += damage;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Pulsar Flail");
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
            if (returnTimer == 0)
            {
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (Projectile.Distance(Main.npc[i].Center) < 160 && !Main.npc[i].townNPC)
                    {
                        Main.npc[i].StrikeNPC(Projectile.damage * 2, 0f, 0, false, false, false);
                        Main.npc[i].AddBuff(ModContent.BuffType<ElecHostile>(), 120);
                    }
                }
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-15, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                    if (i % 8 == 0)
                    {
                        int b = Dust.NewDust(Projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 226);
                        Main.dust[b].noGravity = true;
                        Main.dust[b].velocity = vec;
                    }
                }
                Terraria.Audio.SoundEngine.PlaySound(SoundID.Item62, Projectile.Center);
                //Projectile.timeLeft = 0;
            }
            returnTimer--;
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            returnTimer = -1;
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Dig, Projectile.position);
            return false;
        }

        public override bool PreDraw(ref Color lightColor)
        {
            bool doneOnce = false;
            Vector2 vector2 = new Vector2(Projectile.Center.X, Projectile.Center.Y);
            float num1 = Main.player[Projectile.owner].Center.X - vector2.X;
            float num2 = Main.player[Projectile.owner].Center.Y - vector2.Y;
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
                    Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Lightning/Chain").Value, new Vector2(vector2.X - Main.screenPosition.X, vector2.Y - Main.screenPosition.Y), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, 10, 12)), Color.White, rotation, new Vector2((float)18 * 0.5f, (float)14 * 0.5f), 1f, SpriteEffects.None, 0);

                }
            }
            return true;
        }
    }
}