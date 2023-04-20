using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent;

namespace Emperia.Projectiles
{

    public class BlueSword : ModProjectile
    {
		public override void SetStaticDefaults()
		{
			// DisplayName.SetDefault("Blue Day's Blade");
		}
        public override void SetDefaults()
        {
            Projectile.width = 18;
            Projectile.height = 18;
            Projectile.friendly = true;   
            Projectile.DamageType = DamageClass.Melee;      
            Projectile.tileCollide = false;
            Projectile.penetrate = 1;     
            Projectile.timeLeft = 400;
            Projectile.light = 0.75f;
            Projectile.ignoreWater = true;
        }
        public override void AI()
        {
            Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 0.785f;
			Projectile.alpha = 100 + (int) (Math.Cos(Projectile.timeLeft) * 100);
			if(Main.rand.Next(2) == 0)
			{
				int num250 = Dust.NewDust(new Vector2(Projectile.position.X, Projectile.position.Y), Projectile.width, Projectile.height, 187, (float)(Projectile.direction * 2), 0f, 150, new Color(53f, 67f, 253f), 1.3f);
				Main.dust[num250].noGravity = true;
				Main.dust[num250].velocity *= 0f;
			}
			Player player = Main.player[Projectile.owner];
			if (Projectile.Center.Y > player.Center.Y - player.height * 4)
			{
				Projectile.tileCollide = true;
			}
        }
        
		public override void Kill(int timeLeft)
        {
			Terraria.Audio.SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
            for (int index1 = 4; index1 < 31; ++index1)
            {
              float num1 = (float) (Projectile.oldVelocity.X * (30.0 / (double) index1));
              float num2 = (float) (Projectile.oldVelocity.Y * (30.0 / (double) index1));
              int index2 = Dust.NewDust(new Vector2((float) Projectile.oldPosition.X - num1, (float) Projectile.oldPosition.Y - num2), 8, 8, 187, (float) Projectile.oldVelocity.X * 2, (float) Projectile.oldVelocity.Y * 2, 100, Color.LightBlue, 2f);
              Main.dust[index2].noGravity = true;
              Dust dust1 = Main.dust[index2];
              dust1.velocity = dust1.velocity * 0.5f;
              int index3 = Dust.NewDust(new Vector2((float) Projectile.oldPosition.X - num1, (float) Projectile.oldPosition.Y - num2), 8, 8, 187, (float) Projectile.oldVelocity.X, (float) Projectile.oldVelocity.Y, 100, Color.LightBlue, 1.6f);
              Main.dust[index3].noGravity = true;
              Dust dust2 = Main.dust[index3];
              dust2.velocity = dust2.velocity * 0.5f;
            }
        }
        public override bool PreDraw(ref Color lightColor) {
			Main.instance.LoadProjectile(Projectile.type);
Texture2D texture = TextureAssets.Projectile[Projectile.type].Value;
			Vector2 drawOrigin = new Vector2(texture.Width * 0.5f, Projectile.height * 0.5f);
			for (int k = 0; k < Projectile.oldPos.Length; k++) {
				Vector2 drawPos = Projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, Projectile.gfxOffY);
				Color color = Projectile.GetAlpha(lightColor) * ((float)(Projectile.oldPos.Length - k) / (float)Projectile.oldPos.Length);
				Main.EntitySpriteDraw(texture, drawPos, null, color, Projectile.rotation, drawOrigin, Projectile.scale, SpriteEffects.None, 0);
			}
			return true;
		}
    }
}