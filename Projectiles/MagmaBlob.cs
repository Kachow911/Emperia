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
			DisplayName.SetDefault("Magma Blob");
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 20;       //projectile width
            projectile.height = 20;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
            projectile.magic = true;         // 
            projectile.tileCollide = true;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 1;      //how many npc will penetrate
            projectile.timeLeft = 200;   //how many time projectile projectile has before disepire
            projectile.light = 0.75f;    // projectile light
            projectile.extraUpdates = 1;
            projectile.ignoreWater = true;
			Main.projFrames[projectile.type] = 6;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {                                                           // |
			projectile.frameCounter++;
			if (projectile.frameCounter >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 6;
			} 
			projectile.velocity.X *= 0.99f;
			projectile.velocity.Y *= 0.99f;
		}
		
		public override void Kill(int timeLeft)
        {
			for (int i = 0; i < Main.npc.Length; i++)
            {
				if (projectile.Distance(Main.npc[i].Center) < 32)
                    Main.npc[i].StrikeNPC(projectile.damage, 0f, 0, false, false, false);
			}
			for (int i = 0; i < 50; ++i) //Create dust after teleport
			{
				int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6);
				int dust1 = Dust.NewDust(projectile.position, projectile.width, projectile.height,6);
				Main.dust[dust1].scale = 0.8f;
				Main.dust[dust1].velocity *= 1.5f;
			}
		
        }
    }
}