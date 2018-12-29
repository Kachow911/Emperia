using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Npcs.Mushor
{   //coded by BlueRaven and kachow

	
    public class MushorMinionExplode : ModNPC
    {
        
        private int counter { get { return (int)npc.ai[1]; } set { npc.ai[1] = value; } }
		private const float damageDistance = 64;
        private const float speedMax = 8;
        private const float speed = 2;


        private const int frameTimer = 4;

        private bool created;
		private bool exploded = false;

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Angry Mushroom");
			Main.npcFrameCount[npc.type] = 15;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 50;
            npc.damage = 50;
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
           npc.lifeMax = 60;
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
                counter = 240;
                created = true;
            }
            counter--;

            npc.velocity += Vector2.Normalize((player.Center - npc.Center) * speed);
            npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -speedMax, speedMax);
            npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -speedMax, speedMax);
            if (npc.Distance(player.Center) < 32 || counter <= 0)   //if really close to the enemy or dying or the counter is 0
            {
				Explode(npc.damage);
            }
        }
        
		private void Explode(int damage)
		{
			if (!exploded)
			{
				npc.life = 0;
				Main.PlaySound(SoundID.Item, npc.Center, 14);    //bomb explosion sound
				Main.PlaySound(SoundID.Item, npc.Center, 21); 
				for (int i = 0; i < Main.player.Length; i++)
                {
                    Player player = Main.player[i];
                    if (npc.Distance(player.Center) < damageDistance)
                    {
                        player.Hurt(Terraria.DataStructures.PlayerDeathReason.ByNPC(npc.whoAmI), npc.damage, 0);
                    }
                }
                for (int i = 0; i < 360; i++)
                {
                    Vector2 vec = Vector2.Transform(new Vector2(-damageDistance, 0), Matrix.CreateRotationZ(MathHelper.ToRadians(i)));

                    if (i % 8 == 0)
                    {   //odd
                        Dust.NewDust(npc.Center + vec, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20);
                    }

                    if (i % 9 == 0)
                    {   //even
                        vec.Normalize();
                        Dust.NewDust(npc.Center, Main.rand.Next(1, 7), Main.rand.Next(1, 7), 20, vec.X * 2, vec.Y * 2);
                    }
                }
				exploded = true;
			}
		}
		
    }
}
