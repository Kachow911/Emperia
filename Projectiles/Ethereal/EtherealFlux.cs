using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Ethereal
{
    public class EtherealFlux : ModProjectile
    {
		bool init = false;
		float xOFF = 0;
		float yOFF = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ethereal Flux");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 56;       //projectile width
            projectile.height = 67;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;       // 
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = -1;      //how many npc will penetrate
            projectile.timeLeft = 10;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.ignoreWater = true;
			Main.projFrames[projectile.type] = 6;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
			projectile.frameCounter++;
			if (projectile.frameCounter >= 2)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1);
			} 
			if (!init)
			{
				init = true;
				projectile.rotation = MathHelper.ToRadians(Main.rand.Next(360));
			}
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
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("FireBallHostile"), projectile.damage / 3, 1, Main.myPlayer, 0, 0);

			}

		}*/
    }
}