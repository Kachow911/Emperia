using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    public class GiantsHeadAnim : ModProjectile
    {
		bool init = false;
		float xOFF = 0;
		float yOFF = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Giants Head");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 46;       //Projectile width
            Projectile.height = 42;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.hostile = false;       // 
            Projectile.tileCollide = false;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = -1;      //how many NPC will penetrate
            Projectile.timeLeft = 31;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Main.projFrames[Projectile.type] = 3;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 3;
			} 
			if (!init)
            {
				xOFF = Projectile.Center.X - Main.player[Projectile.owner].Center.X;
				yOFF = Projectile.Center.Y - Main.player[Projectile.owner].Center.Y;
				init = true;
			}
			Projectile.Center = Main.player[Projectile.owner].Center + new Vector2(xOFF, yOFF);
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
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 258);
				Main.dust[dust1].scale = 0.8f;
				Main.dust[dust1].velocity *= 2f;
			}
			for (int i = 0; i < 10; i++)
			{

				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedByRandom(MathHelper.ToRadians(180));
				Projectile.NewProjectile(Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FireBallHostile>(), Projectile.damage / 3, 1, Main.myPlayer, 0, 0);

			}

		}*/
    }
}