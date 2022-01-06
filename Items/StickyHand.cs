using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ReLogic.Content;


namespace Emperia.Items
{
	internal class StickyHand : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sticky Hand");
		}

		public override void SetDefaults() {
			Item.CloneDefaults(ItemID.AmethystHook);
			Item.shootSpeed = 17f;
			Item.shoot = ModContent.ProjectileType<StickyHandProj>();
			Item.damage = 16;
			Item.knockBack = 0;
		}
	}

	internal class StickyHandProj : ModProjectile
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sticky Hand");
		}

		public override void SetDefaults() {
			/*	this.netImportant = true;
				this.name = "Gem Hook";
				this.width = 18;
				this.height = 18;
				this.aiStyle = 7;
				this.friendly = true;
				this.penetrate = -1;
				this.tileCollide = false;
				this.timeLeft *= 10;
			*/
			Projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
			Projectile.damage = 16;
		}

		bool latched;
		NPC NPC;
		Vector2 offset;
		float rot;
		Vector2 direction;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			///*
			if (Projectile.velocity == Vector2.Zero) ///prevents game from crashing due to hooking both a tile and enemy at once
			{
				Projectile.damage = 0;
			}
			else if (!latched)
			{
				NPC = target;
				offset = Projectile.position - NPC.position;
				latched = true;
				rot = Projectile.rotation;
			}
			//*/

			//this.OnTileCollide(Projectile.velocity);
			//float tempSpeed = 14f;
			//this.GrapplePullSpeed(Main.player[Projectile.owner], ref tempSpeed);
		}

		public override void AI()
		{
			Projectile.damage = 16;
			Projectile.knockBack = 0;
			Player player = Main.player[Projectile.owner];
			Vector2 playerCenter = player.MountedCenter;
			if (latched)
			{
				Projectile.rotation = rot;
				Projectile.damage = 0;

				direction = Projectile.Center - player.Center;
				direction.Normalize();
				Projectile.velocity = Vector2.Zero;
				Projectile.position = NPC.position + offset;
                player.velocity = direction * 11f;// maybe start at 10f and multiply by 1.0015f until over 15f (to accelerate on speedy bosses)
                //player.GetModPlayer<MyPlayer>().velocityBoost = direction * 9f;

				if (!NPC.active || player.controlJump)
				{
					Projectile.timeLeft = 0;
					//player.velocity *= 0.75f;
				}
			}
		}
		
		public override bool? CanUseGrapple(Player player) {
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++) {
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == Projectile.type) {
					if ((Main.projectile[l].ModProjectile as StickyHandProj).latched)
					{
						Main.projectile[l].timeLeft = 0;
						hooksOut++;
					}
					else if (Main.projectile[l].velocity != new Vector2(0, 0)) hooksOut++;
				}
			}
			return (hooksOut == 0);
		}


		public override bool? SingleGrappleHook(Player player)
		{
			return true;
		}

		// Use this to kill oldest hook. For hooks that kill the oldest when shot, not when the newest latches on: Like SkeletronHand
		// You can also change the Projectile like: Dual Hook, Lunar Hook
		//public override void UseGrapple(Player player, ref int type)
		//{
		//	int hooksOut = 0;
		//	int oldestHookIndex = -1;
		//	int oldestHookTimeLeft = 100000;
		//	for (int i = 0; i < 1000; i++)
		//	{
		//		if (Main.projectile[i].active && Main.projectile[i].owner == Projectile.whoAmI && Main.projectile[i].type == Projectile.type)
		//		{
		//			hooksOut++;
		//			if (Main.projectile[i].timeLeft < oldestHookTimeLeft)
		//			{
		//				oldestHookIndex = i;
		//				oldestHookTimeLeft = Main.projectile[i].timeLeft;
		//			}
		//		}
		//	}
		//	if (hooksOut > 1)
		//	{
		//		Main.projectile[oldestHookIndex].Kill();
		//	}
		//}

		// Amethyst Hook is 300, Static Hook is 600, 1 tile = 16
		public override float GrappleRange() {
			//return 456f;
			return 380f;
		}

		//public override void NumGrappleHooks(Player player, ref int numHooks) {
		//	numHooks = 2;
		//}

		// default is 11, Lunar is 24
		public override void GrappleRetreatSpeed(Player player, ref float speed) {
			speed = 24f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed) {
			if (latched) return;
			else speed = 11;
		}

		private static Asset<Texture2D> chainTexture;

		public override void Load()
		{ //This is called once on mod (re)load when this piece of content is being loaded.
		  // This is the path to the texture that we'll use for the hook's chain. Make sure to update it.
			chainTexture = ModContent.Request<Texture2D>("Emperia/Items/StickyHandChain");
		}

		public override void Unload()
		{ //This is called once on mod reload when this piece of content is being unloaded.
		  // It's currently pretty important to unload your static fields like this, to avoid having parts of your mod remain in memory when it's been unloaded.
			chainTexture = null;
		}

		public override bool PreDraw(ref Color lightColor) {
			Vector2 playerCenter = Main.player[Projectile.owner].MountedCenter;
			Vector2 center = Projectile.Center;
			Vector2 distToProj = playerCenter - Projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 20f && !float.IsNaN(distance)) {
				distToProj.Normalize();                 //get unit vector
				distToProj *= 9f;                      //speed = 24
				center += distToProj;                   //update draw position
				distToProj = playerCenter - center;    //update distance
				distance = distToProj.Length();
				Color drawColor = lightColor;

				//Draw chain
				float height = 16f;
				Main.EntitySpriteDraw(chainTexture.Value, center - Main.screenPosition,
				chainTexture.Value.Bounds, drawColor, projRotation,
				chainTexture.Size() * 0.5f, 1f, SpriteEffects.None, 0);
				//Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Items/StickyHandChain").Value, new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
				//	new Rectangle(0, 0, Main.chain30Texture.Width, (int)height), drawColor, projRotation,
				//	new Vector2(Main.chain30Texture.Width * 0.5f, height * 0.5f), 1f, SpriteEffects.None, 0);
			}
			return true;
		}
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[Projectile.owner];
			player.fallStart = (int)(player.position.Y / 16f);
			if (latched) player.velocity *= 0.75f;
		}
		
	}
}
