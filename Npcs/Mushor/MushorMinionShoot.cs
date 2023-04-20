using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles.Mushroom;

namespace Emperia.Npcs.Mushor
{   //coded by kachow

	
    public class MushorMinionShoot : ModNPC
    {
        
        private int counter { get { return (int)NPC.ai[1]; } set { NPC.ai[1] = value; } }
		private const float shootRadius = 280;
        private const float speedMax = 5;
        private const float speed = 2;
		private bool shooting = false;

        private const int frameTimer = 4;

        private bool created;

		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Angry Mushroom");
			Main.npcFrameCount[NPC.type] = 15;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 75;
            NPC.damage = 23;
            NPC.defense = 7;
            NPC.knockBackResist = 0f;
            NPC.width = 40;
            NPC.height = 40;
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

        public override void ApplyDifficultyAndPlayerScaling(int numPlayers, float balance, float bossAdjustment)/* tModPorter Note: bossLifeScale -> balance (bossAdjustment is different, see the docs for details) */
        {
           NPC.lifeMax = 80;
        }

        public override void FindFrame(int frameHeight)
        {
            NPC.frameCounter++;
            NPC.frame.Y = frameHeight * (int)(NPC.frameCounter / frameTimer);

            if (NPC.frameCounter > 13 * frameTimer)
                NPC.frameCounter = 0;
        }

        public override void AI()
        {
            Player player = Main.player[NPC.target];
            if (!player.active || player.dead)
            {
                NPC.TargetClosest(false);
                player = Main.player[NPC.target];
            }

            if (!created)
            {
                NPC.velocity = new Vector2(-1, 0).RotatedByRandom(MathHelper.ToRadians(360)) * .8f;   //float in a random direction
                counter = 120;
                created = true;
            }
            if (!shooting)
			{
				NPC.velocity += Vector2.Normalize((player.Center - NPC.Center) * speed);
				NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speedMax, speedMax);
				NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -speedMax, speedMax);
				if (NPC.Distance(player.Center) < shootRadius)  
				{
					counter = 120;
					shooting = true;
				}
			}
			else
			{
				NPC.velocity = Vector2.Zero;
				counter--;
				if (Main.rand.Next(counter) == 0)
				{
					int dust = Dust.NewDust(new Vector2(NPC.Center.X, NPC.Center.Y), NPC.width / 8, NPC.height / 8, DustID.PurificationPowder, 0f, 0f, 0, new Color(39, 90, 219), 0.5f);
				}
				if (counter <= 0)
				{
					Vector2 direction = (Main.player[NPC.target].Center - NPC.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-10, 10)));
					direction.Normalize();
					Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, direction.X * 8f, direction.Y * 8f, ModContent.ProjectileType<BigShroom>(), 30, 1, Main.myPlayer, 0, 0);
					shooting = false;
					
				}
				
			}
            
			
        }
       
		
    }
}
