using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Npcs
{
    public class ForestSpirit : ModNPC
	{
		private const int frameTimer = 12;
		private int speed = 2;
		private int speedMax = 3;
		private int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Forest Spirit");
			Main.npcFrameCount[NPC.type] = 3;
		}

		public override void SetDefaults()
		{
			NPC.lifeMax = 160;
			NPC.damage = 25;
			NPC.defense = 0;
			NPC.width = 28;
			NPC.height = 26;
			NPC.aiStyle = -1;
			NPC.knockBackResist = 0f;
			AnimationType = 81;
			NPC.npcSlots = 1f;
			NPC.HitSound = SoundID.NPCHit1;
			NPC.DeathSound = SoundID.NPCDeath4;
			NPC.noTileCollide = true;
			NPC.value = Item.buyPrice(0, 0, 7, 8);
		}
		/*public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            NPC.frame.Y = 32 * (int)(NPC.frameCounter / frameTimer);

            if (NPC.frameCounter > 3 * frameTimer)
                NPC.frameCounter = 0;
        }*/
		public override void AI()
        {
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			Vector2 direction = (player.Center + new Vector2(0, -player.height) - NPC.Center);
			direction.Normalize();
			NPC.velocity = direction * speed;
            NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speedMax, speedMax);
            NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -speedMax, speedMax);
			counter++;
			if (counter % 180 == 0)
			{
				for (int i = 0; i < 12; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 30 * i));
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<EnemyNeedle>(), NPC.damage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}
			NPC.frameCounter++;
            NPC.frame.Y = 32 * (int)(NPC.frameCounter / frameTimer);

            if (NPC.frameCounter > 3 * frameTimer)
                NPC.frameCounter = 0;
        }
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			NPC.lifeMax = Convert.ToInt32(NPC.lifeMax * 1.4);
			NPC.damage = Convert.ToInt32(NPC.damage * 1.4);
		}
		/*public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = (int)Main.tile[x, y].TileType;
			return (tile == TileID.Dirt || tile == TileID.Grass) && NPC.downedBoss2 ? 0.05f : 0f;
		}*/
		
	}
}
