using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Mushroom
{
    public class ShroomFlask : ModProjectile
    {
    	public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shroomy Flask");
		}
        public override void SetDefaults()
        {
            projectile.width = 25;
            projectile.height = 25;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 180;
            aiType = 48;
        }
        
        public override void AI()
        {
        	if (Main.rand.Next(5) == 0)
            {
            	Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 41, projectile.velocity.X * 0.15f, projectile.velocity.Y * 0.15f);
            }
        }
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Player player = Main.player[projectile.owner];
			player.statMana+=5;
			player.ManaEffect(5);
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("FlaskEnemyEffect"), 0, projectile.knockBack, projectile.owner, 0f, 0f);
			projectile.Kill();
		}
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0, 0, mod.ProjectileType("FlaskTileEffect"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 0f);
			projectile.Kill();
			return false;
		}
        public override void Kill(int timeLeft)
        {
        	Main.PlaySound(SoundID.Item, projectile.Center, 107);  
        }
        public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough)
        {
            fallThrough = false;
            return true;
        }
 
    }
}
