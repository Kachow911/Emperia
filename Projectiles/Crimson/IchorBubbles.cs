using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;
using Emperia.Projectiles.Crimson;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Projectiles.Crimson
{

    public class BigBubble : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ichor Bubble");
        }
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 12;       //Projectile width
            Projectile.height = 12;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 80;
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {

            Projectile.velocity.X *= .98f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(2) == 0)
                target.AddBuff(BuffID.Ichor, 600);
        }
        public override void Kill(int timeLeft)
        {
            PlaySound(SoundID.Item54, Projectile.Center);
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<MedBubble>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-6, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                if (i % 8 == 0)
                {
                   int b = Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
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
        {  //Projectile name
            Projectile.width = 12;       //Projectile width
            Projectile.height = 12;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 80;
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {

            Projectile.velocity.X *= .99f;

        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(3) == 0)
                target.AddBuff(BuffID.Ichor, 300);
        }
        public override void Kill(int timeLeft)
        {
            PlaySound(SoundID.Item54, Projectile.Center);
            for (int i = 0; i < 2; i++)
            {
                Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(360));
                Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X, Projectile.position.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SmallBubble>(), Projectile.damage / 2, Projectile.knockBack, Projectile.owner, 0, 0);
            }
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-4, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                if (i % 8 == 0)
                {
                    int b = Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
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
        {  //Projectile name
            Projectile.width = 12;       //Projectile width
            Projectile.height = 12;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 80;
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
            Projectile.alpha = 100;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {


        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            if (Main.rand.Next(5) == 0)
                target.AddBuff(BuffID.Ichor, 300);
        }
        public override void Kill(int timeLeft)
        {
            PlaySound(SoundID.Item54, Projectile.Center);
            for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-2, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
                if (i % 8 == 0)
                {
                    int b = Dust.NewDust(Projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 87);
                    Main.dust[b].noGravity = true;
                    Main.dust[b].velocity = Vector2.Zero;
                }
            }
        }

    }
}