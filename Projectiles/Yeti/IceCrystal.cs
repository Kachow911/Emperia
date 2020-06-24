using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Yeti
{

    public class IceCrystal : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ice Crystal");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 32;       //projectile width
            projectile.height = 32;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
			if (Main.rand.NextBool(2))
			{
				int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, default(Color), 0.9f);
				Main.dust[index2].noGravity = true;
			}
			projectile.velocity.X *= 0.99f;
			projectile.velocity.Y *= 0.99f;
			projectile.rotation += .05f;
		}
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
			Color rgb = new Color(135,206,250);
			int index2 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 68, (float) projectile.velocity.X, (float) projectile.velocity.Y, 0, rgb, 0.9f);
			Main.dust[index2].noGravity = true;
		}
		public override void Kill(int timeLeft)
        {
			Main.PlaySound(SoundID.Item, projectile.Center, 27);  
			for (int i = 0; i < 6; i++)
			{
				Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 60 * i));
				int p = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("IceShard"), projectile.damage / 6, 0, Main.myPlayer, 0, 0);
				Main.projectile[p].rotation = MathHelper.ToRadians(Main.rand.Next(360));
			}
		}
        
    }
}