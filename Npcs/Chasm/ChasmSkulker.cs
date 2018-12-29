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
			DisplayName.SetDefault("Chasm Belcher");
			Main.npcFrameCount[npc.type] = 4;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.damage = 50;
            npc.defense = 7;
            npc.knockBackResist = 100f;
            npc.width = 32;
            npc.height = 40;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //true
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
        }
		 public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
			if (npc.frameCounter >= 15)
			{
				npc.frame.Y += frameHeight;
				
				if (npc.frame.Y >= Main.npcFrameCount[npc.type] * frameHeight)
				{
					npc.frameCounter = 0;
					npc.frame.Y = 0;
				}
			} 
        }
		 public override void AI()
        {
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (move == 1)
			{
			
				counter--;
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y);
				npc.velocity += Vector2.Normalize((targetPos - npc.Center) * .05f);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -6f, 6f);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -6f, 6f);
				if (counter <= 0)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity.X = direction.X *= 5f;
					npc.velocity.Y = direction.Y *= 5f;
					counter = 20;
					move = 2;
					
				}
			}
			if (move == 2)
			{
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				if (counter % 2 == 0)
				{
					npc.velocity.X *= .99f;
					npc.velocity.Y *= .99f;
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
