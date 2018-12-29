using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;

namespace Emperia.Projectiles
{
	public class MellowSlice : ModProjectile
	{
		int count = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Mellow Slice");
		}
		public override void SetDefaults()
		{
			projectile.width = 42;
			projectile.height = 64;
			//drawOffsetX = 40; 
            //drawOriginOffsetY = -10; 
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.melee = true;
			projectile.penetrate = -1;
			projectile.timeLeft = 30;
			projectile.light = 0.5f;
			projectile.tileCollide = false;
			projectile.alpha = 0;
			Main.projFrames[projectile.type] = 9;
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			MyPlayer p = player.GetModPlayer<MyPlayer>(mod);
			p.isMellowProjectile = true;
			projectile.frameCounter++;
			if (projectile.frameCounter >= 3)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 5;
			} 
			float num1 = Main.MouseWorld.X;
			float num2 = Main.MouseWorld.Y;
			float num3 = (float) Math.Atan((num2 - player.Center.Y) / (num1 - player.Center.X)) / 50;
			if (num1 < player.Center.X)
			{
				projectile.Center = Vector2.Transform(new Vector2(200, 0), Matrix.CreateRotationZ(MathHelper.ToDegrees(num3 + 180))) + player.Center;
			}
			else
			{
				projectile.Center = Vector2.Transform(new Vector2(200, 0), Matrix.CreateRotationZ(MathHelper.ToDegrees(num3))) + player.Center;
			}
			
		}
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			MyPlayer p = player.GetModPlayer<MyPlayer>(mod);
			p.isMellowProjectile = false;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(mod.BuffType("MellowDebuff"), 1200, false);
		}
	}
}