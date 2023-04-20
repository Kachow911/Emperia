using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;
using static Terraria.Audio.SoundEngine;



namespace Emperia.Npcs.GoblinArmy
{
    public class GoblinBattleRam: ModNPC
    {
		private bool charging;
		int counter = 0;
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Goblin Battle Ram");
			Main.npcFrameCount[NPC.type] = 14;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 250;
            NPC.damage = 15;
            NPC.defense = 3;
            NPC.knockBackResist = -3f;
            NPC.width = 96;
            NPC.height = 56;
            NPC.value = Item.buyPrice(0, 0, 20, 0);
            NPC.npcSlots = 1f;
            NPC.boss = false;
            NPC.lavaImmune = false;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.netAlways = true;
			NPC.scale = 1f;
			NPC.aiStyle = 3;
			AIType = 508;
        }
		public override void FindFrame(int frameHeight)
		{	
			if (charging)
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 6; 
				int frame = (int)NPC.frameCounter + 8; 
				NPC.frame.Y = frame * frameHeight; 
			}
			else
			{
				NPC.frameCounter += 0.2f;
				NPC.frameCounter %= 8; 
				int frame = (int)NPC.frameCounter; 
				NPC.frame.Y = frame * frameHeight; 
			}
		}

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
            NPC.lifeMax = 300;
            NPC.damage = 20;
        }

        public override void AI()
		{
			counter++;
			Player player = Main.player[NPC.target];
			if (Math.Abs(NPC.velocity.X) < 5f)
			{
				if (NPC.Center.X > player.Center.X && Math.Abs(player.Center.X - NPC.Center.X) < 600)
					NPC.velocity.X -= .07f;
				else if (NPC.Center.X < player.Center.X && Math.Abs(player.Center.X - NPC.Center.X) < 600)
					NPC.velocity.X += .07f;
			}
			if (NPC.velocity.X > 5f)
				NPC.velocity.X = 5f;
			if (NPC.velocity.X < -5f)
				NPC.velocity.X = -5f;
			if (NPC.velocity.X > 0)
				NPC.spriteDirection = 1;
			else if (NPC.velocity.X < 0)
				NPC.spriteDirection = -1; 
			charging = (Math.Abs(NPC.velocity.X) > 4.5f);
			if (charging)
			{
				NPC.velocity.Y = 5f;
				Color rgb = new Color(252, 207, 83);
				NPC.damage = 100;
				if (counter % 5 == 0)
					Dust.NewDust(NPC.Center, NPC.width, NPC.height, 76, NPC.velocity.X, (float) NPC.velocity.Y, 0, rgb, 0.9f);
			}
			else
			{
				NPC.damage = 20;
			}
		}
		public override void OnHitPlayer(Player target, Player.HurtInfo hurtInfo)
		{
			if (charging)
			{
				PlaySound(SoundID.Item14, NPC.Center);
				Color rgb = new Color(252, 207, 83);
				for (int i = - 50; i < 50; i++)
				{
					int index2 = Dust.NewDust(NPC.Center + new Vector2(i, 0), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float) NPC.velocity.Y, 0, rgb, 0.9f);
				}
				
				Gore.NewGore(NPC.GetSource_OnHit(target), NPC.position, new Vector2(0, -2), ModContent.Find<ModGore>("Gores/BattleRam").Type, 1f);
				NPC.life = 0;
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 25, (int)NPC.Center.Y, NPCType<GoblinRamCarrier>());
				NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 50, (int)NPC.Center.Y, NPCType<GoblinRamCarrier>());
			}
		}
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
			return Main.invasionType == 1 ? 0.08f : 0;
		}*/


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

        public override void OnKill()
        {
			PlaySound(SoundID.Item14, NPC.Center);
			Color rgb = new Color(252, 207, 83);
			for (int i = -50; i < 50; i++)
			{
				int index2 = Dust.NewDust(NPC.Center + new Vector2(i, 0), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float)NPC.velocity.Y, 0, rgb, 0.9f);
			}

			Gore.NewGore(NPC.GetSource_Death(), NPC.position, new Vector2(0, -2), ModContent.Find<ModGore>("Gores/BattleRam").Type, 1f);
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X + 25, (int)NPC.Center.Y, NPCType<GoblinRamCarrier>());
			NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X - 50, (int)NPC.Center.Y, NPCType<GoblinRamCarrier>());
		}
        
    }
}
