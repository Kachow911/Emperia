using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using static Terraria.ModLoader.ModContent;

namespace Emperia.Tiles.Water
{
	public class GrottoWater : ModWaterStyle
	{

		//public override bool WaterStyle { get { return true; } }

		//public override bool ChooseWaterStyle()
		//{
		//	return Main.bgStyle == Mod.GetSurfaceBgStyleSlot("TwilightSurfaceBg");
		//return true;
		//}

		public override int ChooseWaterfallStyle() => Find<ModWaterfallStyle>("Emperia/Tiles/Water").Slot;

		/*public override int GetSplashDust()
		{
			return ModContent.DustType<GrottoSplash>();
		} lol we dont even have this dust. lame */

		//public override int GetDropletGore()
		//{
		//	return ModContent.Find<ModGore>("Gores/GrottoDroplet").Type;
		//}

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

        public override int GetSplashDust()
        {
            throw new System.NotImplementedException();
        }

        public override int GetDropletGore()
        {
            throw new System.NotImplementedException();
        } //HELLA SUS MIGHT BE VERY BAD
    }
}