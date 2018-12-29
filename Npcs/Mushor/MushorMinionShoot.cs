using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Mushor
{   //coded by kachow

	
    public class MushorMinionShoot : ModNPC
    {
        
        private int counter { get { return (int)npc.ai[1]; } set { npc.ai[1] = value; } }
		private const float shootRadius = 280;
        private const float speedMax = 5;
        private const float speed = 2;
		private bool shooting = false;

        private const int frameTimer = 4;

        private bool created;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Angry Mushroom");
			Main.npcFrameCount[npc.type] = 15;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 75;
            npc.damage = 23;
            npc.defense = 7;
            npc.knockBackResist = 0f;
            npc.width = 40;
            npc.height = 40;
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
           npc.lifeMax = 80;
        }

        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter++;
            npc.frame.Y = frameHeight * (int)(npc.frameCounter / frameTimer);

            if (npc.frameCounter > 13 * frameTimer)
                npc.frameCounter = 0;
        }

        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
            }

            if (!created)
            {
                npc.velocity = new Vector2(-1, 0).RotatedByRandom(MathHelper.ToRadians(360)) * .8f;   //float in a random direction
                counter = 120;
                created = true;
            }
            if (!shooting)
			{
				npc.velocity += Vector2.Normalize((player.Center - npc.Center) * speed);
				npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
				npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
				if (npc.Distance(player.Center) < shootRadius)  
				{
					counter = 120;
					shooting = true;
				}
			}
			else
			{
				npc.velocity = Vector2.Zero;
				counter--;
				if (Main.rand.Next(counter) == 0)
				{
					int dust = Dust.NewDust(new Vector2(npc.Center.X, npc.Center.Y), npc.width / 8, npc.height / 8, 20, 0f, 0f, 0, new Color(39, 90, 219), 0.5f);
				}
				if (counter <= 0)
				{
					Vector2 direction = (Main.player[npc.target].Center - npc.Center).RotatedBy(MathHelper.ToRadians(Main.rand.Next(-10, 10)));
					direction.Normalize();
					Projectile.NewProjectile(npc.Center.X, npc.Center.Y, direction.X * 8f, direction.Y * 8f, mod.ProjectileType("BigShroom"), 30, 1, Main.myPlayer, 0, 0);
					shooting = false;
					
				}
				
			}
            
			
        }
       
		
    }
}
