using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles.Desert
{
	
    public class DesertSpikeBig : ModProjectile
    {
        NPC npc;

		private int initCounter = 0;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Sandstone Spike");
            ProjectileID.Sets.DontAttachHideToAlpha[projectile.type] = true;
		}
        public override void SetDefaults()
        {  //projectile name
            projectile.width = 32;       //projectile width
            projectile.height = 54;  //projectile height
            projectile.friendly = true;      //make that the projectile will not damage you
			projectile.hostile = false;
            projectile.tileCollide = false;   //make that the projectile will be destroed if it hits the terrain
            projectile.penetrate = 2;      //how many projectile will penetrate
            projectile.timeLeft = 149;   //how many time projectile projectile has before disepire
            projectile.light = 0f;    // projectile light
            projectile.ignoreWater = true;
			projectile.alpha = 0;
            projectile.damage = 0;
            projectile.hide = true;
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()           //projectile make that the projectile will face the corect way
        {
            initCounter++;
            if (initCounter == 1)
            {
                Player player = Main.player[projectile.owner];
                MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
                projectile.direction = modPlayer.desertSpikeDirection;
                projectile.spriteDirection = modPlayer.desertSpikeDirection;
            }

            if (initCounter == 9) //make this faster
            {
                projectile.damage = 2;
            }

            //animation stuff
            projectile.frameCounter++;

			if (projectile.frameCounter >= 4 && projectile.frame < 3 && projectile.timeLeft >= 4)
			{
				projectile.frameCounter = 0;
				projectile.frame = (projectile.frame + 1) % 4;
			}

            if (projectile.timeLeft == 4)
            {
                projectile.frame = 1;
            }
        }
        public override bool? CanHitNPC(NPC target)
		{
            if (projectile.penetrate == 1 | initCounter > 10)
            {
                return false;
            }
            else return true;
		}
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            npc = target;
            npc.GetGlobalNPC<MyNPC>().impaledDirection = npc.direction;
		}
        public override void ModifyHitNPC(NPC target, ref int damage, ref float knockback, ref bool crit, ref int hitDirection) {
            npc = target;
            npc.GetGlobalNPC<MyNPC>().desertSpikeTime = 120;
            npc.GetGlobalNPC<MyNPC>().desertSpikeHeight = projectile.Top.Y + 28;
		}
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI) {
			drawCacheProjsBehindNPCs.Add(index);
		}
    }
}