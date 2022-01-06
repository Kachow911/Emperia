using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Emperia.Buffs;

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
			Projectile.width = 42;
			Projectile.height = 64;
			//drawOffsetX = 40; 
            //drawOriginOffsetY = -10; 
			Projectile.aiStyle = -1;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Melee;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 30;
			Projectile.light = 0.5f;
			Projectile.tileCollide = false;
			Projectile.alpha = 0;
			Main.projFrames[Projectile.type] = 9;
		}
		
		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			p.isMellowProjectile = true;
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 3)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 9;
			} 
			float num1 = Main.MouseWorld.X;
			float num2 = Main.MouseWorld.Y;
			float num3 = (float) Math.Atan((num2 - player.Center.Y) / (num1 - player.Center.X)) / 50;
			if (num1 < player.Center.X)
			{
				Projectile.Center = Vector2.Transform(new Vector2(200, 0), Matrix.CreateRotationZ(MathHelper.ToDegrees(num3 + 180))) + player.Center;
			}
			else
			{
				Projectile.Center = Vector2.Transform(new Vector2(200, 0), Matrix.CreateRotationZ(MathHelper.ToDegrees(num3))) + player.Center;
			}
			
		}
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[Projectile.owner];
			MyPlayer p = player.GetModPlayer<MyPlayer>();
			p.isMellowProjectile = false;
		}
		/*public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			target.AddBuff(ModContent.BuffType<Buffs.MellowDebuff>(), 1200, false);
		}*/
	}
}
