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
			// DisplayName.SetDefault("Goblin Carrier");
			Main.npcFrameCount[NPC.type] = 8;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 75;
            NPC.damage = 15;
            NPC.defense = 3;
            NPC.knockBackResist = 0.6f;
            NPC.width = 96;
            NPC.height = 56;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
			NPC.scale = 1f;
			NPC.aiStyle = 3;
			AIType = 508;
        }
		public override void FindFrame(int frameHeight)
		{	
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 8; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight; 
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 100;
            NPC.damage = 20;
        }

        public override void AI()
		{
			Player player = Main.player[NPC.target];
			if (Math.Abs(NPC.velocity.X) < 3f)
			{
				if (NPC.Center.X > player.Center.X)
					NPC.velocity.X -= .05f;
				else if (NPC.Center.X < player.Center.X)
					NPC.velocity.X += .05f;
			}
			if (NPC.velocity.X > 0)
				NPC.spriteDirection = 1;
			else if (NPC.velocity.X < 0)
				NPC.spriteDirection = -1; 
		}
		

       

       /* private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - NPC.Center).Length()) >= slowRange)
            {
                NPC.velocity += Vector2.Normalize((toPosition - NPC.Center) * addSpeed);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -maxSpeed, maxSpeed);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                NPC.velocity *= slowBy;
            }
        }*/
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			
		}
        
    }
}
