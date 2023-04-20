using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Yeti
{
    public class Yetiling : ModNPC
    {
        bool onGround = false;
        int aboveTimer = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Yetiling");
			Main.npcFrameCount[NPC.type] = 6;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 120;
            NPC.damage = 20;
            NPC.defense = 10;
            NPC.knockBackResist = 0.4f;
            NPC.width = 48;
            NPC.height = 48;
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
			NPC.frameCounter %= 6; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight; 
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 180;
            NPC.damage = 30;
        }

        public override void AI()
		{
            Player player = Main.player[NPC.target];
            if (player.Center.Y < NPC.Center.Y && NPC.velocity.Y >= 0)
            {
                aboveTimer++;
            }
            else aboveTimer = 0;
            if (aboveTimer > 90)
            {
                NPC.velocity.Y = -8;
                aboveTimer = 0;
            }
            if (onGround && NPC.velocity.Y <= 0)
            {
                NPC.velocity.Y *= 1.4f;
            }
			if (Math.Abs(NPC.velocity.X) < 8f)
			{
				if (NPC.Center.X > player.Center.X)
					NPC.velocity.X -= .05f;
				else if (NPC.Center.X < player.Center.X)
					NPC.velocity.X += .05f;
			}
			if (NPC.velocity.X > 0)
				NPC.spriteDirection = -1;
			else if (NPC.velocity.X < 0)
				NPC.spriteDirection = 1;
            onGround = (NPC.velocity.Y == 0);
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
			Color rgb = new Color(160, 243, 255);
			for (int i = 0; i < 360; i+=6)
            {
				int index2 = Dust.NewDust(NPC.position + new Vector2(5, 0).RotatedBy(MathHelper.ToRadians(i)), NPC.width, NPC.height, DustID.Snow, NPC.velocity.X / 5, (float) NPC.velocity.Y, 0, rgb, 0.9f);
			}
			
			
		}
        
    }
}
