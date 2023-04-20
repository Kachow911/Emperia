using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace Emperia.Projectiles
{

    public class Cerith : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Cerith");
		}
        public override void SetDefaults()
        {
            Projectile.width = 14;
            Projectile.height = 14;
            Projectile.friendly = true;
            Projectile.DamageType = DamageClass.Magic;
            Projectile.tileCollide = true;
            Projectile.penetrate = 1;
            Projectile.timeLeft = 140;
            Projectile.extraUpdates = 1;
            Projectile.ignoreWater = true;
			//Projectile.aiStyle = 29;
			//Projectile.alpha = 255;
			Main.projFrames[Projectile.type] = 4;
			DrawOffsetX = -6;
			//DrawOriginOffsetY
			//Projectile.light = 0.8f;
		}

		/*public void FadeInAndOut()
		{
			if (Projectile.ai[0] <= 50f)
			{
				Projectile.alpha -= 25;
				if (Projectile.alpha <= 0) Projectile.alpha = 0;
			}
		}*/
		public override void AI()
        {
            Lighting.AddLight(Projectile.position, 0f, 0.3f, 0f);
			//Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			//Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.ToRadians(90f);
			if (Projectile.timeLeft % 2 == 0)
            {
                int dust = Dust.NewDust(Projectile.Center - new Vector2(5, 5) - new Vector2(20, 20) * Vector2.Normalize(Projectile.velocity), 10, 10, 107, Projectile.velocity.X, Projectile.velocity.Y, 0, default(Color), 0.6f);
                Main.dust[dust].velocity *= 0.5f;
            }

			Projectile.frameCounter++;
			if (Projectile.frameCounter >= 7)
			{
				Projectile.frameCounter = 0;
				Projectile.frame = (Projectile.frame + 1) % 4;
			}

		}


        /*public override void PostDraw(Color lightColor)
		{
			// SpriteEffects helps to flip texture horizontally and vertically
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (Projectile.spriteDirection == -1)
				spriteEffects = SpriteEffects.FlipHorizontally;

			// Getting texture of projectile
			Texture2D texture = (Texture2D)Mod.Assets.Request<Texture2D>("Projectiles/CerithGlowmask");

			// Calculating frameHeight and current Y pos dependence of frame
			// If texture without animation frameHeight is always texture.Height and startY is always 0
			int frameHeight = texture.Height / Main.projFrames[Projectile.type];
			int startY = frameHeight * Projectile.frame;

			// Get this frame on texture
			Rectangle sourceRectangle = new Rectangle(0, startY, texture.Width, frameHeight);

			// Alternatively, you can skip defining frameHeight and startY and use this:
			// Rectangle sourceRectangle = texture.Frame(1, Main.projFrames[Projectile.type], frameY: Projectile.frame);

			Vector2 origin = sourceRectangle.Size() / 2f;

			// If image isn't centered or symmetrical you can specify origin of the sprite
			// (0,0) for the upper-left corner
			float offsetX = 12f;
			//offsetX *= Projectile.rotation;
			//offsetX += 10f;
			origin.X = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Width - offsetX : offsetX);
			Main.NewText(MathHelper.ToDegrees(Projectile.rotation));

			// If sprite is vertical
			float offsetY = 34f;
			origin.Y = (float)(Projectile.spriteDirection == 1 ? sourceRectangle.Height - offsetY : offsetY);


			// Applying lighting and draw current frame
			Color drawColor = Projectile.GetAlpha(lightColor);
			Main.EntitySpriteDraw(texture,
				Projectile.Center - Main.screenPosition + new Vector2(0f, Projectile.gfxOffY),
				sourceRectangle, drawColor, Projectile.rotation, origin, Projectile.scale, spriteEffects, 0);

			// It's important to return false, otherwise we also draw the original texture.
			return;
		}*/

		public override void Kill(int timeLeft)
		{
            Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, 7);
			    int dust2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 0.8f);
				Main.dust[dust2].velocity *= 1.5f;
                int dust2copy = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 0.8f);
                Vector2 vel = new Vector2(0, -1).RotatedBy(Main.rand.NextFloat() * 6.283f) * 3.5f;
			}
            //{
				//int index2 = Dust.NewDust(new Vector2(Projectile.Center.X, Projectile.Center.Y), Projectile.width, Projectile.height, 107, 0.0f, 0.0f, 15, default(Color), 1f);
				//Main.dust[index2].noGravity = true;
				//Main.dust[index2].velocity *= 2f;
			//}
		}
        //public override void PostDraw(ref Color lightColor)
		//{
		//	Main.EntitySpriteDraw(Mod.Assets.Request<Texture2D>("Projectiles/Cerith_Glow").Value, Projectile.Center - Main.screenPosition, null, Color.White, 0f, new Vector2(11f, 19f), Projectile.scale, SpriteEffects.None, 0);
        //}
    }
}
