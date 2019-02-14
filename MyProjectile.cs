using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;
namespace Emperia
{
    public class MyProjectile: GlobalProjectile
    {
		public override bool InstancePerEntity
		{
			get { return true; }
		}
		public bool scoriaExplosion = false;
		public override void Kill(Projectile projectile, int timeLeft)
		{
			if (scoriaExplosion)
			{
             
                for (int num621 = 0; num621 < 20; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num622].velocity *= 3f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[num622].scale = 0.5f;
                        Main.dust[num622].fadeIn = 1f + (float)Main.rand.Next(10) * 0.1f;
                    }
                }
                for (int num623 = 0; num623 < 35; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 5f;
                    num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 258, 0f, 0f, 100, default(Color));
                    Main.dust[num624].velocity *= 2f;
                }
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                for (int i = 0; i < Main.npc.Length; i++)
                {
                    if (projectile.Distance(Main.npc[i].Center) < 64 && !Main.npc[i].townNPC)
                        Main.npc[i].StrikeNPC(projectile.damage / 2, 0f, 0, false, false, false);
                }
      
			}
		}
	}
}