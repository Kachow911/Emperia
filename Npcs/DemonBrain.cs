using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

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
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 150;
            NPC.damage = 20;
            NPC.defense = 5;
            NPC.knockBackResist = 3f;
            NPC.width = 40;
            NPC.height = 40;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //true
            NPC.DeathSound = SoundID.NPCDeath1;
            //NPC.buffImmune[24] = true;
            NPC.netAlways = true;
        }
		 public override void AI()
        {
            timer++;
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
            if (!hit)
            {
                NPC.velocity.X = 0;
                NPC.velocity.Y = 0.8f * (float)Math.Cos(MathHelper.ToRadians(timer * 2));
            }
            else
            {
                if (NPC.Center.X >= player.Center.X && moveSpeed >= -20) 
                {
                    moveSpeed--;
                }

                if (NPC.Center.X <= player.Center.X && moveSpeed <= 20)
                {
                    moveSpeed++;
                }
                if (NPC.Center.Y >= player.Center.Y - HomeY && moveSpeedY >= -20)
                {
                    moveSpeedY--;
                    HomeY = 35f;
                }

                if (NPC.Center.Y <= player.Center.Y - HomeY && moveSpeedY <= 20)
                {
                    moveSpeedY++;
                }
                NPC.velocity.X = moveSpeed * 0.1f;
                NPC.velocity.Y = moveSpeedY * 0.1f;
                if (timer % 180 == 0)
                {
                    Vector2 placePosition1 = NPC.Center;
                    Vector2 direction1 = player.Center - placePosition1;
                    direction1.Normalize();
                    Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), placePosition1.X, placePosition1.Y, direction1.X * 10f, direction1.Y * 10f, ModContent.ProjectileType<DemonBrainProj>(), 22, 1, Main.myPlayer, 0, 0);
                }
            }

        }
        public override void HitEffect(int hitDirection, double damage)
        {
            NPC.velocity.X = 0;
            NPC.velocity.Y = 0;
            hit = true;
        }
    }
    
}
