using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace Emperia.Projectiles.Corrupt
{
	public class FireBallCursed2 : ModProjectile
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Cursed Bolt");
		}
		public override void SetDefaults()
		{
			Projectile.width = 20;       //Projectile width
			Projectile.height = 20;  //Projectile height
			Projectile.friendly = true;      //make that the Projectile will not damage you
			Projectile.DamageType = DamageClass.Ranged;         // 
			Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
			Projectile.penetrate = 1;      //how many NPC will penetrate
			Projectile.timeLeft = 240;
			Projectile.ignoreWater = true;
			Projectile.alpha = 255;
		}
		
	

		public override void AI()
		{
			Projectile.velocity *= .97f;
			for (int i = 0; i < 2; i++)
			{
				int num = Dust.NewDust(Projectile.Center, 26, 26, 75, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[num].alpha = 0;
				Main.dust[num].position.X = Projectile.Center.X - Projectile.velocity.X / 10f * (float)i;
				Main.dust[num].position.Y = Projectile.Center.Y - Projectile.velocity.Y / 10f * (float)i;
				Main.dust[num].noGravity = true;
			}
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (Main.rand.Next(3) == 0)
				target.AddBuff(BuffID.CursedInferno, 240);
		}


	}
}