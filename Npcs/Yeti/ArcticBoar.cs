using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Yeti
{
    public class ArcticBoar : ModNPC
    {
    
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arctic Boar");
			Main.npcFrameCount[NPC.type] = 3;
		}
        public override void SetDefaults()
        {
            NPC.lifeMax = 250;
            NPC.damage = 30;
            NPC.defense = 5;
            NPC.knockBackResist = -2f;
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
			NPC.frameCounter %= 3; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight; 
		}

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 300;
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
				NPC.spriteDirection = -1;
			else if (NPC.velocity.X < 0)
				NPC.spriteDirection = 1; 
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
			int tile = Main.tile[x, y].type;
			return spawnInfo.player.ZoneSnow ? 0.05f : 0;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			Color rgb = new Color(160, 243, 255);
			for (int i = 0; i < 360; i+=6)
            {
				int index2 = Dust.NewDust(NPC.position + new Vector2(5, 0).RotatedBy(MathHelper.ToRadians(i)), NPC.width, NPC.height, 76, NPC.velocity.X / 5, (float) NPC.velocity.Y, 0, rgb, 0.9f);
			}
			if (Main.rand.Next(3) == 0)
			{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.GelidHide>());
			}
			if (Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Accessories.BoarTusk>());
			}
		}
        
    }
}
