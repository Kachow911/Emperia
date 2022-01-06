using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace Emperia.Backgrounds
{
	public class VolcanoSurfaceBg : ModSurfaceBackgroundStyle
	{
        //public override bool ChooseBgStyle()
        //{
        //    return !Main.gameMenu && (Main.player[Main.myPlayer].GetModPlayer<MyPlayer>().ZoneVolcano);
        //}

        public override void ModifyFarFades(float[] fades, float transitionSpeed)
		{
			for (int i = 0; i < fades.Length; i++)
			{
				if (i == Slot)
				{
					fades[i] += transitionSpeed;
					if (fades[i] > 1f)
					{
						fades[i] = 1f;
					}
				}
				else
				{
					fades[i] -= transitionSpeed;
					if (fades[i] < 0f)
					{
						fades[i] = 0f;
					}
				}
			}
		}
		public override int ChooseCloseTexture(ref float scale, ref double parallax, ref float a, ref float b)
		{
            b -= 750;
            scale = 0.9f;
            return BackgroundTextureLoader.GetBackgroundSlot("Backgrounds/VolcanoBG");
		}
	}
}
