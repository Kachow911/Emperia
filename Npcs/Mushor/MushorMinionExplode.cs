using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.Audio.SoundEngine;

namespace Emperia.Npcs.Mushor
{   //coded by BlueRaven and kachow

	
    public class MushorMinionExplode : ModNPC
    {
        
        private int counter { get { return (int)NPC.ai[1]; } set { NPC.ai[1] = value; } }
		private const float damageDistance = 64;
        private const float speedMax = 8;
        private const float speed = 2;


        private const int frameTimer = 4;

        private bool created;
		private bool exploded = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Angry Mushroom");
			Main.npcFrameCount[NPC.type] = 15;
		}
        public override void SetDefaults()
        {
            NPC.aiStyle = -1;
            NPC.lifeMax = 50;
            NPC.damage = 50;
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

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
           NPC.lifeMax = 60;
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
                counter = 240;
                created = true;
            }
            counter--;

            NPC.velocity += Vector2.Normalize((player.Center - NPC.Center) * speed);
            NPC.velocity.X = MathHelper.Clamp(NPC.velocity.X, -speedMax, speedMax);
            NPC.velocity.Y = MathHelper.Clamp(NPC.velocity.Y, -speedMax, speedMax);
            if (NPC.Distance(player.Center) < 32 || counter <= 0)   //if really close to the enemy or dying or the counter is 0
            {
				Explode(NPC.damage);
            }
        }
        
		private void Explode(int damage)
		{
			if (!exploded)
			{
				NPC.life = 0;
				PlaySound(SoundID.Item14, NPC.Center);    //bomb explosion sound
				PlaySound(SoundID.Item21, NPC.Center); 
				for (int i = 0; i < Main.player.Length; i++)
                {
                    Player player = Main.player[i];
                    if (NPC.Distance(player.Center) < damageDistance)
                    {
                        player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(NPC.whoAmI), NPC.damage, 0);
                    }
                }
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-damageDistance, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                    if (i % 8 == 0)
                    {   //odd
                        Dust.NewDust(NPC.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20);
                    }

                    if (i % 9 == 0)
                    {   //even
                        vec.Normalize();
                        Dust.NewDust(NPC.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, vec.X * 2, vec.Y * 2);
                    }
                }
				exploded = true;
			}
		}
		
    }
}
