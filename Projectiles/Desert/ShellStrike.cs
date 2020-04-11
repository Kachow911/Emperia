using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{

    public class ShellStrike : ModProjectile
    {
        NPC npc;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Shell Strike");
		}
        public override void SetDefaults()
        {
            projectile.width = 6;
            projectile.height = 6;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 140;
            projectile.ignoreWater = false;
            projectile.spriteDirection = projectile.direction;
        }
        public override void AI()
        {
            projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
		}
		public override void Kill(int timeLeft)
		{
			for (int i = 0; i < 3; i++)
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 9);
                Main.dust[dust].noGravity = true;
				Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
            npc = target;
            npc.GetGlobalNPC<MyNPC>().strikeCount += 1;
            if (target.GetGlobalNPC<MyNPC>().strikeCount == 4)
            {
				damage = damage * 2;
                Main.PlaySound(SoundID.Item14, projectile.Center);
				for (int i = 0; i < Main.npc.Length; i++)
            	{
					if (projectile.Distance(Main.npc[i].Center) < 40 && Main.npc[i] != npc)
                    	Main.npc[i].StrikeNPC(projectile.damage * 2, 0f, 0, false, false, false);
				}	
				for (int i = 0; i < 20; ++i)
				{
				    int dust = Dust.NewDust(new Vector2(projectile.position.X - 14, projectile.position.Y - 14), projectile.width * 8, projectile.height * 8, 262);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1f;
				}
			}
		}
    	public override bool OnTileCollide(Vector2 oldVelocity)
		{
            Main.PlaySound(SoundID.Dig, projectile.position);
            return true;
		}
        
        
    }
}
