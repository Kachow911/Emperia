using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{
    internal class HarpoonBladeProj : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Harpoon");
		}

		public override void SetDefaults()
        {
            Projectile.width = 6;
			Projectile.height = 6;
			DrawOriginOffsetY = -12;
			DrawOffsetX = -8;
			Projectile.netImportant = true;
			Projectile.friendly = true;
			Projectile.timeLeft = 36000;
			Projectile.penetrate = -1;
        }

        bool latched;
		public float returningSpeed;
		NPC NPC;
		Vector2 npcOffset;
		float oldRot;
		bool reeling;
		int reelingTime;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (!latched) //
			{
				NPC = target;
				Projectile.GetGlobalProjectile<MyProjectile>().latchedNPC = NPC;
				if (Projectile.velocity.X > 0 && Projectile.Center.X > NPC.Left.X || Projectile.velocity.X < 0 && Projectile.Center.X < NPC.Right.X 
				|| Projectile.velocity.Y > 0 && Projectile.Center.Y > NPC.Top.Y || Projectile.velocity.Y < 0 && Projectile.Center.Y < NPC.Bottom.Y)
				{ Projectile.position -= Projectile.velocity * 0.85f; } //prevents harpoons from totally covering NPCs lol
                npcOffset = Projectile.position - NPC.position;
				latched = true;
				oldRot = Projectile.rotation;
				latchDistance = (Main.player[Projectile.owner].MountedCenter - Projectile.position).Length();
				//Main.NewText(((NPC.width + NPC.height) / 2).ToString()); was to make max harpoons stuck in npc = height + width / 22, further ones doing 1/4th damage, glancing off w/ metal sound, too complex
			}
		}

		public override void AI()
		{
			Player player = Main.player[Projectile.owner];
			Vector2 playerCenter = player.MountedCenter;
			Vector2 direction = playerCenter - Projectile.position;
			direction.Normalize();
			Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f; //half of pi
			Projectile.knockBack = 0;

			if (player.dead) Projectile.Kill();
			if (returningSpeed == 0 && Projectile.timeLeft < 35977)
			{
				Projectile.velocity.Y += 0.40f;
				Projectile.velocity.X *= 0.975f;
			}
			if ((playerCenter - Projectile.Center).Length() > 857) //prevents awkwardness from PreDraw not being called over a distance of 857 (on small resolutions) (677 on zoomed in resolutions but fuck that)
			{
				if (latched)
				{
					int defense = player.statDefense;
					if (Main.expertMode) defense = (int)(defense * 0.75f);
					else if (!Main.masterMode) defense /= 2;
					player.Hurt(PlayerDeathReason.ByCustomReason($"{player.name} had their heart ripped out on a chain."), 5 + defense, 0);
					player.immune = false;
					player.immuneTime = 0;
					Projectile.Kill();
					Terraria.Audio.SoundEngine.PlaySound(SoundID.Item153, playerCenter);
				}
				else returningSpeed = 22;
			}

			if (latched)
			{
				Projectile.tileCollide = false;
				Projectile.rotation = oldRot; //Projectile.rotation = rot + NPC.rotation;
				direction = Projectile.Center - playerCenter; // i think this one makes the projectile face correctly after being unlatched (forgot what my own code does :trollface:)
				direction.Normalize();
				Projectile.velocity = Vector2.Zero;
				Projectile.position = NPC.position + npcOffset;
				if (!NPC.active)
				{
					latched = false;
					returningSpeed = 22;
				}
			}
			else if (returningSpeed > 0)
			{
				Projectile.rotation -= 3.14f; //so it doesn't face backwards
				Projectile.damage = 0;
				Projectile.velocity = direction * returningSpeed;
				Projectile.tileCollide = false;
				//Main.NewText((playerCenter - Projectile.Center).Length().ToString()); //remove chain break and force predraw?
				if ((playerCenter - Projectile.Center).Length() < 20)
				{
					Projectile.Kill();
				}
				if (returningSpeed >= 6 && returningSpeed < 22) returningSpeed *= 1.015f;
			}
			else Projectile.spriteDirection = Projectile.direction;

			if (reeling) //(Projectile.ModProjectile as HarpoonBladeProj).Reel(player);
			{
				Vector2 reelAngle = NPC.Center - player.Center;
				reelAngle.Normalize();
				if (latched && (NPC.Center - player.Center).Length() > 25f)
				{
					player.velocity = reelAngle * 11f; //add for same reason
					//player.velocity += reelAngle * 1.75f;
					latchDistance = (playerCenter - Projectile.Center).Length();
					reelingTime = 25;
					//reeling = false; // remove to make continuous pull
				}
				else
				{
					reeling = false;
                    player.velocity = reelAngle * 5f;
				}
			}
			if (reelingTime > 0)
            {
				latchDistance = (playerCenter - Projectile.Center).Length();
				reelingTime--;
			}
		}

		public override bool? CanHitNPC(NPC target)
		{
			if (latched) return false;
			else return null;
		}

        public bool Unchain(Projectile Projectile)
		{
			latched = false;
			returningSpeed = 6;
			return true;
		}
		public bool ReelToggle(Player player)
		{
			if (!reeling) reeling = true;
			else
			{
				reeling = false;
				Vector2 reelAngle = NPC.Center - player.Center;
				reelAngle.Normalize();
				player.velocity = reelAngle * 5f;
			}
			return true;
		}
		public override bool OnTileCollide(Vector2 oldVelocity)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			returningSpeed = 22;
			return false;
        }

		private static Asset<Texture2D> chainTexture;

		public override void Load()
		{ //This is called once on mod (re)load when this piece of content is being loaded.
		  // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
			chainTexture = ModContent.Request<Texture2D>("Emperia/Projectiles/HarpoonBladeChain");
		}

		public override void Unload()
		{ //This is called once on mod reload when this piece of content is being unloaded.
		  // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
			chainTexture = null;
		}

		float sag = 0;
		float curveMidPoint = 0.2f;
		float latchDistance = 0; //how "long" the chain is so that it can sag differently depending on how much is out
		public override bool PreDraw(ref Color lightColor) {
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;


			if (Projectile.timeLeft < 35977 && !latched && returningSpeed == 0)
            {
				if (sag < 150f) sag += 3f;
            }
			if (latched || returningSpeed > 0)
			{
				if (sag > 20f) sag -= 20f;
				else if (sag > 0f) sag -= sag;
			}
			if (returningSpeed > 0)
            {
				if (sag < -20f) sag += 20f;
				else if (sag < 0f) sag -= sag;
			}
			if (latched && sag <= 0)
            {
				curveMidPoint = 0.5f;
				if ((playerCenter - center).Length() > latchDistance) latchDistance = (playerCenter - center).Length();
				sag = -250f * (1 - ((playerCenter - center).Length() / latchDistance));
				if (sag < -250f) sag = -250f;
			}

			//all this stuff up here determines the sag of the curve based on player distance and what the projectile is doing

			Vector2 p0 = center; //all this stuff down here is quadratic bezier curve
			Vector2 p1 = (center + (playerCenter - center) * curveMidPoint) - new Vector2(0, sag);
			Vector2 p2 = playerCenter;
			Vector2 l0 = p0;
			Vector2 l1 = p1;
			Vector2 q = l0;
			Vector2 oldQ;

			float t = 0;
			Vector2 chainPos = center;
			Vector2 chainPosPlus = chainPos; //the chain drawing works best with 3 positions so the angles of each chain can curve smoothly
			Vector2 chainPosOld; 
			float chaingle;

			while (t < 1)
			{
				float spacing = 0.0065f; //* (857 / (playerCenter - Projectile.Center).Length());
				t += spacing;
				l0 += (p1 - p0) * spacing;
				l1 += (p2 - p1) * spacing; 
				oldQ = q; //might not need this
				q = l0 + (l1 - l0) * t; //if condition might be unnecessary
				//so yeah this whole section draws two lines between 3 points and then another point (q) between those two lines

				if (q.X - 5f < chainPosPlus.X && Projectile.Center.X < playerCenter.X && Math.Abs(q.Y - chainPosPlus.Y) < 5f) q = oldQ; 
				else if (q.X + 5f > chainPosPlus.X && Projectile.Center.X > playerCenter.X && Math.Abs(q.Y - chainPosPlus.Y) < 5f) q = oldQ; //only draw chain if curve is far enough ahead
				else //draw chain to follow curve closely, but at set intervals
				{
					chainPosOld = chainPos;
					chainPos = chainPosPlus;
					chainPosPlus = q - chainPosPlus;
					chainPosPlus.Normalize();
					chainPosPlus *= 8;
					chainPosPlus += chainPos;
					chaingle = (chainPosPlus - chainPosOld).ToRotation() - 1.57f;
					if (t == spacing) //makes chain connect with harpoon better
					{
						chainPos = center + new Vector2(0, 4);
						chaingle = (chainPosPlus - chainPos).ToRotation() - 1.57f;
					}
					Color drawColor = lightColor;

					Main.EntitySpriteDraw(chainTexture.Value, chainPos - Main.screenPosition,
					chainTexture.Value.Bounds, drawColor, chaingle,
					chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
				}

			}
			return true;
		}
	}
}
