using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Kraken
{
    public class TheKraken : ModNPC
    {
		int counter = 240;
		int DepthMineTimer = 80;
		float angleShot;
		int phase = 1;
		int move = 1;
		int deadCounter = 20;
		Vector2 targetPos;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Kraken");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 22500;
            npc.damage = 40;
            npc.defense = 13;
            npc.knockBackResist = 0f;
            npc.width = 108;
            npc.height = 272;
            npc.alpha = 0;
			npc.boss = true;
            npc.value = Item.buyPrice(0, 20, 0, 0);
            npc.npcSlots = 1f;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
        }
		public override void AI()
        {
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
            if (player.dead)
            {
                npc.TargetClosest(false);
				npc.velocity.Y = 10f;
				deadCounter--;
				if (deadCounter <= 0)
					npc.active = false;
            }
			if (npc.life > (npc.lifeMax / 3) * 2)
			{
				phase = 1;
			}
			else if (npc.life > npc.lifeMax / 4 && npc.life < (npc.lifeMax / 3) * 2)
			{
				phase = 2;
			}
			else
			{			
				phase = 3;
			}
			if (phase < 2)
			{
				DepthMineTimer--;
				if (DepthMineTimer <= 0)
				{
					NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-400, 400), (int)npc.Center.Y + Main.rand.Next(-400, 400), mod.NPCType("DepthCharge"));
					DepthMineTimer = 80;
				}
				
			}
			if (move == 1 && (phase == 2 || phase == 1))
			{
			
				counter--;
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y - 200);
				npc.velocity += Vector2.Normalize((targetPos - npc.Center) * .2f);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -8f, 8f);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -9f, 9f);
				if (counter <= 0)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity.X = direction.X *= 20f;
					npc.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 2;
					//counter = 120;
					//move = 2;
				}
			}
			if (move == 2 && (phase == 2 || phase == 1))
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
					move = 3;
				}
			}
			if (move == 3 && (phase == 2 || phase == 1))
			{
			
				
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y);
				npc.velocity += Vector2.Normalize((targetPos - npc.Center) * .2f);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -4f, 4f);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -4f, 4f);
				if (counter % 20 == 0 && phase == 1)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 10f, direction.Y * 10f, mod.ProjectileType("InkShot"), 40, 1, Main.myPlayer, 0, 0);	
				}
				if (phase == 2)
				{
					Projectile.NewProjectile(npc.Center.X + Main.rand.Next(-400, 400), npc.Center.Y - 400, 0, 10f, mod.ProjectileType("InkShot"), 25, 1, Main.myPlayer, 0, 0);
				}
				counter--;
				if (counter <= 0)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity.X = direction.X *= 20f;
					npc.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 4;
					
				}
			}
			if (move == 4 && (phase == 2 || phase == 1))
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
					counter = 240;
					move = 1;
				}
			}
			if (move == 1 && phase == 3)
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
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity.X = direction.X *= 20f;
					npc.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 2;
				}
			}
			if (move == 2 && phase == 3)
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
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity.X = direction.X *= 20f;
					npc.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 3;
				}
			}
			if (move == 3 && phase == 3)
			{
				npc.rotation = (float)Math.Atan2((double)npc.velocity.Y, (double)npc.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y);
				npc.velocity += Vector2.Normalize((targetPos - npc.Center) * .2f);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -6f, 6f);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -6f, 6f);
				if (counter % 10 == 0 && phase == 1)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 10f, direction.Y * 10f, mod.ProjectileType("InkShot"), 80, 1, Main.myPlayer, 0, 0);	
				}
				counter--;
				if (counter <= 0)
				{
					Vector2 direction = Main.player[npc.target].Center - npc.Center;
					direction.Normalize();
					npc.velocity.X = direction.X *= 20f;
					npc.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 1;
				}
			}
		
			
		}
		private void WatchPlayer()
		{
			Vector2 look = new Vector2(Main.player[npc.target].Center.X + 25, Main.player[npc.target].Center.Y)  - new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);;
			LookAt(look);
		}
		private void LookAt(Vector2 look)
		{
			float angle = 0.5f * (float)Math.PI;
			if (look.X != 0f)
			{
				angle = (float)Math.Atan(look.Y / look.X);
			}
			else if (look.Y < 0f)
			{
				angle += (float)Math.PI;
			}
			if (look.X < 0f)
			{
				angle += (float)Math.PI;
			}
			npc.rotation = angle += 90f;
		}
    }
}
