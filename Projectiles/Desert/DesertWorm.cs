using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{
    public class DesertWorm : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Desert Worm");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 24;       //projectile width
            projectile.height = 24;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;       // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 300;   //how many time projectile projectile has before disepire
            //projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			Main.projFrames[projectile.type] = 5;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
			projectile.frameCounter++;
			if (projectile.frameCounter >= 6)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 5;
			}
			projectile.rotation = projectile.velocity.ToRotation();

			/*int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 258, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			int dust2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 258, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
			Main.dust[dust].noGravity = true;
			Main.dust[dust2].noGravity = true;
			Main.dust[dust2].velocity *= 0f;
			Main.dust[dust2].velocity *= 0f;
			Main.dust[dust2].scale = 1.9f;
			Main.dust[dust].scale = 1.9f;*/
		}
		/*public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item, projectile.Center, 14);
			for (int i = 0; i < Main.player.Length; i++)
			{
				if (projectile.Distance(Main.player[i].Center) < 32)
					Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(Main.player[i].whoAmI, projectile.whoAmI), projectile.damage, 0);
			}
			for (int i = 0; i < 50; ++i) //Create dust after teleport
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
				int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
				Main.dust[dust1].scale = 0.8f;
				Main.dust[dust1].velocity *= 2f;
			}
			for (int i = 0; i < 10; i++)
			{

				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(180));
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireBall"), projectile.damage / 2, 1, Main.myPlayer, 0, 0);

			}

		}*/
    }
}