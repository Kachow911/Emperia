using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Summon
{
    public class EmberTyrant : ModProjectile
    {
        int timer = 0;
        int move = 0;
        bool slam = true;
        NPC targetNPC;
        int timeFromLastD = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ember Tyrant");
            Main.projFrames[base.Projectile.type] = 1;
            ProjectileID.Sets.MinionSacrificable[base.Projectile.type] = true;
            ProjectileID.Sets.CultistIsResistantTo[base.Projectile.type] = true;
            ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
        }
        int counter = 0;
        public override void SetDefaults()
        {
            Projectile.CloneDefaults(ProjectileID.Spazmamini);
            Projectile.width = 46;
            Projectile.height = 42;
            Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = true;
            Projectile.netImportant = true;
            AIType = -1;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.minionSlots = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {

            return false;
        }

        public override void AI()
        {
            Player player = Main.player[Projectile.owner];
            timer++;
            if (timeFromLastD < 60 && timeFromLastD > 0)
            {
                move = 0;
            }
            for (int i = 0; i < 200; i++)
            {
                if (player.Distance(Main.npc[i].Center) < 400f && move != 2 && !Main.npc[i].townNPC && Main.npc[i].life >= 1)
                {
                    if (timeFromLastD > 60)
                    {
                        move = 1;
                        targetNPC = Main.npc[i];
                    }
                }
            }
            if (move == 0)
            {
                Vector2 targetPos = player.Center + new Vector2(0, -100);
                if (player.velocity.X == 0)
                {
                    targetPos += new Vector2(16f * (float)Math.Cos(MathHelper.ToRadians(timer * 3)), 0);
                }
                SmoothMoveToPosition(targetPos, .1f, 6, 32);
            }
            if (move == 1)
            {
                if (Math.Abs(Projectile.Center.X - targetNPC.Center.X) > 20f && Projectile.Center.Y < targetNPC.Center.Y)
                {
                    SmoothMoveToPosition(targetNPC.Center + new Vector2(0, -100), .1f, 6, 32);
                }
                else
                {
                    if (timeFromLastD > 60)
                    {
                        Projectile.velocity = Vector2.Zero;
                        move = 2;
                    }
                }
            }
            if (move == 2)
            {
                Projectile.velocity.Y = 12f;
                counter++;
                targetNPC = null;
                timeFromLastD = 0;
            }
            else if (move != 1)
            {
                timeFromLastD++;
            }
            if (counter >= 120)
            {
                move = 0;
                Projectile.Center = player.Center + new Vector2(0, -100);
                for (int i = 0; i < 50; ++i) //Create dust b4 teleport
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
                    int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
                    Main.dust[dust1].scale = 1.5f;
                    Main.dust[dust1].velocity *= 1.5f;
                    int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
                    Main.dust[dust2].scale = 1.5f;
                }
                counter = 0;
            }
            bool flag64 = Projectile.type == ModContent.ProjectileType<EmberTyrant>();

            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            if (flag64)
            {
                if (player.dead)
                    modPlayer.EmberTyrant = false;

                if (modPlayer.EmberTyrant)
                    Projectile.timeLeft = 2;

            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (move != 0)
            {
                for (int i = 0; i < 50; ++i) //Create dust b4 teleport
                {
                    int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
                    int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
                    Main.dust[dust1].scale = 1.5f;
                    Main.dust[dust1].velocity *= 1.5f;
                    int dust2 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
                    Main.dust[dust2].scale = 1.5f;
                }
                Projectile.velocity.Y *= -1;
                move = 0;
            }
        }
        private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - Projectile.Center).Length()) >= slowRange)
            {
                Projectile.velocity += Vector2.Normalize((toPosition - Projectile.Center) * addSpeed);
                Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -maxSpeed, maxSpeed);
                Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                Projectile.velocity *= slowBy;
            }
        }
        
    }
}
