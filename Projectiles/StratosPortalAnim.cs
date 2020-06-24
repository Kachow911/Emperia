using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class StratosPortalAnim : ModProjectile
    {
		bool init = false;
		float xOFF = 0;
		float yOFF = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stratos Portal");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 34;       //projectile width
            projectile.height = 34;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;       // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 45;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			Main.projFrames[projectile.type] = 1;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
			projectile.rotation += 1;
			
			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 180);
				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(360));
				Main.dust[dust].velocity = perturbedSpeed;
				Main.dust[dust].noGravity = true;
			}
			projectile.alpha+=3;
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
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 62);
				int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 258);
				Main.dust[dust1].scale = 0.8f;
				Main.dust[dust1].velocity *= 2f;
			}
			for (int i = 0; i < 10; i++)
			{

				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(180));
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireBallHostile"), projectile.damage / 3, 1, Main.myPlayer, 0, 0);

			}

		}*/
    }
}