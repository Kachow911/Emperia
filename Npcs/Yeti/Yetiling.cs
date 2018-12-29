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
    
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Yetiling");
			Main.npcFrameCount[npc.type] = 6;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 120;
            npc.damage = 20;
            npc.defense = 10;
            npc.knockBackResist = 0.2f;
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
			npc.frameCounter %= 6; 
			int frame = (int)npc.frameCounter; 
			npc.frame.Y = frame * frameHeight; 
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 180;
            npc.damage = 30;
        }

        public override void AI()
		{
			Player player = Main.player[npc.target];
			if (Math.Abs(npc.velocity.X) < 6f)
			{
				if (npc.Center.X > player.Center.X)
					npc.velocity.X -= .05f;
				else if (npc.Center.X < player.Center.X)
					npc.velocity.X += .05f;
			}
			if (npc.velocity.X > 0)
				npc.spriteDirection = -1;
			else if (npc.velocity.X < 0)
				npc.spriteDirection = 1; 
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
			Color rgb = new Color(160, 243, 255);
			for (int i = 0; i < 360; i+=6)
            {
				int index2 = Dust.NewDust(npc.position + new Vector2(5, 0).RotatedBy(MathHelper.ToRadians(i)), npc.width, npc.height, 76, npc.velocity.X / 5, (float) npc.velocity.Y, 0, rgb, 0.9f);
			}
			
			
		}
        
    }
}
