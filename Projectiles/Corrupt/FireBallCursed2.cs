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
			projectile.width = 20;       //projectile width
			projectile.height = 20;  //projectile height
			projectile.friendly = true;      //make that the projectile will not damage you
			projectile.ranged = true;         // 
			projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
			projectile.penetrate = 1;      //how many npc will penetrate
			projectile.timeLeft = 240;
			projectile.ignoreWater = true;
			projectile.alpha = 255;
		}
		
	

		public override void AI()
		{
			projectile.velocity *= .97f;
			for (int i = 0; i < 2; i++)
			{
				int num = Dust.NewDust(projectile.Center, 26, 26, 75, 0f, 0f, 0, default(Color), 1.5f);
				Main.dust[num].alpha = 0;
				Main.dust[num].position.X = projectile.Center.X - projectile.velocity.X / 10f * (float)i;
				Main.dust[num].position.Y = projectile.Center.Y - projectile.velocity.Y / 10f * (float)i;
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