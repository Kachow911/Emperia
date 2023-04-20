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
    public class AnastasiaP1 : ModProjectile
    {
		int timer = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Twilight Spine");
		}
        public override void SetDefaults()
        {
			Projectile.width = 30;
			Projectile.height = 32;
			Projectile.aiStyle = 4;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.tileCollide = false;
			Projectile.alpha = 255;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Magic;
		}

        public override void AI()
		{
			timer++;
			int num52 = Projectile.type;
			
			if (Projectile.ai[1] >= 6f)
			{
				num52 = ModContent.ProjectileType<AnastasiaP2>();
			}
			if (Projectile.ai[1] != -1 && timer > 2)
			{
				int num53 = Projectile.NewProjectile(Projectile.InheritSource(Projectile), Projectile.position.X + Projectile.velocity.X * .9f + (float)(Projectile.width / 2), Projectile.position.Y + Projectile.velocity.Y * .9f + (float)(Projectile.height / 2), Projectile.velocity.X, Projectile.velocity.Y, num52, Projectile.damage, Projectile.knockBack, Projectile.owner, 0f, 0f);
				Main.projectile[num53].damage = Projectile.damage;
				Main.projectile[num53].ai[1] = Projectile.ai[1] + 1f;
				Projectile.ai[1] = -1;
				NetMessage.SendData(27, -1, -1, null, num53, 0f, 0f, 0f, 0, 0, 0);
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
