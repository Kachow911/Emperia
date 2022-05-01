using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.ModLoader.ModContent;

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
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 22500;
            NPC.damage = 40;
            NPC.defense = 13;
            NPC.knockBackResist = 0f;
            NPC.width = 108;
            NPC.height = 272;
            NPC.alpha = 0;
			NPC.boss = true;
            NPC.value = Item.buyPrice(0, 20, 0, 0);
            NPC.npcSlots = 1f;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
        }
		public override void AI()
        {
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
            if (player.dead)
            {
                NPC.TargetClosest(false);
				NPC.velocity.Y = 10f;
				deadCounter--;
				if (deadCounter <= 0)
					NPC.active = false;
            }
			if (NPC.life > (NPC.lifeMax / 3) * 2)
			{
				phase = 1;
			}
			else if (NPC.life > NPC.lifeMax / 4 && NPC.life < (NPC.lifeMax / 3) * 2)
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
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-400, 400), (int)NPC.Center.Y + Main.rand.Next(-400, 400), NPCType<DepthCharge>());
					DepthMineTimer = 80;
				}
				
			}
			if (move == 1 && (phase == 2 || phase == 1))
			{
			
				counter--;
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y - 200);
				NPC.velocity += Vector2.Normalize((targetPos - NPC.Center) * .2f);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -8f, 8f);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -9f, 9f);
				if (counter <= 0)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity.X = direction.X *= 20f;
					NPC.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 2;
					//counter = 120;
					//move = 2;
				}
			}
			if (move == 2 && (phase == 2 || phase == 1))
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
					move = 3;
				}
			}
			if (move == 3 && (phase == 2 || phase == 1))
			{
			
				
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y);
				NPC.velocity += Vector2.Normalize((targetPos - NPC.Center) * .2f);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -4f, 4f);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -4f, 4f);
				if (counter % 20 == 0 && phase == 1)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, direction.X * 10f, direction.Y * 10f, ModContent.ProjectileType<InkShot>(), 40, 1, Main.myPlayer, 0, 0);	
				}
				if (phase == 2)
				{
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X + Main.rand.Next(-400, 400), NPC.Center.Y - 400, 0, 10f, ModContent.ProjectileType<InkShot>(), 25, 1, Main.myPlayer, 0, 0);
				}
				counter--;
				if (counter <= 0)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity.X = direction.X *= 20f;
					NPC.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 4;
					
				}
			}
			if (move == 4 && (phase == 2 || phase == 1))
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
					counter = 240;
					move = 1;
				}
			}
			if (move == 1 && phase == 3)
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
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity.X = direction.X *= 20f;
					NPC.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 2;
				}
			}
			if (move == 2 && phase == 3)
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
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity.X = direction.X *= 20f;
					NPC.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 3;
				}
			}
			if (move == 3 && phase == 3)
			{
				NPC.rotation = (float)Math.Atan2((double)NPC.velocity.Y, (double)NPC.velocity.X) + 1.57f;
				targetPos = new Vector2(player.Center.X, player.Center.Y);
				NPC.velocity += Vector2.Normalize((targetPos - NPC.Center) * .2f);
                NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -6f, 6f);
                NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -6f, 6f);
				if (counter % 10 == 0 && phase == 1)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, direction.X * 10f, direction.Y * 10f, ModContent.ProjectileType<InkShot>(), 80, 1, Main.myPlayer, 0, 0);	
				}
				counter--;
				if (counter <= 0)
				{
					Vector2 direction = Main.player[NPC.target].Center - NPC.Center;
					direction.Normalize();
					NPC.velocity.X = direction.X *= 20f;
					NPC.velocity.Y = direction.Y *= 20f;
					counter = 20;
					move = 1;
				}
			}
		
			
		}
		private void WatchPlayer()
		{
			Vector2 look = new Vector2(Main.player[NPC.target].Center.X + 25, Main.player[NPC.target].Center.Y)  - new Vector2(NPC.position.X + (float)NPC.width * 0.5f, NPC.position.Y + (float)NPC.height * 0.5f);;
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
			NPC.rotation = angle += 90f;
		}
    }
}
