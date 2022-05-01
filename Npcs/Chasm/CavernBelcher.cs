using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Npcs.Chasm
{
    public class CavernBelcher : ModNPC
    {
		public int counter = 100;
		private const float explodeRadius = 120;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Chasm Belcher");
			Main.npcFrameCount[NPC.type] = 5;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 800;
            NPC.damage = 50;
            NPC.defense = 7;
            NPC.knockBackResist = 100f;
            NPC.width = 60;
            NPC.height = 66;
            NPC.value = Item.buyPrice(0, 15, 0, 0);
            NPC.npcSlots = 0f;
            NPC.lavaImmune = true;
            NPC.noGravity = false;
            NPC.noTileCollide = false;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;
            NPC.netAlways = true;
        }
		 public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
        }
		 public override void AI()
        {
			NPC.TargetClosest(true);
			Player player = Main.player[NPC.target];
			counter --;
			if (counter <= 0)
			{
				Vector2 placePos1 = new Vector2(NPC.Center.X, NPC.Center.Y - 75);
				Vector2 placePos2 = new Vector2(NPC.Center.X - 30, NPC.Center.Y - 60);
				Vector2 placePos3 = new Vector2(NPC.Center.X + 30, NPC.Center.Y - 60);
				Vector2 direction1 = placePos1 - NPC.Center;
				Vector2 direction2 = placePos2 - NPC.Center;
				Vector2 direction3 = placePos3 - NPC.Center;
				direction1.Normalize();
				direction2.Normalize();
				direction3.Normalize();
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, direction1.X * 30f, direction1.Y * 30f, ModContent.ProjectileType<InkShot>(), 45, 1, Main.myPlayer, 0, 0);
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, direction1.X * 30f, direction2.Y * 30f, ModContent.ProjectileType<InkShot>(), 45, 1, Main.myPlayer, 0, 0);
				Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, direction1.X * 30f, direction3.Y * 30f, ModContent.ProjectileType<InkShot>(), 45, 1, Main.myPlayer, 0, 0);
				counter = 100;
			}
			 NPC.frameCounter++;
			
		}
    }
    
}
