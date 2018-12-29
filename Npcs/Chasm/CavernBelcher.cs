using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Chasm
{
    public class CavernBelcher : ModNPC
    {
		public int counter = 100;
		private const float explodeRadius = 120;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chasm Belcher");
			Main.npcFrameCount[npc.type] = 5;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 800;
            npc.damage = 50;
            npc.defense = 7;
            npc.knockBackResist = 100f;
            npc.width = 60;
            npc.height = 66;
            npc.value = Item.buyPrice(0, 15, 0, 0);
            npc.npcSlots = 0f;
            npc.lavaImmune = true;
            npc.noGravity = false;
            npc.noTileCollide = false;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
        }
		 public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
        }
		 public override void AI()
        {
			npc.TargetClosest(true);
			Player player = Main.player[npc.target];
			counter --;
			if (counter <= 0)
			{
				Vector2 placePos1 = new Vector2(npc.Center.X, npc.Center.Y - 75);
				Vector2 placePos2 = new Vector2(npc.Center.X - 30, npc.Center.Y - 60);
				Vector2 placePos3 = new Vector2(npc.Center.X + 30, npc.Center.Y - 60);
				Vector2 direction1 = placePos1 - npc.Center;
				Vector2 direction2 = placePos2 - npc.Center;
				Vector2 direction3 = placePos3 - npc.Center;
				direction1.Normalize();
				direction2.Normalize();
				direction3.Normalize();
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction1.X * 30f, direction1.Y * 30f, mod.ProjectileType("InkShot"), 45, 1, Main.myPlayer, 0, 0);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction1.X * 30f, direction2.Y * 30f, mod.ProjectileType("InkShot"), 45, 1, Main.myPlayer, 0, 0);
				Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction1.X * 30f, direction3.Y * 30f, mod.ProjectileType("InkShot"), 45, 1, Main.myPlayer, 0, 0);
				counter = 100;
			}
			 npc.frameCounter++;
			
		}
    }
    
}
