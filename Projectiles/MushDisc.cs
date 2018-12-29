using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class MushDisc : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mush Disc");
		}
        public override void SetDefaults()
        { //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 28;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.thrown = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 60;   //how many time this projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //this make that the projectile will face the corect way
        {                                                           // |
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			if (Main.rand.Next(2) == 0)
				Dust.NewDust(projectile.position + projectile.velocity, projectile.width / 2, projectile.height / 2, 20, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			if (projectile.timeLeft > 15) projectile.timeLeft = 15;
		}
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < 360; i++)
            {
                Vector2 vec = Vector2.Transform(new Vector2(-20, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                if (i % 8 == 0)
                {   //odd
                    Dust.NewDust(projectile.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20);
                }

                if (i % 9 == 0)
                {   //even
                    vec.Normalize();
                    Dust.NewDust(projectile.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, vec.X * 2, vec.Y * 2);
                }
            }
			for (int i = 0; i < 6; i++)
			{
				if (i % 2 == 0)
				{
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MushroomShard1"), projectile.damage / 2, 1, Main.myPlayer, 0, 0);
				}
				else
				{
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
					Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MushroomShard2"), projectile.damage / 2, 1, Main.myPlayer, 0, 0);
				}
			}
		}
        
    }
}