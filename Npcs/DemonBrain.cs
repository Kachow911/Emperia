using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs
{
    public class DemonBrain : ModNPC
    {
		public bool hit = false;
        int timer = 0;
        int moveSpeed = 0;
        int moveSpeedY = 0;
        float HomeY = 35f;
        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Demon Brain");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 150;
            npc.damage = 20;
            npc.defense = 5;
            npc.knockBackResist = 3f;
            npc.width = 40;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //true
            npc.DeathSound = SoundID.NPCDeath1;
            //npc.buffImmune[24] = true;
            npc.netAlways = true;
        }
		 public override void AI()
        {
            timer++;
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
            if (!hit)
            {
                npc.velocity.X = 0;
                npc.velocity.Y = 0.8f * (float)Math.Cos(MathHelper.ToRadians(timer * 2));
            }
            else
            {
                if (npc.Center.X >= player.Center.X && moveSpeed >= -20) 
                {
                    moveSpeed--;
                }

                if (npc.Center.X <= player.Center.X && moveSpeed <= 20)
                {
                    moveSpeed++;
                }
                if (npc.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -20)
                {
                    moveSpeedY--;
                    HomeY = 35f;
                }

                if (npc.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 20)
                {
                    moveSpeedY++;
                }
                npc.velocity.X = moveSpeed * 0.1f;
                npc.velocity.Y = moveSpeedY * 0.1f;
                if (timer % 180 == 0)
                {
                    Vector2 placePosition1 = npc.Center;
                    Vector2 direction1 = player.Center - placePosition1;
                    direction1.Normalize();
                    Projectile.NewProjectile(placePosition1.X, placePosition1.Y, direction1.X * 10f, direction1.Y * 10f, mod.ProjectileType("DemonBrainProj"), 22, 1, Main.myPlayer, 0, 0);
                }
            }

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            npc.velocity.X = 0;
            npc.velocity.Y = 0;
            hit = true;
        }
    }
    
}
