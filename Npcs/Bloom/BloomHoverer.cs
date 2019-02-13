using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Bloom
{
    public class BloomHoverer : ModNPC
    {
		public int counter = 100;
		private int move = 1;
		Vector2 targetPos;
		private const float explodeRadius = 120;
		private float dist = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Floral Skitter");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 500;
            npc.damage = 50;
            npc.defense = 7;
            npc.knockBackResist = 3f;
            npc.width = 80;
            npc.height = 88;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //true
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
        }
		 public override void AI()
        {
			
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (move == 1)
			{
				if (npc.position.X > player.position.X)
				{
					npc.spriteDirection = 1;
				}
				else if (npc.position.X < player.position.X)
				{
					npc.spriteDirection = -1;
				}
				counter--;
				npc.velocity.X = 0;
				npc.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter <= 0)
				{
					Vector2 targetPos = player.Center + new Vector2(0, -100);
					dist = ((player.Center.Y) - npc.Center.Y) / 60;
					move = 2;
					counter = 60;
				}
			}
			if (move == 2)
			{
				counter--;
				npc.velocity.Y = dist;
				
				if (counter <= 0)
				{
					counter = 60;
					move = 3;
					npc.velocity.Y = 0;
					if (player.position.X > npc.position.X)
					{
						npc.velocity.X = 11f;
						npc.velocity.Y = 0;
						npc.spriteDirection = -1;
					}	
					else
					{
						npc.velocity.X = -11f;
						npc.velocity.Y = 0;
						npc.spriteDirection = 1;
					}
					
			}
			}
			if (move == 3)
			{
				npc.velocity.Y = 0;
				counter--;
				if (counter <= 0)
				{
					dist = ((player.Center.Y - 100) - npc.Center.Y) / 60;
					move = 4;
					counter = 60;
				
				}
			}
			if (move == 4)
			{
				if (npc.position.X > player.position.X)
				{
					npc.spriteDirection = 1;
				}
				else if (npc.position.X < player.position.X)
				{
					npc.spriteDirection = -1;
				}
				counter--;
				npc.velocity.X = 0;
				npc.velocity.Y = dist;
				if (counter <= 0)
				{
					counter = 240;
					move = 1;
				}
			}
			
		}
    }
    
}
