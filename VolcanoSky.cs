using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace Emperia
{
	public class VolcanoSky : CustomSky
	{
		private bool isActive = false;
		private float intensity = 0f;
		private int inqIndex = -1;

		public override void Update(GameTime gameTime)
		{
			if (isActive && intensity < 1f)
			{
				intensity += 0.01f;
			}
			else if (!isActive && intensity > 0f)
			{
				intensity -= 0.01f;
			}
		}

		private float GetIntensity()
		{
			return 1f - Utils.SmoothStep(3000f, 6000f, 200f);
		}

		public override Color OnTileColor(Color inColor)
		{
			float intensity = this.GetIntensity();
			return new Color(Vector4.Lerp(new Vector4(0.5f, 0.8f, 1f, 1f), inColor.ToVector4(), 1f - intensity));
		}

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
        {
            if (maxDepth >= 0 && minDepth < 0)
            {
                float intensity = this.GetIntensity();
				//Main.EntitySpriteDraw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), Color.Orange * 0.1f);
				//this might not be the same thing lol also this code might suck completely shooting in the dark here
				Main.EntitySpriteDraw(Main.Assets.Request<Texture2D>("Terraria/Images/Black_Tile").Value, new Vector2(Main.screenWidth, Main.screenHeight), null, Color.Orange * 0.1f, 0, Main.screenPosition, 1f, SpriteEffects.None, 0);
				
			}
        }

        public override float GetCloudAlpha()
		{
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			isActive = false;
		}

		public override void Reset()
		{
			isActive = false;
		}

		public override bool IsActive()
		{
			return isActive || intensity > 0f;
		}
    }
}