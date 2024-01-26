using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class PrimalBomb : ModProjectile
    {
		private int explodeRadius = 100;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Primal Bomb");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 32;       //Projectile width
            Projectile.height = 32;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 100;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Projectile.alpha = 255;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {       
			if (Projectile.timeLeft > 10)
			{
			for (int i = -5; i < 6; i+= 5)
			{
				for (int j = -5; j < 6; j+= 5)
				{
					int dust = Dust.NewDust(new Vector2(Projectile.Center.X + i, Projectile.Center.Y + j), Projectile.width / 4, Projectile.height / 4, DustID.GemEmerald, 0f, 0f, 0, new Color(89, 249, 116), 1.5f);
					Main.dust[dust].velocity = Vector2.Zero;
					Main.dust[dust].scale = 1.5f;
					Main.dust[dust].noGravity = true;
				
					
				} 
			}
			}
		}
		public override void OnKill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
                if (Projectile.Distance(Main.npc[i].Center) < explodeRadius && !Main.npc[i].townNPC)
					
                     Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
            }
			for (int i = 0; i < 360; i += 10)
				{
					Vector2 vec = Vector2.Transform(new Vector2(-10, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));
					vec.Normalize();
					int num622 = Dust.NewDust(new Vector2(Projectile.Center.X, (float) ((double) Projectile.Center.Y + (double) Projectile.height - 16.0)), Projectile.width, 16, DustID.GemEmerald, 0.0f, 0.0f, 0, new Color(), 1f);
					Main.dust[num622].velocity += (vec *0.2f);
					Main.dust[num622].noGravity = true;
				}
		}
        
    }
}