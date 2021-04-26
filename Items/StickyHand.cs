using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Items
{
	internal class StickyHand : ModItem
	{
		public override void SetStaticDefaults() {
			DisplayName.SetDefault("Sticky Hand");
		}

		public override void SetDefaults() {
			item.CloneDefaults(ItemID.AmethystHook);
			item.shootSpeed = 14f;
			item.shoot = mod.ProjectileType("StickyHandProj");
			item.damage = 16;
			item.knockBack = 0;
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
			projectile.CloneDefaults(ProjectileID.GemHookAmethyst);
			projectile.damage = 16;
		}

		bool latched;
		NPC npc;
		Vector2 offset;
		float rot;
		Vector2 direction;

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			///*
			if (projectile.velocity == Vector2.Zero) ///prevents game from crashing due to hooking both a tile and enemy at once
			{
				projectile.damage = 0;
			}
			else if (!latched)
			{
				npc = target;
				offset = projectile.position - npc.position;
				latched = true;
				rot = projectile.rotation;
			}
			//*/

			//this.OnTileCollide(projectile.velocity);
			//float tempSpeed = 14f;
			//this.GrapplePullSpeed(Main.player[projectile.owner], ref tempSpeed);
		}

		public override void AI()
		{
			projectile.damage = 16;
			projectile.knockBack = 0;
			Player player = Main.player[projectile.owner];
			Vector2 playerCenter = player.MountedCenter;
			if (latched)
			{
				projectile.rotation = rot;
				projectile.damage = 0;

				direction = projectile.Center - player.Center;
				direction.Normalize();
				projectile.velocity = Vector2.Zero;
				projectile.position = npc.position + offset;
				player.velocity = direction * 11f;

				if (!npc.active || player.controlJump)
				{
					projectile.timeLeft = 0;
					//player.velocity *= 0.75f;
				}
			}
		}
		
		public override bool? CanUseGrapple(Player player) {
			int hooksOut = 0;
			for (int l = 0; l < 1000; l++) {
				if (Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == projectile.type && Main.projectile[l].velocity != new Vector2(0, 0)) {
					hooksOut++;
				}
			}
			return (hooksOut == 0);
		}


		public override bool? SingleGrappleHook(Player player)
		{
			return true;
		}

		// Use this to kill oldest hook. For hooks that kill the oldest when shot, not when the newest latches on: Like SkeletronHand
		// You can also change the projectile like: Dual Hook, Lunar Hook
		//public override void UseGrapple(Player player, ref int type)
		//{
		//	int hooksOut = 0;
		//	int oldestHookIndex = -1;
		//	int oldestHookTimeLeft = 100000;
		//	for (int i = 0; i < 1000; i++)
		//	{
		//		if (Main.projectile[i].active && Main.projectile[i].owner == projectile.whoAmI && Main.projectile[i].type == projectile.type)
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
			return 456f;
		}

		//public override void NumGrappleHooks(Player player, ref int numHooks) {
		//	numHooks = 2;
		//}

		// default is 11, Lunar is 24
		public override void GrappleRetreatSpeed(Player player, ref float speed) {
			speed = 14f;
		}

		public override void GrapplePullSpeed(Player player, ref float speed) {
			if (latched) return;
			else speed = 11;
		}

		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor) {
			Vector2 playerCenter = Main.player[projectile.owner].MountedCenter;
			Vector2 center = projectile.Center;
			Vector2 distToProj = playerCenter - projectile.Center;
			float projRotation = distToProj.ToRotation() - 1.57f;
			float distance = distToProj.Length();
			while (distance > 30f && !float.IsNaN(distance)) {
				distToProj.Normalize();                 //get unit vector
				distToProj *= 9f;                      //speed = 24
				center += distToProj;                   //update draw position
				distToProj = playerCenter - center;    //update distance
				distance = distToProj.Length();
				Color drawColor = lightColor;

				//Draw chain
				float height = 16f;
				spriteBatch.Draw(mod.GetTexture("Items/StickyHandChain"), new Vector2(center.X - Main.screenPosition.X, center.Y - Main.screenPosition.Y),
					new Rectangle(0, 0, Main.chain30Texture.Width, (int)height), drawColor, projRotation,
					new Vector2(Main.chain30Texture.Width * 0.5f, height * 0.5f), 1f, SpriteEffects.None, 0f);
			}
			return true;
		}
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			player.fallStart = (int)(player.position.Y / 16f);
			if (latched) player.velocity *= 0.75f;
		}
		
	}
}
