using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

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
			Main.npcFrameCount[npc.type] = 3;
		}

		public override void SetDefaults()
		{
			npc.lifeMax = 160;
			npc.damage = 25;
			npc.defense = 0;
			npc.width = 28;
			npc.height = 26;
			npc.aiStyle = -1;
			npc.knockBackResist = 0f;
			animationType = 81;
			npc.npcSlots = 1f;
			npc.HitSound = SoundID.NPCHit1;
			npc.DeathSound = SoundID.NPCDeath4;
			npc.noTileCollide = true;
			npc.value = Item.buyPrice(0, 0, 7, 8);
		}
		/*public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            npc.frame.Y = 32 * (int)(npc.frameCounter / frameTimer);

            if (npc.frameCounter > 3 * frameTimer)
                npc.frameCounter = 0;
        }*/
		public override void AI()
        {
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			Vector2 direction = (player.Center + new Vector2(0, -player.height) - npc.Center);
			direction.Normalize();
			npc.velocity = direction * speed;
            npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
            npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
			counter++;
			if (counter % 180 == 0)
			{
				for (int i = 0; i < 12; i++)
				{
				
					Vector2 perturbedSpeed = new Vector2(0, 3).RotatedBy(MathHelper.ToRadians(90 + 30 * i));
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("EnemyNeedle"), npc.damage / 3, 1, Main.myPlayer, 0, 0);
				
				}
			}
			npc.frameCounter++;
            npc.frame.Y = 32 * (int)(npc.frameCounter / frameTimer);

            if (npc.frameCounter > 3 * frameTimer)
                npc.frameCounter = 0;
        }
		
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
		{
			npc.lifeMax = Convert.ToInt32(npc.lifeMax * 1.4);
			npc.damage = Convert.ToInt32(npc.damage * 1.4);
		}
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.spawnTileX;
			int y = spawnInfo.spawnTileY;
			int tile = (int)Main.tile[x, y].type;
			return (tile == TileID.Dirt || tile == TileID.Grass) && NPC.downedBoss2 ? 0.05f : 0f;
		}
		
	}
}
