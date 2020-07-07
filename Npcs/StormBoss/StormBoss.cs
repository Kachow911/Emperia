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

namespace Emperia.Npcs.StormBoss
{
	//[AutoloadBossHead]
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
            npc.knockBackResist = 100f;
            npc.width = 135;
            npc.height = 135;
            npc.value = Item.buyPrice(0, 8, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1; 
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
                if (counter <= 0)
                {
                    npc.velocity = toPlayer * 32f; // jets in somewhat of a lightning pattern towards the player
                    initialPosition = npc.Center;
                    SetMove(Move.DashRecovery, 180);
                }
                
            }
            else if (move == Move.DashRecovery)
            {
                int dust = Dust.NewDust(npc.Center, 8, 8, 226);
                
                if (LineIntersectsRect(new Point((int)npc.Center.X, (int)npc.Center.Y), new Point((int)initialPosition.X, (int)initialPosition.Y), player.Hitbox))
                {
                    Main.NewText("Intersection");
                }
                float lineLength = (float)Math.Sqrt(Math.Abs(npc.Center.X - initialPosition.X) * Math.Abs(npc.Center.X - initialPosition.X) + Math.Abs(npc.Center.Y - initialPosition.Y) * Math.Abs(npc.Center.Y - initialPosition.Y));

                int numDusts = 5;
                for (float i = 0; i <= numDusts; i++)
                { // make npc center and inital position variables that dont change after it starts dashing to fix I gotta go :))))))
                    int dust1 = Dust.NewDust(new Vector2((float)(npc.Center.X + Math.Sqrt(Math.Pow((i / numDusts) * lineLength, 2) - Math.Pow(npc.Center.Y - initialPosition.Y, 2))), (float)(npc.Center.Y + Math.Sqrt(Math.Pow((i / numDusts) * lineLength, 2) - Math.Pow(npc.Center.X - initialPosition.X, 2))w)), 8, 8, 226);
                }
                //if (counter % 2 == 0)
                npc.velocity *= .95f;
                counter--;
                if (counter <= 0)
                {
                    SetMove(Move.LightningDash, 100);

                }
            }
            else if (move == Move.Lightning)
            {
                
                counter--;
            } else if (move == Move.Shockwave)
            {
                counter--;
            } else if (move == Move.SweepingBeams)
            {
                counter--;
            }
        }
		

        public override void PostDraw(SpriteBatch batch, Color drawColor)
        {
           
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
		public override void NPCLoot()
		{
			
			
		}
        public static bool LineIntersectsRect(Point p1, Point p2, Rectangle r)
        {
            return LineIntersectsLine(p1, p2, new Point(r.X, r.Y), new Point(r.X + r.Width, r.Y)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y), new Point(r.X + r.Width, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X + r.Width, r.Y + r.Height), new Point(r.X, r.Y + r.Height)) ||
                   LineIntersectsLine(p1, p2, new Point(r.X, r.Y + r.Height), new Point(r.X, r.Y)) ||
                   (r.Contains(p1) && r.Contains(p2));
        }

        private static bool LineIntersectsLine(Point l1p1, Point l1p2, Point l2p1, Point l2p2)
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
    }
}
