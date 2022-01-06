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
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 500;
            NPC.damage = 50;
            NPC.defense = 7;
            NPC.knockBackResist = 3f;
            NPC.width = 80;
            NPC.height = 88;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //true
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
        }
		 public override void AI()
        {
			
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (move == 1)
			{
				if (NPC.position.X > player.position.X)
				{
					NPC.spriteDirection = 1;
				}
				else if (NPC.position.X < player.position.X)
				{
					NPC.spriteDirection = -1;
				}
				counter--;
				NPC.velocity.X = 0;
				NPC.velocity.Y = 0.5f * (float)Math.Cos(MathHelper.ToRadians(counter * 2));
				if (counter <= 0)
				{
					Vector2 targetPos = player.Center + new Vector2(0, -100);
					dist = ((player.Center.Y) - NPC.Center.Y) / 60;
					move = 2;
					counter = 60;
				}
			}
			if (move == 2)
			{
				counter--;
				NPC.velocity.Y = dist;
				
				if (counter <= 0)
				{
					counter = 60;
					move = 3;
					NPC.velocity.Y = 0;
					if (player.position.X > NPC.position.X)
					{
						NPC.velocity.X = 11f;
						NPC.velocity.Y = 0;
						NPC.spriteDirection = -1;
					}	
					else
					{
						NPC.velocity.X = -11f;
						NPC.velocity.Y = 0;
						NPC.spriteDirection = 1;
					}
					
			}
			}
			if (move == 3)
			{
				NPC.velocity.Y = 0;
				counter--;
				if (counter <= 0)
				{
					dist = ((player.Center.Y - 100) - NPC.Center.Y) / 60;
					move = 4;
					counter = 60;
				
				}
			}
			if (move == 4)
			{
				if (NPC.position.X > player.position.X)
				{
					NPC.spriteDirection = 1;
				}
				else if (NPC.position.X < player.position.X)
				{
					NPC.spriteDirection = -1;
				}
				counter--;
				NPC.velocity.X = 0;
				NPC.velocity.Y = dist;
				if (counter <= 0)
				{
					counter = 240;
					move = 1;
				}
			}
			
		}
    }
    
}
