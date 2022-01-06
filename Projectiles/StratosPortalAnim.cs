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
        {  //Projectile name
            Projectile.width = 34;       //Projectile width
            Projectile.height = 34;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.hostile = false;       // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 45;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Main.projFrames[Projectile.type] = 1;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
			Projectile.rotation += 1;
			
			if (Main.rand.NextBool(2))
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 180);
				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(360));
				Main.dust[dust].velocity = perturbedSpeed;
				Main.dust[dust].noGravity = true;
			}
			Projectile.alpha+=3;
		}
		/*public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item, Projectile.Center, 14);
			for (int i = 0; i < Main.player.Length; i++)
			{
				if (Projectile.Distance(Main.player[i].Center) < 32)
					Main.player[i].Hurt(Terraria.DataStructures.PlayerDeathReason.ByProjectile(Main.player[i].whoAmI, Projectile.whoAmI), Projectile.damage, 0);
			}
			for (int i = 0; i < 50; ++i) //Create dust after teleport
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 62);
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
				Main.dust[dust1].scale = 0.8f;
				Main.dust[dust1].velocity *= 2f;
			}
			for (int i = 0; i < 10; i++)
			{

				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(180));
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FireBallHostile>(), Projectile.damage / 3, 1, Main.myPlayer, 0, 0);

			}

		}*/
    }
}