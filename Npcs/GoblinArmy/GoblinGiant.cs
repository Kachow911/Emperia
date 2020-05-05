using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinGiant : ModNPC
    {
        private enum Move
        {
           Walk, 
		   Shoot
        }

		private int counter = 0;

        private Move move { get { return (Move)npc.ai[1]; } set { npc.ai[1] = (int)value; } }
        private Move prevMove;
        private Vector2 targetPosition;
		private int goblinCounter = 600;
		private bool init = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Goblin Giant");
			Main.npcFrameCount[npc.type] = 11;
		}
        public override void SetDefaults()
        {
            npc.lifeMax = 2000;
            npc.damage = 60;
            npc.defense = 12;
            npc.knockBackResist = 0f;
            npc.width = 128;
            npc.height = 132;
            npc.value = Item.buyPrice(0, 2, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.netAlways = true;
			npc.scale = 1f;
        }
		public override void FindFrame(int frameHeight)
		{
			if (move == Move.Walk)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 8; 
				int frame = (int)npc.frameCounter; 
				npc.frame.Y = frame * frameHeight; 
			}
			else if (move == Move.Shoot)
			{
				npc.frameCounter += 0.2f;
				npc.frameCounter %= 3; 
				int frame = (int)npc.frameCounter + 8; 
				npc.frame.Y = frame * frameHeight; 
			}
			
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 2250;
            npc.damage = 55;
        }

        public override void AI()
		{
			goblinCounter--;
			if (goblinCounter <= 0)
            {
				NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-25, 25), (int)npc.Center.Y + 40, mod.NPCType("GoblinRamCarrier"));
				NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-25, 25), (int)npc.Center.Y + 40, mod.NPCType("GoblinRamCarrier"));
				NPC.NewNPC((int)npc.Center.X + Main.rand.Next(-25, 25), (int)npc.Center.Y + 40, mod.NPCType("GoblinRamCarrier"));
				goblinCounter = 600;
            }
			if (npc.velocity.X < 0)
				npc.spriteDirection = -1;
			else if (npc.velocity.X > 0)
				npc.spriteDirection = 1;
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			if (!init)
			{
				move = Move.Walk;
				counter = 300;
				init = true;
			}
			if (move == Move.Walk)
            { 
				counter--;
				npc.aiStyle = 3;
				aiType = 508;
				if (npc.velocity.X > 1.2f)
					npc.velocity.X = 1.2f;
				if (npc.velocity.X < -1.2f)
					npc.velocity.X = -1.2f;
				if (counter <= 0)
				{
					SetMove(Move.Shoot, 15);
				}
			}
			if (move == Move.Shoot)
			{
				Main.PlaySound(SoundID.Item14, player.position);
				counter--;
				if (player.Center.X > npc.Center.X)
					npc.spriteDirection = 1;
				else
					npc.spriteDirection = -1;
				npc.velocity = Vector2.Zero;
				if (counter <= 0)
				{
					SetMove(Move.Walk, 300);
					Vector2 placePosition = npc.Center + new Vector2(0, -npc.height / 2);
					Vector2 direction = Main.player[npc.target].Center + new Vector2(0, -10) - placePosition;
				
					direction.Normalize();
					for (int index = 0; index < 10; ++index)
						Dust.NewDust(placePosition, npc.width, npc.height, 31, 0.0f, 0.0f, 100, new Color(), 1.5f);
					for (int index1 = 0; index1 < 10; ++index1)
					{
						int index2 = Dust.NewDust(placePosition, npc.width, npc.height, 6, 0.0f, 0.0f, 100, new Color(), 2.5f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 3f;
						int index3 = Dust.NewDust(placePosition, npc.width, npc.height, 6, 0.0f, 0.0f, 100, new Color(), 1.5f);
						Main.dust[index3].velocity *= 2f;
					}
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y - npc.height/2, direction.X * 12f, direction.Y * 12f, mod.ProjectileType("GoblinBomb"), npc.damage / 3, 1, Main.myPlayer, 0, 0);
					
				}
			}
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

        private bool IsBelowPhaseTwoThreshhold()
        {
            return npc.life <= npc.lifeMax / 2;     
        }

        private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = Main.tile[x, y].type;
			return Main.invasionType == 1 ? 0.02f : 0;
		}
		public override void NPCLoot()
		{
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Goblin/GoblinGiantGoreHead"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Goblin/GoblinGiantGoreLeg"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Goblin/GoblinGiantGoreArm"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Goblin/GoblinGiantGoreCannon_4"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Goblin/GoblinGiantGoreCannon_3"), 1f);
			Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Goblin/GoblinGiantGoreCannon_1"), 1f);

			Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GiantPlating"), Main.rand.Next(3, 8));
				
				if (Main.rand.Next(5) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GiantsDagger"));
				}
				if (Main.rand.Next(5) == 0)
				{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GiantsDevastator"));
				}
				if (Main.rand.Next(5) == 0)
				{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("OversizedFemur"));
				}
			if (Main.rand.Next(5) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GiantsHead"));
			}
			for (int i = 0; i < 25; i++)
			{
				int dust = Dust.NewDust(npc.position, npc.width, npc.height, 7);
				Vector2 vel = new Vector2(0, -5).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
		}

	}
}
