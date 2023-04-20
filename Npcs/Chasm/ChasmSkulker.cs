using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Chasm
{
    public class ChasmSkulker : ModNPC
    {
		public int counter = 100;
		private int move = 1;
		Vector2 targetPos;
		private const float explodeRadius = 120;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Chasm Belcher");
			Main.npcFrameCount[NPC.type] = 4;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 500;
            NPC.damage = 50;
            NPC.defense = 7;
            NPC.knockBackResist = 100f;
            NPC.width = 32;
            NPC.height = 40;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //true
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
        }
		 public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
			if (NPC.frameCounter >= 15)
			{
				NPC.frame.Y += frameHeight;
				
				if (NPC.frame.Y >= Main.npcFrameCount[NPC.type] * frameHeight)
				{
					NPC.frameCounter = 0;
					NPC.frame.Y = 0;
				}
			} 
        }
		 public override void AI()
        {
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (move == 1)
			{
			
				counter--;
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y);
				NPC.velocity += Vector2.Normalize((targetPos - NPC.Center) * .05f);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -6f, 6f);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -6f, 6f);
				if (counter <= 0)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity.X = direction.X *= 5f;
					NPC.velocity.Y = direction.Y *= 5f;
					counter = 20;
					move = 2;
					
				}
			}
			if (move == 2)
			{
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				if (counter % 2 == 0)
				{
					NPC.velocity.X *= .99f;
					NPC.velocity.Y *= .99f;
				}
				counter--;
				if (counter <= 0)
				{
					counter = 120;
					move = 1;
				}
			}
			
		}
    }
    
}
