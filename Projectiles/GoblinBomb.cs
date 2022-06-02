using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Projectiles
{
    public class GoblinBomb : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Rocket");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 32;       //Projectile width
            Projectile.height = 32;  //Projectile height
            Projectile.friendly = false;      //make that the Projectile will not damage you
			Projectile.hostile = true;       // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.ignoreWater = true;
			Main.projFrames[Projectile.type] = 6;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 6)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			} 
		}
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
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
				Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<FireBallHostile>(), Projectile.damage / 3, 1, Main.myPlayer, 0, 0);

			}

		}
    }
}