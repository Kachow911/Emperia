using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class MagmaBlob : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Magma Blob");
		}
        public override void SetDefaults()
        {  //Projectile name
            Projectile.width = 20;       //Projectile width
            Projectile.height = 20;  //Projectile height
            Projectile.friendly = true;      //make that the Projectile will not damage you
            Projectile.DamageType = DamageClass.Magic;         // 
            Projectile.tileCollide = true;   //make that the Projectile will be destroed if it hits the terrain
            Projectile.penetrate = 1;      //how many NPC will penetrate
            Projectile.timeLeft = 200;   //how many time Projectile Projectile has before disepire
            Projectile.light = 0.75f;    // Projectile light
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			Main.projFrames[Projectile.type] = 6;
        }
        public override void AI()           //Projectile make that the Projectile will face the corect way
        {                                                           // |
			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 4)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 6;
			} 
			Projectile.velocity.X *= 0.99f;
			Projectile.velocity.Y *= 0.99f;
		}
		
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (Projectile.Distance(Main.npc[i].Center) < 32)
                    Main.npc[i].SimpleStrikeNPC(Projectile.damage, 0);
			}
			for (int i = 0; i < 50; ++i) //Create dust after teleport
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch);
				int dust1 = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height,DustID.Torch);
				Main.dust[dust1].scale = 0.8f;
				Main.dust[dust1].velocity *= 1.5f;
			}
		
        }
    }
}