using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Yeti;
using Emperia.Items.Weapons.Yeti;
using static Terraria.ModLoader.ModContent;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Npcs.Yeti
{
    public class Yeti : ModNPC
    {
        private enum Move
        {
           Walk, 
		   JumpStart,
		   Jump,
		   Snowball,
		   YetilingSpawn,
		   IcicleStart
        }

		private int counter;

		private Move move;
        private Move prevMove;
        private Vector2 targetPosition;

		private int side;
		private int counter3 = 0;
        private bool phase2Active;
		private bool init = false;
		private int walkTimer = 1;
		private int remainingTime = 0;
		private int jumpDir = 1;
		private float maxWalkSpeed = 2f;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("The Yeti");
			Main.npcFrameCount[NPC.type] = 18;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 2650;
            NPC.damage = 50;
            NPC.defense = 12;
            NPC.knockBackResist = 0f;
            NPC.width = 45;
            NPC.height = 55;
            NPC.value = Item.buyPrice(0, 8, 0, 0);
            NPC.npcSlots = 1f;
            NPC.boss = true;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
			NPC.scale = 2f;
			//BossBag = ModContent.ItemType<Items.YetiBag>();
        }
		public override void FindFrame(int frameHeight)
		{
			if (move == Move.Walk)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 8; 
				int frame = (int)NPC.frameCounter; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.YetilingSpawn || move == Move.IcicleStart)
            {
				NPC.frameCounter += 0.5f;
				int frame;
				if (counter > 12)
				{
					NPC.frameCounter %= 4;
					frame = (int)NPC.frameCounter + 8;
				}
				else
				{
					NPC.frameCounter %= 2;
					frame = (int)NPC.frameCounter + 12;
				}
				NPC.frame.Y = frame * frameHeight;
			}
			else if (move == Move.JumpStart && counter >= 70)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 4; 
				int frame = (int)NPC.frameCounter + 8; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Jump)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 2; 
				int frame = (int)NPC.frameCounter + 12; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Snowball && counter >= 5)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 4; 
				int frame = (int)NPC.frameCounter + 14; 
				NPC.frame.Y = frame * frameHeight; 
			}
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 3100;
            NPC.damage = 60;
        }

        public override void AI()
		{
			counter3++;
			if (IsBelowPhaseTwoThreshhold() && !phase2Active)
			{
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y - 150, NPCType<Yetiling>());
				phase2Active = true;
			}
			if (NPC.velocity.X < 0)
				NPC.spriteDirection = 1;
			else if (NPC.velocity.X > 0)
				NPC.spriteDirection = -1;
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			if (!player.active || player.dead)
			{
				move = Move.Walk;
				NPC.TargetClosest(false);
				NPC.velocity.Y = 25;
				NPC.noTileCollide = true;
				if (NPC.timeLeft > 10)
				{
					NPC.timeLeft = 10;
				}
			}
			
			if (!init)
			{
				move = Move.Walk;
				counter = 600;
				init = true;
			}
			if (move == Move.YetilingSpawn)
			{
				if (counter == 12) NPC.velocity.Y = -6;
				counter--;
				if (NPC.velocity.Y >= 0) NPC.velocity.Y *= 1.2f;
				if (counter <= 0 && NPC.velocity.Y == 0)
				{
					PlaySound(SoundID.Dig, NPC.Center);
					for (int i = -50; i < 50; i++)
					{
						Color rgb = new Color(255, 255, 255);
						int index2 = Dust.NewDust(NPC.position + new Vector2(i, NPC.height), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float)NPC.velocity.Y, 0, rgb, 0.9f);
					}
					NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + Main.rand.Next(-75, 75), (int)NPC.Center.Y + NPC.height - 58, NPCType<YetilingInit>());
					SetMove(Move.Walk, remainingTime);
					remainingTime = 0;
				}

			}
			if (move == Move.IcicleStart)
			{
				if (counter == 12) NPC.velocity.Y = -6;
				counter--;
				if (NPC.velocity.Y >= 0) NPC.velocity.Y *= 1.2f;
				if (counter <= 0 && NPC.velocity.Y == 0)
				{
					PlaySound(SoundID.Dig, NPC.Center);
					for (int i = -50; i < 50; i++)
					{
						Color rgb = new Color(160, 243, 255);
						int index2 = Dust.NewDust(NPC.position + new Vector2(i, NPC.height), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float)NPC.velocity.Y, 0, rgb, 0.9f);
					}
					for (int i = 0; i < 12; i++)
					{

						Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(Main.rand.Next(30) + 30 * i)) * 1.8f;
						Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<IcicleC>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);

					}
					SetMove(Move.Walk, remainingTime);
					remainingTime = 0;
				}
			}
			if (move == Move.Walk)
			{
				walkTimer++;
				counter--;
				NPC.aiStyle = 3;
				AIType = 508;
				if(Vector2.Distance(player.Center, NPC.Center) > 800 || (player.Center.Y - NPC.Center.Y >= 350))
				{
					SetMove(Move.JumpStart, 90);
				}
				if (!IsBelowPhaseTwoThreshhold() && counter % 200 == 0)
				{
					//NPC.NewNPC((int)NPC.Center.X + Main.rand.Next(-200, 200), (int)NPC.Center.Y - 150, NPCType<Yetiling>());
					if(Main.rand.NextBool(3))
						SetMove(Move.YetilingSpawn, 25, counter);
					else if (Main.rand.NextBool(2))
						SetMove(Move.Snowball, 25, counter);
				}
				if (!IsBelowPhaseTwoThreshhold())
				{
					if (player.position.X > NPC.Center.X)
						NPC.velocity.X += 0.2f;
					else
						NPC.velocity.X -= 0.2f;
					if (NPC.velocity.X > 2.5f)
						NPC.velocity.X = 2.5f;
					if (NPC.velocity.X < -2.5f)
						NPC.velocity.X = -2.5f;
				}
				else
                {
					if (Main.rand.NextBool(80) && counter3 > 90)
					{
						counter3 = 0;
						if (Vector2.Distance(player.Center, NPC.Center) > 400 && Main.rand.Next(2) == 0)
							SetMove(Move.Snowball, 25, counter);
						else
							SetMove(Move.IcicleStart, 20, counter);
					}
					if (player.position.X > NPC.Center.X)
						NPC.velocity.X += 0.2f;
					else
						NPC.velocity.X -= 0.2f;
					if (NPC.velocity.X > 3.7f)
						NPC.velocity.X = 3.7f;
					if (NPC.velocity.X < -3.7f)
						NPC.velocity.X = -3.7f;
				}
				if (Main.rand.Next(2) == 0 && counter <= 0 && Main.expertMode && walkTimer > 400)
				{
					SetMove(Move.Snowball, 25);
				}
				else if (counter <= 0 && walkTimer > 400)
				{
					SetMove(Move.JumpStart, 90);
				}
			}
			if (move == Move.JumpStart)
			{
				NPC.velocity.X = 0;
				counter--;
				NPC.aiStyle = -1;
				Color rgb = new Color(160, 243, 255);
				int index2 = Dust.NewDust(NPC.position, NPC.width, NPC.height, 76, (float) NPC.velocity.X, (float) NPC.velocity.Y, 0, rgb, 0.9f);
				if (counter <= 0)
				{
				NPC.noTileCollide = true;
					SetMove(Move.Jump, 1);
					if (player.Center.X > NPC.Center.X)
					{
						NPC.velocity.Y = -12;
						NPC.velocity.X = 6.5f;
						jumpDir = 1;
					}
					else
					{
						NPC.velocity.Y = -12;
						NPC.velocity.X = -6.5f;
						jumpDir = -1;
					}
				}
			}
			if (move == Move.Jump)
			{
				counter ++;
				if (counter < 40)
				{
					if (jumpDir == 1)
						NPC.velocity.X = Math.Abs(player.Center.X - NPC.Center.X) / 20;
					else
						NPC.velocity.X = -1 * Math.Abs(player.Center.X - NPC.Center.X) / 20;
				}
				if (NPC.velocity.Y > 0)
					NPC.noTileCollide = false;
				if (NPC.velocity.Y == 0)
				{
					PlaySound(SoundID.Dig, NPC.Center);
					PlaySound(SoundID.Item107, NPC.Center);
					if (IsBelowPhaseTwoThreshhold())
					{
						walkTimer = 1;
						SetMove(Move.Walk, 500);
					}
					else
					{
						walkTimer = 1;
						SetMove(Move.Walk, 600);
					}
					Color rgb = new Color(160, 243, 255);
					for (int i = - 50; i < 50; i++)
					{
						int index2 = Dust.NewDust(NPC.position + new Vector2(i, 0), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float) NPC.velocity.Y, 0, rgb, 0.9f);
					}
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y + NPC.height / 2, 2, 0, ModContent.ProjectileType<YetiProjOne>(), 0, 1, Main.myPlayer, 0, 0);
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y + NPC.height / 2, -2, 0, ModContent.ProjectileType<YetiProjOne>(), 0, 1, Main.myPlayer, 0, 0);
					//go back lool
				}
			}
			if (move == Move.Snowball)
			{
				
				counter--;
				if (player.Center.X > NPC.Center.X)
					NPC.spriteDirection = 1;
				else
					NPC.spriteDirection = -1;
				NPC.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					walkTimer = 1;
					if (remainingTime > 0)
					{
						SetMove(Move.Walk, remainingTime);
						remainingTime = 0;
					}
					else
						SetMove(Move.Walk, 200);
					Vector2 placePosition = NPC.Center + new Vector2(0, -NPC.height / 2);
					Vector2 direction = Main.player[NPC.target].Center + new Vector2(0, -115) - placePosition;
					direction.Normalize();
					direction *= 9f;
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y - NPC.height/2, direction.X, direction.Y, ModContent.ProjectileType<YetiSnowball>(), NPC.damage / 2, 1, Main.myPlayer, 0, 0);
					
				}
			}
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

        private bool IsBelowPhaseTwoThreshhold()
        {
            return NPC.life <= NPC.lifeMax / 2;     
        }

		private void SetMove(Move toMove, int counter, int remaining = 0)
        {
			remainingTime = remaining;
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}

        public override void OnKill()
        {
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Yeti/gore1").Type, 1f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Yeti/gore2").Type, 1f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Yeti/gore3").Type, 1f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Yeti/gore4").Type, 1f);
			Gore.NewGore(NPC.GetSource_Death(), NPC.position, NPC.velocity, ModContent.Find<ModGore>("Gores/Yeti/gore5").Type, 1f);
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			/*if (!EmperialWorld.downedMushor)
			{
            	Main.NewText("The guardian of the mushroom biome has fallen...", 0, 75, 161, false);
				EmperialWorld.downedMushor = true;
			}*/
			//if (Main.rand.Next(10) == 0)
			//{
			//	Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<YetiTrophy>());
			//}
			if (Main.expertMode)
			{
				Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.YetiBag>());
				//NPC.DropBossBags();
			}
			else
			{
				
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<MammothineClub>());
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Weapons.Yeti.HuntersSpear>());
				}
				if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<IcicleCannon>());
				}
				
				if (Main.rand.Next(7) == 0)
				{
					Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Armor.YetiMask>());
				}
				if (Main.rand.Next(10) == 0)
				{
				Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.ChilledFootprint>());
				}
				if (Main.rand.Next(2) == 0)
				{
				Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<ArcticIncantation>());
				}
				Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Sets.PreHardmode.Frostleaf.Frostleaf>(), Main.rand.Next(20, 30)); 
			}
		}
        
    }
}
