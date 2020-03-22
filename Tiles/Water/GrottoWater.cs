using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace Emperia.Tiles.Water
{
	public class GrottoWater : ModWaterStyle
	{
		public override bool ChooseWaterStyle()
		{
			return Main.bgStyle == mod.GetSurfaceBgStyleSlot("TwilightSurfaceBg");
			//return true;
		}

		public override int ChooseWaterfallStyle()
		{
			return mod.GetWaterfallStyleSlot("GrottoWaterfall");
		}

		public override int GetSplashDust()
		{
			return mod.DustType("GrottoSplash");
		}

		public override int GetDropletGore()
		{
			return mod.GetGoreSlot("Gores/GrottoDroplet");
		}

		public override void LightColorMultiplier(ref float r, ref float g, ref float b)
		{
			r = 1f;
			g = 1f;
			b = 1f;
		}

		public override Color BiomeHairColor()
		{
			return Color.Blue;
		}
	}
}