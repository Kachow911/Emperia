using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

using Terraria.ModLoader;
using Terraria;
using Terraria.ID;
using Emperia;

namespace Emperia.Projectiles.Twilight
{
    public class LeechEye : ModProjectile
    {
        private const int shootRate = 32;
        //private float projectileNumber { get { return Projectile.ai[1]; } set { Projectile.ai[1] = value; } }
		//private float rotate2 = 0;
		//private Vector2 relativePos;
		//private Vector2 targetPosition;
		//private String mode = "Hovering";
		private int timer = 0;
		private bool init = false;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Leech Eye");
		}
        public override void SetDefaults()
        {
            Projectile.width = 42;
            Projectile.height = 22;
            Projectile.friendly = true;
            //Projectile.hostile = true;
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 600;
            Projectile.light = 0.75f;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 75;
			Projectile.scale = 0.8f;
            Projectile.aiStyle = -1;
        }

        public override void AI()
		{
			timer++;
			Player player = Main.player[Projectile.owner];
			//Projectile.velocity = Vector2.Zero;
			Projectile.velocity.X = 0;
			Projectile.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(timer * 2));
			if (timer % 15 == 0)
			{
				int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, DustID.GoldCoin, 0, 0, 0, Color.White, 1.2f);
				Main.dust[index2].position = Projectile.Center;
				Main.dust[index2].noGravity = true;
			}
			
			Vector2 rotVector = Main.MouseWorld - Projectile.Center;
			if (player.controlUseItem)
			{
				if (timer % 60 == 0)
				{
					rotVector.Normalize();
					Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.Center.X, Projectile.Center.Y, rotVector.X * 14f, rotVector.Y * 14f, ModContent.ProjectileType<LeechEyeP2>(), 10, 1, Main.myPlayer, 0, 0);
				}
			}

	
			

        }
		public override void Kill(int timeLeft)
        {
			/*for (int i = 0; i < 360; i += 10)
			{
				Vector2 vec = Vector2.Transform(new Vector2(-16, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
				vec.Normalize();
				int num622 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 15, 0f, 0f, 91, new Color(255, 255, 255), 1.5f);
                Main.dust[num622].velocity += (vec * 2f);
                Main.dust[num622].noGravity = true;
            }*/
	     }
       /* public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Projectile.Kill();
        }*/
	
    }
}
