using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Crimson
{

    public class BigBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Bubble");
        }
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 12;       //projectile width
            projectile.height = 12;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 80;
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
            projectile.alpha = 100;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {

            projectile.velocity.X *= .98f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
                target.AddBuff(BuffID.Ichor, 600);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item54, projectile.Center);
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MedBubble"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-6, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                if (i % 8 == 0)
                {
                   int b = Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
                    Main.dust[b].noGravity = true;
                    Main.dust[b].velocity = Vector2.Zero;
                }
            }
        }
    }
    public class MedBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Bubble");
        }
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 12;       //projectile width
            projectile.height = 12;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 80;
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
            projectile.alpha = 100;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {

            projectile.velocity.X *= .99f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
                target.AddBuff(BuffID.Ichor, 300);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item54, projectile.Center);
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(projectile.position.X, projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("SmallBubble"), projectile.damage / 2, projectile.knockBack, projectile.owner, 0, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-4, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                if (i % 8 == 0)
                {
                    int b = Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
                    Main.dust[b].noGravity = true;
                    Main.dust[b].velocity = Vector2.Zero;
                }
            }
        }

    }
    public class SmallBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Bubble");
        }
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 12;       //projectile width
            projectile.height = 12;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 80;
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
            projectile.alpha = 100;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {


        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(5) == 0)
                target.AddBuff(BuffID.Ichor, 300);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item54, projectile.Center);
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-2, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                if (i % 8 == 0)
                {
                    int b = Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
                    Main.dust[b].noGravity = true;
                    Main.dust[b].velocity = Vector2.Zero;
                }
            }
        }

    }
}