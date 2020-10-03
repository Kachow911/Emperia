using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia;

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
        {
            projectile.width = 32;
            projectile.height = 54;
            projectile.friendly = true;
			projectile.hostile = false;
            projectile.tileCollide = false;
            projectile.penetrate = 2;
            projectile.timeLeft = 109;
            projectile.light = 0f;
            projectile.ignoreWater = true;
			projectile.alpha = 0;
            projectile.damage = 0;
            projectile.hide = true;
            Main.projFrames[projectile.type] = 4;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
            initCounter++;
            if (initCounter == 1)
            {
                projectile.direction = modPlayer.desertSpikeDirection;
                projectile.spriteDirection = modPlayer.desertSpikeDirection;
            }

            if (initCounter == 9)
            {
                projectile.damage = 18;
           } 

            if (projectile.penetrate == 1)
            {
                if (npc.GetGlobalNPC<MyNPC>().desertSpikeTime == 0 && initCounter > 10 && projectile.timeLeft > 5)
                {
                    projectile.timeLeft = 5;
                }
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
            if (npc.knockBackResist > 0f)
            {
                npc.GetGlobalNPC<MyNPC>().desertSpikeTime = 100;
                npc.GetGlobalNPC<MyNPC>().desertSpikeHeight = projectile.Top.Y + 28;
            }
		}
        public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI) {
			drawCacheProjsBehindNPCs.Add(index);
		}
        public override void Kill(int timeLeft)
        {
            for (int i = 0; i < 5; i++)
            {
                int dust = Dust.NewDust(projectile.position, projectile.width / 2, projectile.height, mod.DustType("CarapaceDust"));
                Vector2 vel = new Vector2(projectile.velocity.X * -2, -1);
            }
        }
    }
}