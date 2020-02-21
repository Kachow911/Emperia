using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Inquisitor
{
    public class AgonyMask : ModNPC
    {
        private NPC parent { get { return Main.npc[(int)npc.ai[0]]; } set { npc.ai[0] = value.whoAmI; } }
        private float rotate { get { return npc.ai[1]; } set { npc.ai[1] = value; } }
        //private float rotateValue = Main.rand.Next(359);
        private float rotateValue = 0;
        private float dist = 512;
		private int counter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Agony Mask");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 200;
            npc.damage = 0;
            //npc.defense = 7;
            npc.knockBackResist = 0f;
            npc.width = 34;
            npc.height = 54;
            npc.value = Item.buyPrice(0, 0, 0, 0);
            npc.npcSlots = 0f;
            npc.boss = false;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; //57 //20
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;

            npc.netAlways = true;
        }
		public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 250;
            npc.damage = 10;
        }
        public override void AI()
        {
			npc.ai[3]--;
            Vector2 rotatePosition = Vector2.Transform(new Vector2(-1 * dist, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(rotate * 60 + rotateValue))) + parent.Center;
            npc.Center = rotatePosition;
			if (npc.ai[3] <= 0)
				rotateValue += .75f;

            if (!parent.active) {
                npc.life = 0;
            }
			counter ++;
			if (counter % 60 == 0)
			{
				npc.TargetClosest(true);
				Vector2 placePosition = npc.Center;
				Vector2 direction = (Main.player[npc.target].Center - placePosition);
				direction.Normalize();
				Projectile.NewProjectile(placePosition.X, placePosition.Y, direction.X * 7f, direction.Y * 7f, mod.ProjectileType("TearEnemy"), 30, 1, Main.myPlayer, 0, 0);
			}
        }
    }
    
}
