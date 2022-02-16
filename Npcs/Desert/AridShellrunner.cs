using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.ItemDropRules;
using Terraria.DataStructures;

namespace Emperia.Npcs.Desert
{
    public class AridShellrunner : ModNPC
    {
    
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arid Shellrunner");
			Main.npcFrameCount[NPC.type] = 5;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 125;
            NPC.damage = 30;
            NPC.defense = 13;
            NPC.knockBackResist = 0.1f;
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
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 5; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight; 
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 150;
            NPC.damage = 40;
        }

        public override void AI()
		{
			Player player = Main.player[NPC.target];
			if (Math.Abs(NPC.velocity.X) < 5f)
			{
				if (NPC.Center.X > player.Center.X)
					NPC.velocity.X -= .05f;
				else if (NPC.Center.X < player.Center.X)
					NPC.velocity.X += .05f;
			}
			if (NPC.velocity.X > 0)
				NPC.spriteDirection = 1;
			else if (NPC.velocity.X < 0)
				NPC.spriteDirection = -1; 
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
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = Main.tile[x, y].TileType;
			return spawnInfo.player.ZoneDesert ? 0.1f : 0;
		}

        public override void OnKill()
        {
			Color rgb = new Color(252, 207, 83);
			for (int i = -50; i < 50; i++)
			{
				int index2 = Dust.NewDust(NPC.position + new Vector2(i, 0), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float)NPC.velocity.Y, 0, rgb, 0.9f);
			}
			for (int i = 0; i < Main.player.Length; i++)
			{
				if (NPC.Distance(Main.player[i].Center) < 128)
					Main.player[i].Hurt(PlayerDeathReason.ByCustomReason($"{Main.player[i].name} was caught in the blast of Arid Shellrunner."), NPC.damage / 2, 0);
			}
		}
        public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Main.NewText("lol");
			if (Main.rand.Next(2) == 0)
			{
				npcLoot.Add(ItemDropRule.Common(ModContent.ItemType<Items.Sets.PreHardmode.Desert.PolishedSandstone>(), 2)); //Drop zombie arm with a 1 out of 250 chance.	

				//Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Sets.PreHardmode.Desert.PolishedSandstone>(), Main.rand.Next(1, 4));
			}
		}
        
    }
}
