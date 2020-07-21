using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using Terraria.Utilities;
using Terraria.World.Generation;
using System.ComponentModel;
namespace Emperia.Npcs.StormBoss
{
	[AutoloadBossHead]
    public class StormBoss : ModNPC
    {
        private enum Move
        {
           LightningDash,
           DashRecovery,
		   Shockwave,
		   SweepingBeams,
		   Lightning
        }

		private int counter = 0;

		private Move move;
        private Move prevMove;
        private Vector2 targetPosition;


        private bool phase2Active;
		private bool init = false;
        public Vector2 initialPosition;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Tezlord");
			Main.npcFrameCount[npc.type] = 6;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 12500;
            npc.damage = 75;
            npc.defense = 18;
            npc.knockBackResist = 0f;
            npc.width = 176;
            npc.height = 176;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
           // npc.HitSound = SoundID.NPCHit1; 
            npc.DeathSound = SoundID.NPCDeath1;
            npc.buffImmune[24] = true;
            npc.netAlways = true;
			bossBag = mod.ItemType("StormBag");
        }
      
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.2f;
            npc.frameCounter %= 6;
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }

        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = 25500;
            npc.damage = 100;
        }

        public override void AI()
		{
            npc.TargetClosest(true);
            Player player = Main.player[npc.target];
            if (!init)
            {
                move = Move.LightningDash;
                counter = 100;
                init = true;
            }
            if (move == Move.LightningDash)
            {
                //Main.NewText("Big");
                counter--;
                Vector2 toPlayer = new Vector2(player.Center.X - npc.Center.X, player.Center.Y - npc.Center.Y);
                toPlayer.Normalize();
                
                //LookAt(player.Center);
                npc.rotation = (float)Math.Atan2((double)toPlayer.Y, (double)toPlayer.X) - 1.57f;
                float distToPlayer = (float)Math.Sqrt(Math.Abs(npc.Center.X - player.Center.X) * Math.Abs(npc.Center.X - player.Center.X) + Math.Abs(npc.Center.Y - player.Center.Y) * Math.Abs(npc.Center.Y - player.Center.Y));
                int multFactor;
                if (distToPlayer < 100)
                {
                    multFactor = 30;
                }
                else if (distToPlayer < 800)
                {
                    multFactor = 40;
                }
                else
                {
                    multFactor = 60;
                }
                    if (counter <= 0)
                {
                    npc.velocity = toPlayer.RotatedBy(MathHelper.ToRadians(Main.rand.Next(-10, 10))) * (float)multFactor; // jets in somewhat of a lightning pattern towards the player
                    initialPosition = npc.Center;
                    SetMove(Move.DashRecovery, 180);
                }
                
            }
            else if (move == Move.DashRecovery)
            {
                if (LineIntersectsRect(new Vector2(npc.Center.X, npc.Center.Y), new Vector2(initialPosition.X, initialPosition.Y), player.Hitbox))
                {
                    Main.NewText("Intersection");
                }
                Line line1 = new Line(npc.Center, player.Center);
                line1.Draw(Main.spriteBatch, default(Color));
                Vector2 toBoss = new Vector2(npc.Center.X - initialPosition.X, npc.Center.Y - initialPosition.Y);
                
                int numDusts = 100;
                for (float i = 0; i <= numDusts; i++)
                {
                    if (Main.rand.Next(1, 10) <= 3) {
                        int dust1 = Dust.NewDust(new Vector2(initialPosition.X + ((npc.Center.X - initialPosition.X) * (i / numDusts)), initialPosition.Y + ((npc.Center.Y - initialPosition.Y) * (i / numDusts))), 8, 8, 226);
                        Main.dust[dust1].noGravity = true;
                    }
                }
                //if (counter % 2 == 0)
                npc.velocity *= .95f;
                counter--;
                if (counter <= 0)
                {
                    SetMove(Move.Lightning, 60);

                }
            }
            else if (move == Move.Lightning)
            {
                int dust = Dust.NewDust(npc.Center + new Vector2(0, 25), 8, 8, 226);
                if (counter == 0)
                {

                    SetMove(Move.LightningDash, 100);
                }
                counter--;
            } else if (move == Move.Shockwave)
            {
                counter--;
            } else if (move == Move.SweepingBeams)
            {                
                counter--;
            }
        }
		

        

       /* private void SmoothMoveToPosition(Vector2 toPosition, float addSpeed, float maxSpeed, float slowRange = 64, float slowBy = .95f)
        {
            if (Math.Abs((toPosition - npc.Center).Length()) >= slowRange)
            {
                npc.velocity += Vector2.Normalize((toPosition - npc.Center) * addSpeed);
                npc.velocity.X = MathHelper.Clamp(npc.velocity.X, -maxSpeed, maxSpeed);
                npc.velocity.Y = MathHelper.Clamp(npc.velocity.Y, -maxSpeed, maxSpeed);
            }
            else
            {
                npc.velocity *= slowBy;
            }
        }*/

        private bool IsBelowPhaseTwoThreshhold()
        {
            return npc.life <= npc.lifeMax / 2;     
        }

        private void SetMove(Move toMove, int counter)
        {
            prevMove = move;
            move = toMove;
            this.counter = counter;
        }
        public static bool LineIntersectsRect(Vector2 p1, Vector2 p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y), new Vector2(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y), new Vector2(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X + r.Width, r.Y + r.Height), new Vector2(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Vector2(r.X, r.Y + r.Height), new Vector2(r.X, r.Y)) ||
                   r.Contains(new Point((int)(p1.X), (int)(p1.Y))) && r.Contains(new Point((int)(p2.X), (int)(p2.Y)));
        }

        private static bool LineIntersectsLine(Vector2 l1p1, Vector2 l1p2, Vector2 l2p1, Vector2 l2p2)
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            if (r < 0 || r > 1 || s < 0 || s > 1)
            {
                return false;
            }

            return true;
        }
        private Vector2[] getJaggedPoints(Vector2 p1, Vector2 p2)
        {

        }
       
    }
    
}

