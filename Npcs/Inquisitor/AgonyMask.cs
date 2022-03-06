using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;

namespace Emperia.Npcs.Inquisitor
{
    public class AgonyMask : ModNPC
    {
        private NPC parent { get { return Main.npc[(int)NPC.ai[0]]; } set { NPC.ai[0] = value.whoAmI; } }
        private float rotate { get { return NPC.ai[1]; } set { NPC.ai[1] = value; } }
        //private float rotateValue = Main.rand.Next(359);
        private float rotateValue = 0;
        private float dist = 512;
		private int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Agony Mask");
			Main.npcFrameCount[NPC.type] = 1;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 200;
            NPC.damage = 0;
            //NPC.defense = 7;
            NPC.knockBackResist = 0f;
            NPC.width = 34;
            NPC.height = 54;
            NPC.value = Item.buyPrice(0, 0, 0, 0);
            NPC.npcSlots = 0f;
            NPC.boss = false;
            NPC.lavaImmune = true;
            NPC.noGravity = true;
            NPC.noTileCollide = true;
            NPC.HitSound = SoundID.NPCHit1; //57 //20
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.buffImmune[24] = true;

            NPC.netAlways = true;
        }
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            NPC.lifeMax = 250;
            NPC.damage = 10;
        }
        public override void AI()
        {
			NPC.ai[3]--;
            Vector2 rotatePosition = Vector2.Transform(new Vector2(-1 * dist, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotate * 60 + rotateValue))) + parent.Center;
            NPC.Center = rotatePosition;
			if (NPC.ai[3] <= 0)
				rotateValue += .75f;

            if (!parent.active) {
                NPC.life = 0;
            }
			counter ++;
			if (counter % 60 == 0)
			{
				NPC.TargetClosest(true);
				Vector2 placePosition = NPC.Center;
				Vector2 direction = (Main.player[NPC.target].Center - placePosition);
				direction.Normalize();
				Projectile.NewProjectile(NPC.GetSpawnSource_ForProjectile(), placePosition.X, placePosition.Y, direction.X * 7f, direction.Y * 7f, ModContent.ProjectileType<TearEnemy>(), 30, 1, Main.myPlayer, 0, 0);
			}
        }
    }
    
}
