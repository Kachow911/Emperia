using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinRamCarrier : ModNPC
    {
    
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Carrier");
			Main.npcFrameCount[npc.type] = 8;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 75;
            npc.damage = 15;
            npc.defense = 3;
            npc.knockBackResist = 0.6f;
            npc.width = 96;
            npc.height = 56;
            npc.value = Item.buyPrice(0, 0, 20, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = false;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
			npc.scale = 1f;
			npc.aiStyle = 3;
			aiType = 508;
        }
		public override void FindFrame(int frameHeight)
		{	
			npc.frameCounter += 0.2f;
			npc.frameCounter %= 8; 
			int frame = (int)npc.frameCounter; 
			npc.frame.Y = frame * frameHeight; 
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 100;
            npc.damage = 20;
        }

        public override void AI()
		{
			Player player = Main.player[npc.target];
			if (Math.Abs(npc.velocity.X) < 3f)
			{
				if (npc.Center.X > player.Center.X)
					npc.velocity.X -= .05f;
				else if (npc.Center.X < player.Center.X)
					npc.velocity.X += .05f;
			}
			if (npc.velocity.X > 0)
				npc.spriteDirection = 1;
			else if (npc.velocity.X < 0)
				npc.spriteDirection = -1; 
		}
		

       

       /* private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - npc.Center).Length()) >= slowRange)
            {
                npc.velocity += Vector2.Normalize((toPosition - npc.Center) * addSpeed);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -maxSpeed, maxSpeed);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                npc.velocity *= slowBy;
            }
        }*/
		public override void NPCLoot()
		{
			
		}
        
    }
}
