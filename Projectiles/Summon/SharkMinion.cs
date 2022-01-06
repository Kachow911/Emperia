using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Emperia.Projectiles.Summon
{
	public class SharkMinion : ModProjectile
	{
        int timer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Depth Scrounger");
			Main.projFrames[base.Projectile.type] = 8;
			ProjectileID.Sets.MinionSacrificable[base.Projectile.type] = true;
			ProjectileID.Sets.CultistIsResistantTo[base.Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
            Projectile.CloneDefaults(ProjectileID.Spazmamini);
            Projectile.width = 30;
            Projectile.height = 34;
            Projectile.minion = true;
            Projectile.friendly = true;
            Projectile.ignoreWater = true;
            Projectile.tileCollide = false;
            Projectile.netImportant = true;
            AIType = ProjectileID.Spazmamini;
            Projectile.alpha = 0;
            Projectile.penetrate = -1;
            Projectile.timeLeft = 18000;
            Projectile.minionSlots = 1;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
           
            return false;
        }

        public override void AI()
		{
            Player player = Main.player[Projectile.owner];
            /* timer+=5;
             Vector2 targetPos = player.Center + new Vector2((float) (50 * Math.Cos(MathHelper.ToRadians(timer))), -50f);
             Projectile.velocity.X += Vector2.Normalize((targetPos - Projectile.Center) * .05f).X;
             Projectile.velocity.X = MathHelper.Clamp(Projectile.velocity.X, -5f, 5f);
             Projectile.velocity.Y = Vector2.Normalize((targetPos - Projectile.Center) * .05f).Y;
             Projectile.velocity.Y = MathHelper.Clamp(Projectile.velocity.Y, -5f, 5f);*/
            if (Projectile.velocity.X > 0)
            {
                Projectile.spriteDirection = -1;
                Projectile.rotation = MathHelper.ToRadians(180 + MathHelper.ToDegrees(Projectile.rotation));
            }
			else Projectile.spriteDirection = 1;
			if (Projectile.velocity.Length() > 7f)
			{
				Color rgb = new Color(83, 66, 180);
				int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X + 4.0), (float)(Projectile.position.Y + 4.0)), Projectile.width - 8, Projectile.height - 8, 76, (float)(Projectile.velocity.X * 0.200000002980232), (float)(Projectile.velocity.Y * 0.200000002980232), 0, rgb, 0.9f);
				Main.dust[index2].position = Projectile.Center;
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity = Projectile.velocity * 0.5f;
			}
           
            bool flag64 = Projectile.type == ModContent.ProjectileType<SharkMinion>();
			
			MyPlayer modPlayer = player.GetModPlayer<MyPlayer>();
			if (flag64)
			{
				if (player.dead)
					modPlayer.sharkMinion = false;

				if (modPlayer.sharkMinion)
					Projectile.timeLeft =2;

			}
		}
		public override bool PreDraw(ref Color lightColor)
        {
				Main.instance.LoadProjectile(Projectile.type);
				Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;

				Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
				for(int k = 0; k < Projectile.oldPos.Length; k++)
				{
					Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
					Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
					Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
				}
			
			return true;
        }

	}
}
