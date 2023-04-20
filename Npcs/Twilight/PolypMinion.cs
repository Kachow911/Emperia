using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Twilight
{
    public class PolypMinion : ModNPC
	{
		private const int frameTimer = 12;
		private int speed = 2;
		private int speedMax = 3;
		private int timer = 0;
		private float playerTarget = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Polyp Eye");
			Main.npcFrameCount[NPC.type] = 7;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 25;
			NPC.damage = 25;
			NPC.defense = 0;
			NPC.width = 40;
			NPC.height = 32;
			NPC.aiStyle = -1;
			NPC.knockBackResist = 0f;
			NPC.npcSlots = 1f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath4;
			NPC.noTileCollide = true;
			NPC.value = Item.buyPrice(0, 0, 7, 8);
		}
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 7;
			int frame = (int)NPC.frameCounter;
			NPC.frame.Y = frame * frameHeight;
		}
		public override void AI()
        {
			if (NPC.velocity.X < 0)
			{
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 3.14f;
				NPC.spriteDirection = -1;
			}
			else
			{
				NPC.spriteDirection = 1;
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X);
			}
			
			float num168 = NPC.position.X;
			float num169 = NPC.position.Y;
			float num170 = 10000f;
			bool flag4 = false;
			int num171 = 0;
			timer++;
			if (timer > 30)
			{
				if (playerTarget == 0f)
				{
					int num;
					for (int num172 = 0; num172 < 200; num172 = num + 1)
					{
						float num173 = Main.player[num172].Center.X;
						float num174 = Main.player[num172].Center.Y;
						float num175 = Math.Abs(NPC.position.X + (float)(NPC.width / 2) - num173) + Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - num174);
						if (num175 < num170 && Collision.CanHit(new Vector2(NPC.position.X + (float)(NPC.width / 2), NPC.position.Y + (float)(NPC.height / 2)), 1, 1, Main.player[num172].position, Main.player[num172].width, Main.player[num172].height))
						{
							num170 = num175;
							num168 = num173;
							num169 = num174;
							flag4 = true;
							num171 = num172;
						}
						num = num172;
					}
					if (flag4)
					{
						playerTarget = (float)(num171 + 1);
					}
					flag4 = false;
				}
				if (playerTarget > 0f)
				{
					int num176 = (int)(playerTarget - 1f);
					float num177 = Main.player[num176].position.X + (float)(Main.player[num176].width / 2);
					float num178 = Main.player[num176].position.Y + (float)(Main.player[num176].height / 2);
					float num179 = Math.Abs(NPC.position.X + (float)(NPC.width / 2) - num177) + Math.Abs(NPC.position.Y + (float)(NPC.height / 2) - num178);
					if (num179 < 10000f)
					{
						flag4 = true;
						num168 = Main.player[num176].position.X + (float)(Main.player[num176].width / 2);
						num169 = Main.player[num176].position.Y + (float)(Main.player[num176].height / 2);
					}
					else
					{
						playerTarget = 0f;
					}
				}
				if (flag4)
				{
					float num180 = 5f;
					Vector2 vector19 = NPC.Center;
					float num181 = num168 - vector19.X;
					float num182 = num169 - vector19.Y;
					float num183 = (float)Math.Sqrt((double)(num181 * num181 + num182 * num182));
					num183 = num180 / num183;
					num181 *= num183;
					num182 *= num183;
					int num184 = 8;
					NPC.velocity.X = (NPC.velocity.X * (float)(num184 - 1) + num181) / (float)num184;
					NPC.velocity.Y = (NPC.velocity.Y * (float)(num184 - 1) + num182) / (float)num184;
				}
			}
		}
		
		public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
		{
			NPC.lifeMax = Convert.ToInt32(NPC.lifeMax * 1.4);
			NPC.damage = Convert.ToInt32(NPC.damage * 1.4);
		}
		
	}
}
