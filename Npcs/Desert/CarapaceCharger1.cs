using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Emperia.Projectiles;
using static Terraria.ModLoader.ModContent;
 
namespace Emperia.Npcs.Desert
{
    public class CarapaceCharger1 : ModNPC
    {
		private bool colliding = false;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Carapace Charger");
			Main.npcFrameCount[NPC.type] = 6;
		}
        public override void SetDefaults()
        {
  
            NPC.lifeMax = 125;        //this is the NPC health
            NPC.damage = 15;    //this is the NPC damage
            NPC.defense = 2;         //this is the NPC defense
            NPC.knockBackResist = 0f;
            NPC.width = 42; //this is where you put the NPC sprite width.     important
            NPC.height = 32; //this is where you put the NPC sprite height.   important
            NPC.boss = false;
            NPC.lavaImmune = true;       //this make the NPC immune to lava
            NPC.noGravity = true;           //this make the NPC float
            NPC.noTileCollide = true;        //this make the NPC go thru walls
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.behindTiles = true;          
            NPC.value = Item.buyPrice(0, 0, 2, 10);
            NPC.npcSlots = 1f;
            NPC.netAlways = true;
        }
		public override void FindFrame(int frameHeight)
		{
			NPC.frameCounter += 0.2f;
			NPC.frameCounter %= 6; 
			int frame = (int)NPC.frameCounter; 
			NPC.frame.Y = frame * frameHeight;
		}
		public override void ModifyNPCLoot(NPCLoot npcLoot)
		{
			if (Main.rand.Next(2) == 0)
				{
					Item.NewItem(NPC.GetSource_Loot(), (int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height, ModContent.ItemType<Items.Sets.PreHardmode.Desert.PolishedSandstone>(), Main.rand.Next(1, 4));
				}
		}
        public override bool PreAI()
        {
            if (Main.netMode != 1)
            {
                // So, we start the AI off by checking if NPC.ai[0] is 0.
                // This is practically ALWAYS the case with a freshly spawned NPC, so this means this is the first update.
                // Since this is the first update, we can safely assume we need to spawn the rest of the worm (bodies + tail).
                if (NPC.ai[0] == 0)
                {
                    // So, here we assing the NPC.realLife value.
                    // The NPC.realLife value is mainly used to determine which NPC loses life when we hit this NPC.
                    // We don't want every single piece of the worm to have its own HP pool, so this is a neat way to fix that.
                    NPC.realLife = NPC.whoAmI;
                    // LatestNPC is going to be used later on and I'll explain it there.
                    int latestNPC = NPC.whoAmI;
 
                    // Here we determine the length of the worm.
                    // In this case the worm will have a length of 10 to 14 body parts.
                    
                    for (int i = 0; i < 8; ++i)
                    {
                        // We spawn a new NPC, setting latestNPC to the newer NPC, whilst also using that same variable
                        // to set the parent of this new NPC. The parent of the new NPC (may it be a tail or body part)
                        // will determine the movement of this new NPC.
                        // Under there, we also set the realLife value of the new NPC, because of what is explained above.
                        latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<CarapaceCharger2>(), NPC.whoAmI, 0, latestNPC);
                        Main.npc[(int)latestNPC].realLife = NPC.whoAmI;
                        Main.npc[(int)latestNPC].ai[3] = NPC.whoAmI;
                    }
                    // When we're out of that loop, we want to 'close' the worm with a tail part!
                    latestNPC = NPC.NewNPC(NPC.GetSource_FromAI(), (int)NPC.Center.X, (int)NPC.Center.Y, NPCType<CarapaceCharger3>(), NPC.whoAmI, 0, latestNPC);
                    Main.npc[(int)latestNPC].realLife = NPC.whoAmI;
                    Main.npc[(int)latestNPC].ai[3] = NPC.whoAmI;
 
                    // We're setting NPC.ai[0] to 1, so that this 'if' is not triggered again.
                    NPC.ai[0] = 1;
                    NPC.netUpdate = true;
                }
            }
 
            int minTilePosX = (int)(NPC.position.X / 16.0) - 1;
            int maxTilePosX = (int)((NPC.position.X + NPC.width) / 16.0) + 2;
            int minTilePosY = (int)(NPC.position.Y / 16.0) - 1;
            int maxTilePosY = (int)((NPC.position.Y + NPC.height) / 16.0) + 2;
            if (minTilePosX < 0)
                minTilePosX = 0;
            if (maxTilePosX > Main.maxTilesX)
                maxTilePosX = Main.maxTilesX;
            if (minTilePosY < 0)
                minTilePosY = 0;
            if (maxTilePosY > Main.maxTilesY)
                maxTilePosY = Main.maxTilesY;
 
            bool collision = false;
            // This is the initial check for collision with tiles.
            for (int i = minTilePosX; i < maxTilePosX; ++i)
            {
                for (int j = minTilePosY; j < maxTilePosY; ++j)
                {
                    if (Main.tile[i, j] != null && (Main.tile[i, j].HasUnactuatedTile && (Main.tileSolid[(int)Main.tile[i, j].TileType] || Main.tileSolidTop[(int)Main.tile[i, j].TileType] && (int)Main.tile[i, j].TileFrameY == 0) || (int)Main.tile[i, j].LiquidType > 64))
                    {
                        Vector2 vector2; 
                        vector2.X = (float)(i * 16);
                        vector2.Y = (float)(j * 16);
                        if (NPC.position.X + NPC.width > vector2.X && NPC.position.X < vector2.X + 16.0 && (NPC.position.Y + NPC.height > (double)vector2.Y && NPC.position.Y < vector2.Y + 16.0))
                        {
							colliding = true;
                            collision = true;
                            if (Main.rand.Next(100) == 0 && Main.tile[i, j].HasUnactuatedTile)
                                WorldGen.KillTile(i, j, true, true, false);
                        }
						else
						{
							if (colliding)
							{
							
								Vector2 perturbedSpeed = new Vector2(0, -5).RotatedByRandom(MathHelper.ToRadians(45));
                                if (Main.rand.NextBool(2))
                                {
                                    int p = Projectile.NewProjectile(NPC.GetSource_FromAI(), NPC.Center.X, NPC.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, ModContent.ProjectileType<SandBlock>(), NPC.damage / 3, 1, Main.myPlayer, 0, 0);
                                    Main.projectile[p].rotation = MathHelper.ToRadians(Main.rand.Next(360));
                                }
								colliding = false;
							}
						}
                    }
                }
            }
            // If there is no collision with tiles, we check if the distance between this NPC and its target is too large, so that we can still trigger 'collision'.
            if (!collision)
            {
                Rectangle rectangle1 = new Rectangle((int)NPC.position.X, (int)NPC.position.Y, NPC.width, NPC.height);
                int maxDistance = 1000;
                bool playerCollision = true;
                for (int index = 0; index < 255; ++index)
                {
                    if (Main.player[index].active)
                    {
                        Rectangle rectangle2 = new Rectangle((int)Main.player[index].position.X - maxDistance, (int)Main.player[index].position.Y - maxDistance, maxDistance * 2, maxDistance * 2);
                        if (rectangle1.Intersects(rectangle2))
                        {
                            playerCollision = false;
                            break;
                        }
                    }
                }
                if (playerCollision)
                    collision = true;
            }
 
            // speed determines the max speed at which this NPC can move.
            // Higher value = faster speed.
            float speed = 15f;
            // acceleration is exactly what it sounds like. The speed at which this NPC accelerates.
            float acceleration = 0.08f;
 
            Vector2 npcCenter = new Vector2(NPC.position.X + NPC.width * 0.5f, NPC.position.Y + NPC.height * 0.5f);
            float targetXPos = Main.player[NPC.target].position.X + (Main.player[NPC.target].width / 2);
            float targetYPos = Main.player[NPC.target].position.Y + (Main.player[NPC.target].height / 2);
 
            float targetRoundedPosX = (float)((int)(targetXPos / 16.0) * 16);
            float targetRoundedPosY = (float)((int)(targetYPos / 16.0) * 16);
            npcCenter.X = (float)((int)(npcCenter.X / 16.0) * 16);
            npcCenter.Y = (float)((int)(npcCenter.Y / 16.0) * 16);
            float dirX = targetRoundedPosX - npcCenter.X;
            float dirY = targetRoundedPosY - npcCenter.Y;
 
            float length = (float)Math.Sqrt(dirX * dirX + dirY * dirY);
            // If we do not have any type of collision, we want the NPC to fall down and de-accelerate along the X axis.
            if (!collision)
            {
                NPC.TargetClosest(true);
                NPC.velocity.Y = NPC.velocity.Y + 0.11f;
                if (NPC.velocity.Y > speed)
                    NPC.velocity.Y = speed;
                if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.4)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    else
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                }
                else if (NPC.velocity.Y == speed)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                }
                else if (NPC.velocity.Y > 4.0)
                {
                    if (NPC.velocity.X < 0.0)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 0.9f;
                    else
                        NPC.velocity.X = NPC.velocity.X - acceleration * 0.9f;
                }
            }
            // Else we want to play some audio (soundDelay) and move towards our target.
            else
            {
                if (NPC.soundDelay == 0)
                {
                    float num1 = length / 40f;
                    if (num1 < 10.0)
                        num1 = 10f;
                    if (num1 > 20.0)
                        num1 = 20f;
                    NPC.soundDelay = (int)num1;
                    Terraria.Audio.SoundEngine.PlaySound(SoundID.WormDig, NPC.position);
                }
                float absDirX = Math.Abs(dirX);
                float absDirY = Math.Abs(dirY);
                float newSpeed = speed / length;
                dirX = dirX * newSpeed;
                dirY = dirY * newSpeed;
                if (NPC.velocity.X > 0.0 && dirX > 0.0 || NPC.velocity.X < 0.0 && dirX < 0.0 || (NPC.velocity.Y > 0.0 && dirY > 0.0 || NPC.velocity.Y < 0.0 && dirY < 0.0))
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration;
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    if (Math.Abs(dirY) < speed * 0.2 && (NPC.velocity.X > 0.0 && dirX < 0.0 || NPC.velocity.X < 0.0 && dirX > 0.0))
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration * 2f;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration * 2f;
                    }
                    if (Math.Abs(dirX) < speed * 0.2 && (NPC.velocity.Y > 0.0 && dirY < 0.0 || NPC.velocity.Y < 0.0 && dirY > 0.0))
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration * 2f;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration * 2f;
                    }
                }
                else if (absDirX > absDirY)
                {
                    if (NPC.velocity.X < dirX)
                        NPC.velocity.X = NPC.velocity.X + acceleration * 1.1f;
                    else if (NPC.velocity.X > dirX)
                        NPC.velocity.X = NPC.velocity.X - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.Y > 0.0)
                            NPC.velocity.Y = NPC.velocity.Y + acceleration;
                        else
                            NPC.velocity.Y = NPC.velocity.Y - acceleration;
                    }
                }
                else
                {
                    if (NPC.velocity.Y < dirY)
                        NPC.velocity.Y = NPC.velocity.Y + acceleration * 1.1f;
                    else if (NPC.velocity.Y > dirY)
                        NPC.velocity.Y = NPC.velocity.Y - acceleration * 1.1f;
                    if (Math.Abs(NPC.velocity.X) + Math.Abs(NPC.velocity.Y) < speed * 0.5)
                    {
                        if (NPC.velocity.X > 0.0)
                            NPC.velocity.X = NPC.velocity.X + acceleration;
                        else
                            NPC.velocity.X = NPC.velocity.X - acceleration;
                    }
                }
            }
            // Set the correct rotation for this NPC.
            NPC.rotation = (float)Math.Atan2(NPC.velocity.Y, NPC.velocity.X) + 1.57f;
           
            // Some netupdate stuff (multiplayer compatibility).
            if (collision)
            {
                if (NPC.localAI[0] != 1)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 1f;
            }
            else
            {
                if (NPC.localAI[0] != 0.0)
                    NPC.netUpdate = true;
                NPC.localAI[0] = 0.0f;
            }
            if ((NPC.velocity.X > 0.0 && NPC.oldVelocity.X < 0.0 || NPC.velocity.X < 0.0 && NPC.oldVelocity.X > 0.0 || (NPC.velocity.Y > 0.0 && NPC.oldVelocity.Y < 0.0 || NPC.velocity.Y < 0.0 && NPC.oldVelocity.Y > 0.0)) && !NPC.justHit)
                NPC.netUpdate = true;
 
            return false;
        }
 
        /*public override bool PreDraw(Microsoft.Xna.Framework.Graphics.ref Color drawColor)
        {
            Texture2D texture = Main.npcTexture[NPC.type];
            Vector2 origin = new Vector2(texture.Width * 0.5f, texture.Height * 0.5f);
            Main.EntitySpriteDraw(texture, NPC.Center - Main.screenPosition, new Rectangle?(), drawColor, NPC.rotation, origin, NPC.scale, SpriteEffects.None, 0);
            return false;
        }*/
		public override float SpawnChance(NPCSpawnInfo spawnInfo)
		{
			int x = spawnInfo.SpawnTileX;
			int y = spawnInfo.SpawnTileY;
			int tile = Main.tile[x, y].TileType;
			return spawnInfo.Player.ZoneDesert ? 0.02f : 0;
		}
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.0f;   //this make the NPC Health Bar biger
            return null;
        }
    }
}