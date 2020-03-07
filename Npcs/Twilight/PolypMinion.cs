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
			DisplayName.SetDefault("Polyp Eye");
			Main.npcFrameCount[npc.type] = 7;
		}

		public override void SetDefaults()
		{
			npc.lifeMax = 25;
			npc.damage = 25;
			npc.defense = 0;
			npc.width = 40;
			npc.height = 32;
			npc.aiStyle = -1;
			npc.knockBackResist = 0f;
			npc.npcSlots = 1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath4;
			npc.noTileCollide = true;
			npc.value = Item.buyPrice(0, 0, 7, 8);
		}
		public override void FindFrame(int frameHeight)
		{
			npc.frameCounter += 0.2f;
			npc.frameCounter %= 7;
			int frame = (int)npc.frameCounter;
			npc.frame.Y = frame * frameHeight;
		}
		public override void AI()
        {
			if (npc.velocity.X < 0)
			{
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 3.14f;
				npc.spriteDirection = -1;
			}
			else
			{
				npc.spriteDirection = 1;
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X);
			}
			
			float num168 = npc.position.X;
			float num169 = npc.position.Y;
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
						float num175 = Math.Abs(npc.position.X + (float)(npc.width / 2) - num173) + Math.Abs(npc.position.Y + (float)(npc.height / 2) - num174);
						if (num175 < num170 && Collision.CanHit(new Vector2(npc.position.X + (float)(npc.width / 2), npc.position.Y + (float)(npc.height / 2)), 1, 1, Main.player[num172].position, Main.player[num172].width, Main.player[num172].height))
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
					float num179 = Math.Abs(npc.position.X + (float)(npc.width / 2) - num177) + Math.Abs(npc.position.Y + (float)(npc.height / 2) - num178);
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
					Vector2 vector19 = npc.Center;
					float num181 = num168 - vector19.X;
					float num182 = num169 - vector19.Y;
					float num183 = (float)Math.Sqrt((double)(num181 * num181 + num182 * num182));
					num183 = num180 / num183;
					num181 *= num183;
					num182 *= num183;
					int num184 = 8;
					npc.velocity.X = (npc.velocity.X * (float)(num184 - 1) + num181) / (float)num184;
					npc.velocity.Y = (npc.velocity.Y * (float)(num184 - 1) + num182) / (float)num184;
				}
			}
		}
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = Convert.ToInt32(npc.lifeMax * 1.4);
			npc.damage = Convert.ToInt32(npc.damage * 1.4);
		}
		
	}
}
